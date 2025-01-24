#include <WiFi.h>

const char* ssid = "Pickle";
const char* password = "12345678";
char PING[] = "PING";
WiFiServer server(80);
int loops = 0;

void setup() {
  Serial.begin(115200);
  
  // Create Access Point
  WiFi.softAP(ssid, password);
  Serial.println("Access Point Started");
  Serial.print("IP Address: ");
  Serial.println(WiFi.softAPIP());
  
  server.begin();
}

void loop() {
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
          client.println("Received: " + receivedMessage+"hh");
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
