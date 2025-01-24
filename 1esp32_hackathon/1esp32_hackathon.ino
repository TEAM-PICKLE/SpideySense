#include <WiFi.h>

const char* ssid = "Pickle";
const char* password = "12345678";

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
    client.println("HTTP/1.1 200 OK");
    client.println("Content-Type: text/plain");
    client.println("Connection: close"); // Connection will be closed after this response
    client.println(); // Empty line to indicate the end of headers

    // Send messages to the client
    loops++;
    for (int i = 0; i < 10000000; i++) {  // Limit the number of messages
      client.println("SHello World1E " + String(loops));
      client.println("SHello World2E " + String(loops));
      Serial.println("Printed to client: " + String(loops));
      delay(100);  // Shorter delay for better performance
    }

    Serial.println("Client disconnected");
    client.stop(); // Close the connection
  }
}
