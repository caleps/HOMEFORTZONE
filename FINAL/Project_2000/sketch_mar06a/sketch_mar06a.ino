#include <SoftwareSerial.h>// import the serial library

SoftwareSerial mySerial(10,11); // RX, TX
 
void setup() {
    Serial.begin(9600); 
    delay(2000);  
 
    Serial.println("Type something!");
    pinMode(2,OUTPUT);
}
 
void loop() {
    if(mySerial.available()){
       
                  String  button=mySerial.readStringUntil('.');
        if(button=="1")
        digitalWrite(2,HIGH);
        Serial.print("You typed: " );
        Serial.println(button);
    }
}
