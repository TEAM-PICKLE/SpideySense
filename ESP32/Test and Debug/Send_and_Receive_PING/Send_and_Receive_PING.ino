#include <WiFi.h>

// Declare the server here
WiFiServer server(80);  // Declare server to handle the connection

char PING[] = "PING";
int loops = 0;

// Call Setup_Wifi() from the WifiSetup.ino file
void setup() {
  Setup_Wifi();  // This will initialize the Wi-Fi network and start the server
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

        // Respond based on the message content
        if (receivedMessage == PING) {
          client.println("SPONGE");
          Serial.println("Received " + receivedMessage);
        } else {
          client.println("Received: " + receivedMessage + "hh");
        }
      }

      // **Send outgoing messages**
      client.println("SHello World1E " + String(loops));
      client.println("SHello World2E " + String(loops));
      Serial.println("Printed to client: " + String(loops));
      delay(100);  // Avoid spamming too fast
    }
    
    Serial.println("Client disconnected");
    client.stop(); // Close the connection
  }
}
