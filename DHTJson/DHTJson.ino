#include "DHT.h"
#include <ArduinoJson.h>

// define DHT22
#define DHTTYPE DHT22 
// define pin on DHT22
#define DHTPIN 8 

DHT dht(DHTPIN, DHTTYPE);

StaticJsonBuffer<200> jsonBuffer;
JsonObject& root = jsonBuffer.createObject();

void setup() {
  Serial.begin(9600); 
  dht.begin();
}

void loop() {
  delay(2000);

  // Reading temperature or humidity takes about 250 milliseconds!
  // Sensor readings may also be up to 2 seconds 'old' (its a very slow sensor)
  float h = dht.readHumidity();
  // Read temperature as Celsius (the default)
  float t = dht.readTemperature();
  

  // Check if any reads failed and exit early (to try again).
  if (isnan(h) || isnan(t)) {
    Serial.println("Failed to read from DHT sensor!");
    return;
  }

  // Compute heat index in Celsius (isFahreheit = false)
  float hic = dht.computeHeatIndex(t, h, false);

  root["Humidity"] = double_with_n_digits(h, 2);
  root["Temperature"] = double_with_n_digits(t, 2);
  root.printTo(Serial); 
  Serial.println("");
}
