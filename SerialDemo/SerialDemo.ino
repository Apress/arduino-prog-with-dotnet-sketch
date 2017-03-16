int led = 13;
int val = 10;
void setup() {
  pinMode(led, OUTPUT);
  Serial.begin(9600);
}

void loop() {
  digitalWrite(led,HIGH);
  Serial.print("val=");
  Serial.println(val);
  delay(1000);

  digitalWrite(led,LOW);
  val++;
  if(val>50)
    val = 10;
}
