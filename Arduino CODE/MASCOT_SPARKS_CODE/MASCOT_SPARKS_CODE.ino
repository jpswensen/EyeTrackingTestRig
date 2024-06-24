/*
 * Final code that is the product of work performed by Teams MASCOT and SPARKS from Feb-May '21.
 * Team members from MASCOT include: 
 * - Brenno Aguiar Bonfim Cruz - George Eralil - Kyle Butzerin - Mathew Ubachs - Chenhao Yu
 * - Matthew James
 * Work done by MASCOT deals with the eye and shoulder subsystems.
 * Team members from SPARKS include: 
 * - Kaitlyn Lewis - Brittney Downing - Max Larsen - Carson Kreager - Bennet Moore
 * Work done by SPARKS deals with the neck subsystem.
 */

#include "Functions.h"

//HardwareSerial *SerialXbox = &Serial;
//HardwareSerial *SerialTerminal = &Serial1;

void setup() {
  // Set up Connection to Xbox controller, BNO IMU, and laptop's terminal
  Serial.begin(115200); //max value
  Serial1.begin(38400); //arbitraliy different standard value
  // should test 230400 ^
  while (!Serial) {
    ;
  }
  Serial.println("Serial On");
  if(!bno.begin())
  {
   Serial.print("Ooops, no BNO055 detected ... Check your wiring or I2C ADDR!");
  }
  
  bno.setExtCrystalUse(true);   // prepping the IMU
  
  while (!Serial); // Wait for serial port to connect - used on Leonardo, Teensy and other boards with built-in USB CDC serial connection
  if (Usb.Init() == -1) {
    Serial.print(F("\r\nOSC did not start"));
    while (1); //halt
  }
  Serial.print(F("\r\nXBOX USB Library Started"));

  // Initialize Limit Switch input pins
  pinMode(xEnd, INPUT); // x
  pinMode(yEnd, INPUT); // z
  pinMode(zEnd, INPUT); // y
  
  pinMode(8, OUTPUT); 
  digitalWrite(8, LOW); //Servo enable using relay - using that to prevent jitter when arduino starts
  
  // attaching servos to their respective pins.//
  XservoL.attach(xLPin);
  ZservoL.attach(zLPin);
  XservoR.attach(xRPin);
  ZservoR.attach(zRPin);
  NeckServo.attach(NSPin);

  // Loading calibration variables for neck stepper from EEprom
  //ReadCalibrationStepperPosFromProm();
  //ReadLastStepperPosFromProm();

  /* 
   * Set operating parameters for neck stepper motors 
   * stepper one is front
   * stepper two is back right
   * stepper three is back left
   */
  stepperOne.setCurrentPosition(knownstepperposOne); //laststepperposOne); //knownstepperposOne);
  stepperOne.setMaxSpeed(3000); //SPEED = Steps / second 
  stepperOne.setSpeed(900);
  stepperOne.setAcceleration(3000); //ACCELERATION = Steps /(second)^2    
  delay(500);
  stepperTwo.setCurrentPosition(knownstepperposTwo); //laststepperposTwo); //knownstepperposTwo);
  stepperTwo.setMaxSpeed(3000); //SPEED = Steps / second 
  stepperTwo.setSpeed(900);
  stepperTwo.setAcceleration(3000); //ACCELERATION = Steps /(second)^2
  delay(500); 
  stepperThree.setCurrentPosition(knownstepperposThree); //laststepperposThree); //knownstepperposThree); 
  stepperThree.setMaxSpeed(3000); //SPEED = Steps / second 
  stepperThree.setSpeed(900);
  stepperThree.setAcceleration(3000); //ACCELERATION = Steps /(second)^2      
  delay(500);

  // Loading calibration variables for eye servos and shoulder steppers from Prom
  ReadCalibrationVariablesFromProm();

  XservoL.writeMicroseconds(centerLeftXMicroseconds);    // -ve left; +ve right
  ZservoL.writeMicroseconds(centerLeftZMicroseconds);    // -ve up; +ve down
  XservoR.writeMicroseconds(centerRightXMicroseconds);    // -ve left; +ve right
  ZservoR.writeMicroseconds(centerRightZMicroseconds);    // -ve down; +ve up
  NeckServo.write(83);
  
  // Set operating parameters for stepper motors
  stepperY.setMaxSpeed(10000); //SPEED = Steps / second
  stepperY.setAcceleration(1000); //ACCELERATION = Steps /(second)^2
  delay(500);
  stepperX.setMaxSpeed(10000); //SPEED = Steps / second
  stepperX.setAcceleration(1000); //ACCELERATION = Steps /(second)^2
  delay(500);
  stepperZ.setMaxSpeed(10000); //SPEED = Steps / second
  stepperZ.setAcceleration(1000); //ACCELERATION = Steps /(second)^2
  delay(500);
  
  InitialValues(); //averaging the values of the 3 analog pins (values from potmeters)
}


int whichMotor = 0;

void (*runState[12])() = {
  /* 0*/runMenuModeState,
  /* 1*/runServoCalibrationState,
  /* 2*/runAutoState,
  /* 3*/runStepperHomeState,
  /* 4*/runStepperManualState,
  /* 5*/runFindCoordinatesState,
  /* 6*/runSetCoordinatesState,
  /* 7*/runServoManualState,
  /* 8*/runNeckCalibrationState,
  /* 9*/runMoveToCalibrationState,
  /*10*/runNeckState,
  /*11*/runSpinnyBoiState
};


void loop() {
  Usb.Task();
  runState[state]();
}
