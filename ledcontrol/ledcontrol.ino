int led1 = 12;
int led2 = 11;
int led3 = 10;

void setup() {                 
  pinMode(led1, OUTPUT);     
  pinMode(led2, OUTPUT);     
  pinMode(led3, OUTPUT);    
  Serial.begin(9600);
}

void loop() {

  if (Serial.available() > 0) {
    
    char inputData = Serial.read();
    if(inputData=='1'){
      digitalWrite(led1, HIGH);
      digitalWrite(led2, LOW);
      digitalWrite(led3, LOW);
    }      
    if(inputData=='2'){
      digitalWrite(led1, LOW);
      digitalWrite(led2, HIGH);
      digitalWrite(led3, LOW);
    }
    if(inputData=='3'){
      digitalWrite(led1, LOW);
      digitalWrite(led2, LOW);
      digitalWrite(led3, HIGH);
    }
  
  }
  
}
