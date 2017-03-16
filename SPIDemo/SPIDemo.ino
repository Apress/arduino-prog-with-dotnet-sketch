#include <SPI.h>

byte sendData,recvData;
void setup() {  
  SPI.begin();
  Serial.begin(9600);

}

// source:
// http://forum.arduino.cc/index.php?topic=197633.0
byte randomDigit() {
  unsigned long t = micros();
  byte r = (t % 10) + 1;
  for (byte i = 1; i <= 4; i++) {
    t /= 10;
    r *= ((t % 10) + 1);
    r %= 11;
  }
  return (r - 1);
}

void loop() {
  sendData = randomDigit();
  recvData = SPI.transfer(sendData);

  Serial.print("Send=");
  Serial.println(sendData,DEC);
  Serial.print("Recv=");
  Serial.println(recvData,DEC);
  delay(800);
}
