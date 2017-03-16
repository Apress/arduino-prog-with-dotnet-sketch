int redPin = 9;
int greenPin = 10;
int bluePin = 11;


void setup()
{
    pinMode(redPin, OUTPUT);
    pinMode(greenPin, OUTPUT);
    pinMode(bluePin, OUTPUT);
    Serial.begin(9600);
}

void loop()
{
  setColor(0, 255, 255);  // red
  Serial.println("red");
  delay(1000);
  setColor(255, 0, 255);  // green
  Serial.println("green");
  delay(1000);
  setColor(255, 255, 0);  // blue
  Serial.println("blue");
  delay(1000);
  setColor(0, 0, 255);  // yellow
  Serial.println("yellow");
  delay(1000);
  setColor(80, 255, 80);  // purple
  Serial.println("purple");
  delay(1000);
  setColor(255, 0, 0);  // aqua
  Serial.println("aqua");
  delay(1000);
}

void setColor(int red, int green, int blue)
{
  analogWrite(redPin, red);
  analogWrite(greenPin, green);
  analogWrite(bluePin, blue);
}

