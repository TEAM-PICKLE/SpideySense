#include <WiFi.h>

// USER DEFINED
int MotorPins[] = {3}; // Array of motor pins
// END USER DEFINED

// Declare the server here
WiFiServer server(80);  // Declare server to handle the connection

char PING[] = "PING";
int loops = 0;

int num_Motors = 1; // Calculate the length of the array
// int Intensity1[num_Motors];
int* Intensity1;  // Declare a pointer for the array
int Desired_Motor;  // Declare the Desired_Motor variable

// Call Setup_Wifi() from the WifiSetup.ino file
void setup() {
  Setup_Wifi(server);  // This will initialize the Wi-Fi network and start the server

  // Dynamically allocate memory for Intensity1 based on num_Motors
  Intensity1 = new int[num_Motors];

  for (int i = 0; i < num_Motors; i++) {
    pinMode(MotorPins[i], OUTPUT); // Initialize each motor pin as OUTPUT
  }
}

void loop() {
  // The server should be set up by now, so we can accept clients
  WiFiClient client = server.available();
  if (client) {
    Serial.println("Client connected!");
    
    // Send initial response to the client
    client.println("HTTP/1.1 200 OK");
    client.println("Content-Type: text/plain");
    client.println("Connection: close");
    client.println();
    
    while (client.connected()) {
      loops++;

      // **Check for incoming messages**
      if (client.available()) {
        String receivedMessage = client.readStringUntil('\n'); // Read until newline
        receivedMessage.trim(); // Remove any extra whitespace
        Serial.println("Received message: " + receivedMessage);

        // Check if the message length is greater than 0
        if (receivedMessage.length() > 0) {
        Desired_Motor = receivedMessage[0] - '0';  // Convert char to int

        // Check if Desired_Motor is within valid range
        if (Desired_Motor >= 0 && Desired_Motor < num_Motors) {
            Intensity1[Desired_Motor] = receivedMessage.substring(2).toInt();  // Get the substring from index 2 to the end and convert to int
            Serial.println("Actuating Motor " + String(Desired_Motor) + " on Pin " + String(MotorPins[Desired_Motor]));

            // Call the Run_Motor function
            Run_Motors(MotorPins[Desired_Motor], Intensity1[Desired_Motor]);
        } else {
            // If Desired_Motor is out of bounds
            client.println("Error: Invalid motor number");
        }
        delay(1000);
    } else {
        // If message is empty or does not meet criteria
        client.println("Received: " + receivedMessage + " - it sucks!");
    }

      }
    }
    
    Serial.println("Client disconnected");

    // After the client disconnects, reset all motor intensities to zero
    for (int i = 0; i < num_Motors; i++) {
      Intensity1[i] = 0;
      Run_Motors(MotorPins[i], Intensity1[i]);  // Ensure motors are stopped
      Serial.println("Motor " + String(i) + " reset to intensity 0.");
    }

    client.stop(); // Close the connection
  }
}
