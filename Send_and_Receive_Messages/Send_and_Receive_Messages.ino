#include <WiFi.h>

// USER DEFINED

int MotorPins[] = {5, 6, 7, 4, 19, 18}; // Array of motor pins
int potPin = 1;      // Potentiometer connected to pin 1
// Variables for filtering potentiometer value
float alpha = 0.5;                // Smoothing factor (0.0 to 1.0, smaller is smoother)
float smoothedPotValue = 0;       // Smoothed potentiometer value
int rawPotValue = 0; // Declare rawPotValue globally

// END USER DEFINED

// Declare the server here
WiFiServer server(80);  // Declare server to handle the connection

char PING[] = "PING";
int loops = 0;

int num_Motors = 6; // Calculate the length of the array
// int Intensity1[num_Motors];
int* Intensity1;  // Declare a pointer for the array
int* Scaled_Intensity1;  // Declare a pointer for the array
int Desired_Motor;  // Declare the Desired_Motor variable

// Call Setup_Wifi() from the WifiSetup.ino file
void setup() {
  Setup_Wifi(server);  // This will initialize the Wi-Fi network and start the server

  // Dynamically allocate memory for Intensity1 based on num_Motors
  Intensity1 = new int[num_Motors];
  Scaled_Intensity1 = new int[num_Motors];

  for (int i = 0; i < num_Motors; i++) {
    pinMode(MotorPins[i], OUTPUT); // Initialize each motor pin as OUTPUT
  }
  // Read the raw potentiometer value
  int rawPotValue = analogRead(potPin);
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

      // Read the raw potentiometer value
      int rawPotValue = analogRead(potPin);
      // Apply exponential moving average for smoothing
      smoothedPotValue = alpha * rawPotValue + (1 - alpha) * smoothedPotValue;

      // Map the smoothed potentiometer value to a multiplier between 0 and 1
      float normalizedValue = smoothedPotValue / 4095.0; // Normalize to 0.0 - 1.0
      float multiplier;

      if (normalizedValue < 0.5) {
          // Map from 0.0-0.5 to 0.2-1.0
          multiplier = 0.2 + normalizedValue * 1.6; // Scale from 0.2 to 1
      } else {
          // Map from 0.5-1.0 to 1.0-11.0
          multiplier = 1.0 + (normalizedValue - 0.5) * 20; // Scale from 1.0 to 11.0
      }

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
            Serial.print(" | Multiplier: ");
            Serial.println(multiplier);
            // Calculate PWM value scaled by the potentiometer multiplier
            Scaled_Intensity1[Desired_Motor] = min(max(int(Intensity1[Desired_Motor] * multiplier), 0), 100); // Ensure within 0-100
            
            Serial.println("Actuating Motor " + String(Desired_Motor) + " on Pin " + String(MotorPins[Desired_Motor])+" with Scaled Intensity:"+ String(Scaled_Intensity1[Desired_Motor]));

            // Call the Run_Motor function
            Run_Motors(MotorPins[Desired_Motor], Scaled_Intensity1[Desired_Motor]);
        } else {
            // If Desired_Motor is out of bounds
            client.println("Error: Invalid motor number");
        }
        // delay(30);
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
