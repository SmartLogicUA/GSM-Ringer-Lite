/*
  CodeVisionAVR C Compiler
  (C) 2007 Taras Drozdovsky, My.
*/
#include <stdio.h>
#include <board.h>

#define ATH         "ATH0"
#define CMGR        "AT+CMGR=1"

//Declare string AT-command            

#define CMGS        "AT+CMGS=\"+38"
#define CMGD        "AT+CMGD=1"
#define CCLKS       "AT+CCLK=\""
#define CCLKG       "AT+CCLK?"
#define CLCC        "AT+CLCC"
#define CREGG       "AT+CREG?"
#define VTS         "AT"
#define ATDD        "ATD+38"          
#define ATD         "ATD"          
#define CUSD_       "+CUSD:"
#define CLVL        "AT+CLVL=70"
#define CSCA        "AT+CSCA?"
#define CSCS        "AT+CSCS=\"UCS2\""
#define CMGLU       "AT+CMGL=\"REC UNREAD\""
// Receive message from telephone 
#define RING        "RING"
#define CMTI        "+CMTI: \"SM\","
#define CMGS_       "+CMGS:"
#define CREG_N1     "+CREG: 1,0"
#define CREG_N2     "+CREG: 1,2"
#define CREG_Y      "+CREG: 1,1"
#define NOCARRIER   "NO CARRIER"
#define NOANSWER    "NO ANSWER"
#define BUSY        "BUSY"
#define OK          "OK"
#define ERROR       "ERROR"
#define CONNECT9600 "CONNECT 9600"
#define SIMREADY    "Call Ready" 
#define CIPSEND     "AT+CIPSEND"
#define CIPCLOSE    "AT+CIPCLOSE"
#define CIPSHUT     "AT+CIPSHUT"

#define CGATT       "AT+CGATT=1"

#define CGREGQ      "AT+CGREG?"
#define CGREG10     "+CGREG: 1,0"
#define CGREG11     "+CGREG: 1,1"
#define CGREG0      "+CGREG: 0"
#define CGREG1      "+CGREG: 1"

#define CONNECTOK   "CONNECT OK"
#define CONNECTFAIL "CONNECT FAIL"
#define SENDOK      "SEND OK"
#define SENDFAIL    "SEND FAIL"
#define SHUTOK      "SHUT OK";

/**
 * On power Sim300 
 **/
void SimPowerOn(void)
{
 #asm("wdr")  
 if(!VDD_EXT)
 {
  DDRD.5=1;
  delay_ms(2000);
  DDRD.5=0;
  while(!VDD_EXT)
   #asm("wdr");
 }
}

/**
 * Off power Sim300 
 **/
void SimPowerOff(void)
{
 #asm("wdr")  
 if(VDD_EXT)
 {  
    DDRD.5=1;
    delay_ms(700);
    DDRD.5=0;
    while(VDD_EXT)
    #asm("wdr");
 }
 else
 {
    delay_ms(3000);
 } 
}

/**
 * Reset Sim300  
 **/
void SimRst(void)
{           
 #asm("wdr")     
 UCSR0B=0x00;
 SimPowerOff();
 delay_ms(2000);
 SimPowerOn();
 UCSR0B=0xD8;
 SendCommand("AT");
 delay_ms(1000);        
 SendCommand("AT");        
}

/**
 * Send command PIML with Flash memory string.  
 *
 * @param	*data	a pointer to the string command 
 **/
void SendCommand(flash unsigned char *data){
 unsigned char i=0;
  do {     
   #asm("wdr")
   putcharGSM(data[i]);
  }while(data[++i]);
  putcharGSM(0x0D);  
}
/**
 * Send text PIML with Flash memory string.  
 *
 * @param	*data	a pointer to the string text 
 **/
void Send_textf(flash char *data){
 unsigned char i=0;
  do {        
   #asm("wdr")
   putcharGSM(data[i]);
  }while(data[++i]);
}                            

/**
 * Hendler events from SIM.  
 *
 * @return	0	event processing
 * @return	1	event not processing
 **/
unsigned char HandlerEventGSM(void)
{             
    #asm("wdr")      
    if((strcmpf(&gsm_rx_buffer[0],RING)==0))       // Receive RING
    {          
        status_sim|=S_RING;
        GSM_PACK=0;  
        return 0;
    }
    if((strstrf(&gsm_rx_buffer[0],CMTI)!=NULL))     // Receive SMS  
    {    
        status_sim|=S_SMS;         
        GSM_PACK=0;
        return 0;     
    }    
//     if((strcmpf(&gsm_rx_buffer[0],ERROR)==0))      // Receive ERROR
//     {          
//         status_sim|=S_ERROR;         
//         GSM_PACK=0;
//         return 0;
//     } 
    if((strcmpf(&gsm_rx_buffer[0],SIMREADY)==0))         // Receive Call Ready
    {
        status_sim|=S_CRDY;              
        GSM_PACK=0;          
        return 0;
    }                        
    if((strcmpf(&gsm_rx_buffer[0],"+CREG: 1")==0))       // Network register
    {
        status_sim|=S_CREG;              
        GSM_PACK=0;          
        return 0;
    }                            
    if((strcmpf(&gsm_rx_buffer[0],"+CREG: 0")==0))       // Not network register
    {
        status_sim|=S_CREGNOT;              
        GSM_PACK=0;          
        return 0;
    }                            
    if((strcmpf(&gsm_rx_buffer[0],"+CREG: 2")==0))       // Not network register
    {
        status_sim|=S_CREGNOT;              
        GSM_PACK=0;          
        return 0;
    }                            
    GSM_PACK=0;
    return 1;
}                          
/**
 * Handler event as "CLCC".  
 *
 * @return	OK	if number register in base 
 **/
unsigned char C_CLCC(void)
{     
    unsigned char i=0,j=0;
    #asm("wdr")
    SendCommand(CLCC);
    while(tx_counterGSM);
    CounterGSM=100;
    S_CLCC.status=0;
    while((!GSM_PACK)&&(CounterGSM))
        Receive_gsm();
    if(!GSM_PACK)
        return 1;         
    if(strstrf(&gsm_rx_buffer[0],"+CLCC:")!=NULL)
    {
        while(gsm_rx_buffer[i++]!=',');
        while(gsm_rx_buffer[i++]!=',');
        S_CLCC.stat=gsm_rx_buffer[i];  
        while(gsm_rx_buffer[i++]!='"');
        while(gsm_rx_buffer[i]!='"')
            S_CLCC.number[j++]=gsm_rx_buffer[i++];
        S_CLCC.number[j]=0;
        S_CLCC.status=1;
        GSM_PACK=0;
        gsm_rx_counter=0;
        CounterGSM=100;
        while((!GSM_PACK)&&(CounterGSM))
            Receive_gsm();
        if(!GSM_PACK)
            return 1;         
    }              
    if((strcmpf(&gsm_rx_buffer[0],OK))==0)
    {
        GSM_PACK=0;
        gsm_rx_counter=0;
        return 0;
    }
    return 1;                              
}            
/**
 * hund-up current Call  
 *
 * @return	OK	hand-up Call disconnected
 * @return	ERROR	hand_up ERROR
 **/ 
unsigned char C_ATH(void)
{
    #asm("wdr")
    SendCommand(ATH);
    while(tx_counterGSM);
    CounterGSM=100;
    while((!GSM_PACK)&&(CounterGSM))
        Receive_gsm();
    if(!GSM_PACK)
        return 1;         
    if ((strcmpf(&gsm_rx_buffer[0],"NO CARRIER")==0))      // No Carrier  
    { 
        GSM_PACK=0;
        CounterGSM=100;
        while((!GSM_PACK)&&(CounterGSM))
            Receive_gsm();
        if(!GSM_PACK)
            return 1;         
    }
    if((strcmpf(&gsm_rx_buffer[0],OK))==0)
    {
        GSM_PACK=0;
        return 0; 
    }  
    return 1;             
}   


/**
 * Call accepted 
 *
 * @return	OK	Call accepted 
 * @return	ERROR	ERROR
 **/ 
unsigned char C_ATDD(unsigned char *mas)
{
  unsigned char i;
  #asm("wdr")
  Send_textf(ATDD);
  for(i=0;i!=10;i++)
   putcharGSM(mas[i]);
  putcharGSM(';');   
  putcharGSM(0x0D); 
  while(tx_counterGSM);
  CounterGSM=100;
  while((!GSM_PACK)&&(CounterGSM))
   Receive_gsm();
  if(!GSM_PACK)return 1;         
  if((strcmpf(&gsm_rx_buffer[0],OK))==0)
  {
   GSM_PACK=0;
   return 0; 
  }  
  return 1;             
}    

/**
 * Universal Command 
 *
 * @return	0	OK 
 * @return	1	ERROR or other answer
 **/   
unsigned char C_SendSimpleCommand(flash unsigned char *mas,unsigned int value_counter_GSM){ 
  #asm("wdr")
  SendCommand(mas);
  while(tx_counterGSM);
  CounterGSM=value_counter_GSM;
  while((!GSM_PACK)&&(CounterGSM))
   Receive_gsm();          
  if(!GSM_PACK)
   return 1;         
  if((strcmpf(&gsm_rx_buffer[0],OK))==0)
  {
   GSM_PACK=0;
   return 0; 
  }  
  return 1;              
}      

/**
 * Delete all SMS message 
 *
 * @return	OK	delete successfull 
 * @return	ERROR	ERROR
 **/   
unsigned char C_CMGLU(unsigned char *mas)
{
    #asm("wdr")
    SendCommand(CMGLU);
    while(tx_counterGSM);
    CounterGSM=1000;
    while((!GSM_PACK)&&(CounterGSM))
        Receive_gsm();          
    if(!GSM_PACK)
        return 1;       
    GSM_PACK=0;   
    gsm_rx_counter=0;
    CounterGSM=1000;
    while((!GSM_PACK)&&(CounterGSM))
        Receive_gsm();          
    if(!GSM_PACK)
        return 1;         
    if((strcmpf(&gsm_rx_buffer[0],OK))==0)
    { 
        GSM_PACK=0;   
        return 0; 
    }                       
    else
        strcpy(&mas[0],&gsm_rx_buffer[0]);
    return 1;
}      
                               
/*     
 * Send SMS
 * @param	*massage      a pointer to the string in RAM   
 * @param	*number	a pointer to the string in RAM number phone abonent  
 *
 * @return	OK	massage send
 * @return	ERROR	error
 */   
unsigned char SendSMS(char *massage, char *number)
{
    unsigned char i=0;         
    
    Send_textf(CMGS); 
    do
    {  
        #asm("wdr")
        putcharGSM(number[i]);
    }while(number[++i]);
    putcharGSM('"');
    putcharGSM(0x0D);              
    while(tx_counterGSM);
    CounterGSM=600;
    while((getcharGSM()!='>')&&(CounterGSM))
    #asm("wdr");
    while((getcharGSM()!=' ')&&(CounterGSM))
    #asm("wdr");
    if(!CounterGSM)
        return 1;
    i=0;
    do
    {  
        #asm("wdr")
        putcharGSM(massage[i]);
    }while(massage[++i]);
    putcharGSM(26);
    gsm_rx_counter=0;
    GSM_PACK=0; 
    #asm("cli")
    CounterGSM=2000;
    #asm("sei")
    #asm("wdr")
    while((!GSM_PACK)&&(CounterGSM))
        Receive_gsm();
    if(!GSM_PACK)
        return 1;
    if((strstrf(&gsm_rx_buffer[0],CMGS_)!=NULL))
    {              
        GSM_PACK=0;
        #asm("wdr")
        CounterGSM=500;
        while((!GSM_PACK)&&(CounterGSM))
            Receive_gsm();
        if(!GSM_PACK)
            return 1;
        if((strcmpf(&gsm_rx_buffer[0],OK))==0)
        {
            GSM_PACK=0;
            return 0; 
        }
    }                         
    return 1; 
}      
                                                                            
/*     
 * Check network registration
 *
 * @eturn	C_REG_NET	registered, home network
 * @return	ERROR	error
 */   
unsigned char C_CREGG(void)
{ 
    #asm("wdr")
    SendCommand(CREGG);
    while(tx_counterGSM)
    #asm("wdr");
    CounterGSM=100;
    while((!GSM_PACK)&&(CounterGSM))
        Receive_gsm();
    if(!GSM_PACK)
        return 0;
    if((strcmpf(&gsm_rx_buffer[0],CREG_Y))==0)
    {
        GSM_PACK=0;
        CounterGSM=100;
        while((!GSM_PACK)&&(CounterGSM))
            Receive_gsm();
        if(!GSM_PACK)
            return 0;   
        if((strcmpf(&gsm_rx_buffer[0],OK))==0)
        {
            GSM_PACK=0;
            return C_REG_NET; 
        }   
    } 
    if((strcmpf(&gsm_rx_buffer[0],CREG_N1)==0)||(strcmpf(&gsm_rx_buffer[0],CREG_N2)==0))
    {
        GSM_PACK=0;
        CounterGSM=100;
        while((!GSM_PACK)&&(CounterGSM))
            Receive_gsm();
        if(!GSM_PACK)
            return 0;
        if((strcmpf(&gsm_rx_buffer[0],OK))==0)
        {
            GSM_PACK=0;
            return C_NOT_REG_NET; 
        }
        return 0;  
    }  
    return 0;              
}      
                                             
/*     
 * Receive byte from Sim300 and shaping packet
 */  
void Receive_gsm(void)
{
    unsigned char data;
    #asm("wdr")              
    if(rx_counterGSM)
    {
        data=getcharGSM();
        #ifdef DEBUG
        putcharPC(data);
        #endif
        switch (data) {
            case 0x0D: if(gsm_rx_counter){gsm_rx_buffer[gsm_rx_counter]=0; GSM_PACK=1;}
            break;
            case 0x0A: gsm_rx_counter=0;
            break;   
            default:
            {
                gsm_rx_buffer[gsm_rx_counter++]=data;
                if(gsm_rx_counter==200) gsm_rx_counter=0;
            }
        };
    } 
}

/**
 * USSD 
 *
 * @return	OK	Call accepted 
 * @return	ERROR	ERROR
 **/ 
unsigned char C_USSD(unsigned char *mas){
  unsigned char i=0;
  #asm("wdr")
  Send_textf(ATD);
  do {     
   #asm("wdr")       
   if(mas[i]=='@') putcharGSM('*');
   else putcharGSM(mas[i]);
  }while(mas[++i]);
  putcharGSM(0x0D);  
  while(tx_counterGSM);
  CounterGSM=2000;
  while((!GSM_PACK)&&(CounterGSM))
   Receive_gsm();
  if(!GSM_PACK)return 1;         
  if((strstrf(&gsm_rx_buffer[0],CUSD_))!=NULL)
  {            
   i=10;
   do {     
   #asm("wdr")
   if(gsm_rx_buffer[i]=='*') gsm_rx_buffer[i]='@';
   if(gsm_rx_buffer[i]=='"') gsm_rx_buffer[i+1]=0;
  }while(gsm_rx_buffer[++i]);
   gsm_rx_buffer[3]='S';   
   gsm_rx_buffer[4]='Y';   
   gsm_rx_buffer[5]='U';   
   gsm_rx_buffer[6]='S';   
   gsm_rx_buffer[7]='D';   
   gsm_rx_buffer[8]=' ';   
   SendAnswerR(&gsm_rx_buffer[3]);
   GSM_PACK=0; 
   return 0; 
  }
  SendAnswerF("SYUSD ERROR");  
  return 1;             
}    
           