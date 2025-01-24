#include <WiFi.h>

// Declare the server here to avoid duplication
extern WiFiServer server;  // Declare the server to be used by other files

// Function to set up the Wi-Fi Access Point
void Setup_Wifi() {
  const char* ssid = "Pickle";
  const char* password = "12345678";

  Serial.begin(115200);

  // Create the access point with SSID and password
  WiFi.softAP(ssid, password);
  Serial.println("Access Point Started");
  Serial.print("IP Address: ");
  Serial.println(WiFi.softAPIP());

  // Start the server
  server.begin();
}
