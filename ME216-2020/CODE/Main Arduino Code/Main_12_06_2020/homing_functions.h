
void zeroStepper(int dir, int pulse, int limm)//direction pin, pulse pin, limit pin
{
  limit=digitalRead(limm);
  Serial.println(limit);
  while(limit==HIGH) //Moves slowly towards the limit switch
  {
  limit=digitalRead(limm);
  //Serial.println(limit);
  if (limm==25)
  {
     digitalWrite(dir,LOW);
  }
  else
  {
  digitalWrite(dir,HIGH);
  }
  digitalWrite(pulse,HIGH);
  delayMicroseconds(400);
  digitalWrite(pulse,LOW);
  delayMicroseconds(400);
  }
  Serial.println(limit);

    i=0;

      if (limm==25)
  {
     digitalWrite(dir,HIGH);
  }
  else
  {
  digitalWrite(dir,LOW);
  }
    //digitalWrite(dir,LOW);
    while(i<1000) // Back up 1000 steps
    {
    digitalWrite(pulse,HIGH);
    delayMicroseconds(400);
    digitalWrite(pulse,LOW);
    delayMicroseconds(400);
    i++;
    }
   

    limit=digitalRead(limm);
    while(limit==HIGH) // Move towards limit slowly until limit trips
    {
        if (limm==25)
  {
     digitalWrite(dir,LOW);
  }
  else
  {
  digitalWrite(dir,HIGH);
  }
    //digitalWrite(dir,HIGH);
    digitalWrite(pulse,HIGH);
    delayMicroseconds(800);
    digitalWrite(pulse,LOW);
    delayMicroseconds(800);
    limit=digitalRead(limm);
    }
    
  if (limm==25)
  {
     digitalWrite(dir,HIGH);
  }
  else
  {
  digitalWrite(dir,LOW);
  }
     //digitalWrite(dir,LOW);
    i=0;
    while(i<500) // Back up 500 steps
    {
    digitalWrite(pulse,HIGH);
    delayMicroseconds(800);
    digitalWrite(pulse,LOW);
    delayMicroseconds(800);
    i++;
    }
}


