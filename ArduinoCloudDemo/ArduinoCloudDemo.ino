
#include <WiFi101.h>
#include <ArduinoCloud.h>

/////// Wifi Settings ///////
char ssid[] = "ssid";
char pass[] = "ssid key";


// Arduino Cloud settings and credentials
const char userName[]   = "<username>";
const char thingName[] = "<arduino name>";
const char thingId[]   = "<board id>";
const char thingPsw[]  = "<password>";


WiFiSSLClient sslClient;


// build a new object "KCloud"
ArduinoCloudThing KCloud;


void setup() {
  Serial.begin (9600);

  // attempt to connect to WiFi network:
  Serial.print("Attempting to connect to WPA SSID: ");
  Serial.println(ssid);

  while (WiFi.begin(ssid, pass) != WL_CONNECTED) {
    // unsuccessful, retry in 4 seconds
    Serial.print("failed ... ");
    delay(4000);
    Serial.print("retrying ... ");
  }
  Serial.println("connected to wifi");

  KCloud.begin(thingName, userName, thingId, thingPsw, sslClient);
  KCloud.enableDebug();
  // define the properties
  KCloud.addProperty("humidity", FLOAT, R);
  KCloud.addProperty("temperature", FLOAT, R);

  Serial.println("connected");
  randomSeed(analogRead(0));
}

void loop() {
  KCloud.poll();

  long temp = random(10, 20);
  long humidity = random(20, 80);

  temp = temp + 0.21 * temp;
  humidity = humidity + 0.45 * humidity;

  KCloud.writeProperty("humidity", String(humidity,2));
  KCloud.writeProperty("temperature", String(temp,2));
  
  delay(2000);
  
}

