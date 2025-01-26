// int ledPin = 4;    // LED connected to digital pin 10
// int ledPinlow = 5;    // LED connected to digital pin 10


// void setup() {
//   // declaring LED pin as output
//   pinMode(ledPin, OUTPUT);
//   pinMode(ledPinlow, OUTPUT);
//   analogWrite(ledPinlow, 0);
// }

// void loop() {
//   // fade in from min to max in increments of 5 points:
//   for (int fadeValue = 0 ; fadeValue <= 255; fadeValue += 5) {
//     // sets the value (range from 0 to 255):
//     analogWrite(ledPin, fadeValue);
//     // wait for 30 milliseconds to see the dimming effect
//     delay(50);
//   }

//   // fade out from max to min in increments of 5 points:
//   for (int fadeValue = 255 ; fadeValue >= 0; fadeValue -= 5) {
//     // sets the value (range from 0 to 255):
//     analogWrite(ledPin, fadeValue);
//     // wait for 30 milliseconds to see the dimming effect
//     delay(50);
//   }
// }





// USER DEFINED
int MotorPins[] = {5, 6, 7, 4, 19, 18}; // Array of motor pins
int num_Motors = 6; // Calculate the length of the array
int ledPin = 3;    // LED connected to digital pin 3

void setup() {
  // declaring LED pin as output
  // pinMode(ledPin, OUTPUT);

  for (int i = 0; i < num_Motors; i++) {
    pinMode(MotorPins[i], OUTPUT); // Initialize each motor pin as OUTPUT
    analogWrite(MotorPins[i], 0);
  }
}

void loop() {
  // fade in from min to max in increments of 5 points:
  for (int i = 0; i < num_Motors; i++) {
    int j=i;
  for (int fadeValue = 0 ; fadeValue <= 255; fadeValue += 5) {
    
      analogWrite(MotorPins[j], fadeValue);
    
    // sets the value (range from 0 to 255):
    // analogWrite(ledPin, fadeValue);
    // wait for 30 milliseconds to see the dimming effect
    delay(30);
  }

  // fade out from max to min in increments of 5 points:
  for (int fadeValue = 255 ; fadeValue >= 0; fadeValue -= 5) {
    
      analogWrite(MotorPins[j], fadeValue);
    
    // sets the value (range from 0 to 255):
    // analogWrite(ledPin, fadeValue);
    // wait for 30 milliseconds to see the dimming effect
    delay(30);
  }
  }
  delay(2000);
}
