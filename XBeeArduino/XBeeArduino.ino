long randNumber;
void setup() { 
  Serial.begin(9600);
  randomSeed(analogRead(0));
}

void loop() {
  randNumber = random(20, 80);
  Serial.println(randNumber);
  delay(2000);
}

