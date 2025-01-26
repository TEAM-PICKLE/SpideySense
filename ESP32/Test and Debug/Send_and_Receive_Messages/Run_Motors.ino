void Run_Motors(int Motor_Pin_num, int Intensity) {
  // Ensure the motor pin is configured as an output
  // pinMode(Motor_num, OUTPUT);

  // Scale Intensity (0-100) to PWM range (0-255)
  int pwmValue = (Intensity * 255) / 100;

  // Write the PWM value to the motor pin
  analogWrite(Motor_Pin_num, pwmValue);
}
