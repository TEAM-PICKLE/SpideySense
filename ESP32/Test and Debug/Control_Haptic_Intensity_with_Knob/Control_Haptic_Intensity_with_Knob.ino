// USER DEFINED
int MotorPins[] = {5, 6, 7, 4, 19, 18}; // Array of motor pins
int num_Motors = 6; // Number of motors
int potPin = 1;      // Potentiometer connected to pin 1

// Variables for filtering potentiometer value
float smoothedPotValue = 0;       // Smoothed potentiometer value
float alpha = 0.5;                // Smoothing factor (0.0 to 1.0, smaller is smoother)

void setup() {
  Serial.begin(115200); // Initialize serial communication for debugging

  for (int i = 0; i < num_Motors; i++) {
    pinMode(MotorPins[i], OUTPUT); // Initialize each motor pin as OUTPUT
    analogWrite(MotorPins[i], 0);  // Set initial PWM values to 0
  }
}

void loop() {
  // Read the raw potentiometer value
  int rawPotValue = analogRead(potPin);

  // Apply exponential moving average for smoothing
  smoothedPotValue = alpha * rawPotValue + (1 - alpha) * smoothedPotValue;

  // Map the smoothed potentiometer value to a multiplier between 0 and 1
  float multiplier = smoothedPotValue / 4095.0; // Scale to 0.0 - 1.0

  Serial.print(" | Multiplier: ");
  Serial.println(multiplier);

  // Fade in and out, scaling PWM values with the multiplier
  for (int i = 0; i < num_Motors; i++) {
    // Calculate PWM value scaled by the potentiometer multiplier
    int scaledPWM = min(max(int(255 * multiplier), 0), 255); // Ensure within 0-255

    Serial.print(" | Motor: "+ String(i)+" PWM: "+ String(scaledPWM));
    // Serial.println(multiplier);

    analogWrite(MotorPins[i], scaledPWM);
    delay(100); // Short delay for each motor
  }
}
