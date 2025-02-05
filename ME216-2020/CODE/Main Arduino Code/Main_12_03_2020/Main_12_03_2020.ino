#include"functions.h"

void setup() {

 Serial.begin(115200);
  while (!Serial); // Wait for serial port to connect - used on Leonardo, Teensy and other boards with built-in USB CDC serial connection
  if (Usb.Init() == -1) {
    Serial.print(F("\r\nOSC did not start"));
    while (1); //halt
  }
  Serial.print(F("\r\nXBOX USB Library Started"));


  int xLPin=4;
  int zLPin=5;
  int xRPin=6;
  int zRPin=7;

  bottomScreen=stepperZ.currentPosition();
  topScreen=stepperZ.currentPosition()+5000;
  leftScreen=abs(stepperX.currentPosition());
  rightScreen=leftScreen+5000;

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


pinMode(23,INPUT);
pinMode(25,INPUT);
pinMode(27,INPUT);

pinMode(21,OUTPUT);
digitalWrite(21,LOW);
  XservoL.attach(xLPin);
  ZservoL.attach(zLPin);
  XservoR.attach(xRPin);
  ZservoR.attach(zRPin);
  XservoL.write(XservoL.read());
  ZservoL.write(ZservoL.read());
  XservoR.write(XservoR.read());
  ZservoR.write(ZservoR.read());
//digitalWrite(48,LOW);
  //----------------------------------------------------------------------------  

// Populate Lookup Tables
LookupTables();
}

//LookupTables();


void loop() {

  Usb.Task();

  switch (state)
  {
    case (MENU):
      {
        runMenuState();
        break;
      }
    case (CAL):
      {
        runCalState();
        break;
      }
    case (MANUAL):
      {
        runManualState();
        break;
      }
    case (AUTO):
      {
        runAutoState();
        break;
      }
    case (HOM):
      {
        runHomeState();
        break;
      }
    case (STEPPERS):
      {
        runSteppersState();
        break;
      }
    case (COORDINATES):
      {
      findCoordinates();
      break;
      }
    case (SET_COORDINATES):
    {
      setPos();
      break;
    }
  }
  if (state != MANUAL)
  {
    delay(1);
  }
}
