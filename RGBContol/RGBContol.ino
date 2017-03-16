#include <ArduinoJson.h>

DynamicJsonBuffer jsonBuffer;
String data;

int redPin = 9;
int greenPin = 10;
int bluePin = 11;

void setup() {
  pinMode(redPin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  pinMode(bluePin, OUTPUT);
  Serial.begin(9600); 
  data = "";
}

void loop() {
  if (Serial.available() > 0) {    
    char inputData = Serial.read();
    data += (char)inputData;
    Serial.println(data);
    //data = "{\"R\":24,\"G\":18,\"B\":19}";
    if(inputData!='}')
      return;
    JsonObject& root = jsonBuffer.parseObject(data);
  /*  if (!root.success())
    {
      //Serial.println(data);
      //Serial.println("parseObject() failed");
      return;
    } */
    Serial.println(data);
    int r = root[String("R")];
    int g = root[String("G")];
    int b = root[String("B")];
    setColor(r, g, b);
    data = "";
  }
    
}
void setColor(int red, int green, int blue)
{
  analogWrite(redPin, red);
  analogWrite(greenPin, green);
  analogWrite(bluePin, blue);
}
