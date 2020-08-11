//#include<LiquidCrystal.h>
//#define lights A5

//LiquidCrystal lcd(1,2,4,5,6,7);
const unsigned long rebeat= 6000;
unsigned long pre =0;

int digital_static_pin[6]={2,4,7,8,12,13};
int digital_var_pin[5]={3,5,9,10,11};
int analog_pin[6]={0,1,2,3,4,5};

//int pin_st_status[6]={0,0,0,0,0,0};
//int pin_var_status[5]={0,0,0,0,0};
//int alg_status[6] ={0,0,0,0,0,0};

int digital_static_pin_con[6]={0,0,0,0,0,0};//{1,1,1,1,1,1};
int digital_var_pin_con[5]={0,0,0,0,0};//{1,1,1,1,1};
int analog_pin_con[6]={0,0,0,0,0,0};


//==========================================================
String get_status()
    {
     //////////// -1=no connected device  /// 0= there is a connected device but it is off //// 1= there is a connected device and it is on
           String msg="@";int x;
             for(int i=0;i<5;i++)
                {
                  x=-1; 
                  if(digital_static_pin_con[i]==1)   
                    x=digitalRead(digital_static_pin[i]);
                  //  pin_st_status[i]=x;
                  msg=msg+x+",";
                }
             x=-1;
            if(digital_static_pin_con[5]==1)  
              x=digitalRead(digital_static_pin[5]);
             //  pin_st_status[5]=x;
            msg=msg+x+"@";
           //------------------------------------------
            for(int i=0;i<4;i++)
              {
                 x=-1;
                 if(digital_var_pin_con[i]==1)
                   x=digitalRead(digital_var_pin[i]);
                  //  pin_var_status[i]=x;
                 msg =msg+x+",";
              }
              x=-1;
              if(digital_var_pin_con[4]==1)
                x=digitalRead(digital_var_pin[4]);
                 //pin_var_status[4]=x;
             msg=msg+x+"@";
          //------------------------------------------
           for(int i=0;i<5;i++)
            { 
              x=-1;
              if (analog_pin_con[i]==1) 
                x=analogRead(analog_pin[i]);
                //alg_status[i]=x;
              msg =msg+x+",";
            }
            x=-1; 
            if(analog_pin_con[5]==1) 
              x=analogRead(analog_pin[5]);
              //alg_status[5]=x;
            msg=msg+x+"@";
            
    return msg;
}
//========================================================================

int retrn_pin_num(int pin_id)
{
  
   for(int i=0;i<6;i++)
                {
                
                  if(digital_static_pin[i]==pin_id)   
                  { return i; break;}
                   
                   else if(digital_var_pin[i]==pin_id)   
                   { return i; break;}
                   
                    else if(analog_pin[i]==pin_id)   
                   { return i; break;}
                }
 }
//=======================================================================

void set_on_off(int pin_id,int op)
{
  int x=retrn_pin_num(pin_id);
 
    if(digital_static_pin_con[x]==1||digital_var_pin_con[x]==1)
    { 
      if(op==1)
       digitalWrite(pin_id,HIGH); 
      else
       digitalWrite(pin_id,LOW);
     }
}

void set_fan(int pin_id,int op)
{
  int x=retrn_pin_num(pin_id);
 
    if(digital_static_pin_con[x]==1||digital_var_pin_con[x]==1)
    { 
      
       digitalWrite(pin_id,op); 
     
       digitalWrite(pin_id,op);
     }
}
//========================================================================


void add_device(int pin_id,int type)
{
     if(type==0)
     {
      for(int i=0; i<6; i++)
         if(digital_static_pin[i]==pin_id)
           digital_static_pin_con[i]=1;
     }
 
     else if(type==1)
     {
     for(int i=0; i<5; i++)
         if(digital_var_pin[i]==pin_id)
           digital_var_pin_con[i]=1;
     }
    else if(type==2){
      for(int i=0; i<6; i++)
         if(analog_pin[i]==pin_id)
           analog_pin_con[i]=1;
     
    }
}
//=======================================================================
void remov_device(int pin_id,int type)
{

 if(type==0)
     {
      for(int i=0; i<6; i++)
         if(digital_static_pin[i]==pin_id )
          { digital_static_pin_con[i]=0;
          digitalWrite(pin_id,LOW);}
     }
 
     else if(type==1)
     {
     for(int i=0; i<5; i++)
         if(digital_var_pin[i]==pin_id)
           {digital_var_pin_con[i]=0;
           digitalWrite(pin_id,LOW);}
     }
    else if(type==2){
      for(int i=0; i<6; i++)
         if(analog_pin[i]==pin_id)
           analog_pin_con[i]=0;
     
    }
}
//=======================================================================
void setup() {
pinMode(2, OUTPUT);
pinMode(3, OUTPUT);
pinMode(4, OUTPUT);
pinMode(5, OUTPUT);
pinMode(6, OUTPUT);
pinMode(7, OUTPUT);
pinMode(8, OUTPUT);
pinMode(9, OUTPUT);
pinMode(10, OUTPUT);
pinMode(11, OUTPUT);
pinMode(12, OUTPUT);
pinMode(13, OUTPUT);
pinMode(A0, INPUT);
pinMode(A1, INPUT);
pinMode(A2, INPUT);
pinMode(A3, INPUT);
pinMode(A4, INPUT);
pinMode(A5, INPUT);
Serial.begin(9600); 
}



void loop() {
   unsigned long currentTime=millis();
             if(currentTime-pre>=rebeat)
             {String x=get_status();  
               Serial.println(x);
               pre=currentTime;}
              else{
                if (Serial.available() > 1)
                  {
                      String func=Serial.readStringUntil('.');
                      int  int_func=func.toInt();////1=OnOff///2=AddDevice///3=RemoveDevice
                      String pin = Serial.readStringUntil('.');
                      int  int_pin=pin.toInt();
                       if(int_pin>=20) /// analog array 20,21 to 0,1
                       { int_pin=int_pin-20; }//analog=true;Serial.println("A");Serial.println(int_pin);}
                        String state = Serial.readStringUntil('.'); /// state for < on and off >fun && array type for < add device > fun
                        int int_state=state.toInt();
                      if(int_func==1)
                         set_on_off(int_pin,int_state);
            
                     else if (int_func==2)
                       add_device(int_pin,int_state); // type
              
                     else if (int_func==3)
                      remov_device(int_pin,int_state); // type
                      
                    else if(int_func=4)  
                     set_fan(int_pin,int_state);
                    
                }
                 delay(1000);

    }
 

}





