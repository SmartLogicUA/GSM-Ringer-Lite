/*
  CodeVisionAVR C Compiler
  (C) 2007 Taras Drozdovsky, My.
*/

#ifndef _SIM300_INCLUDED_
#define _SIM300_INCLUDED_   

#define AT          "AT"
#define CMGDA       "AT+CMGDA=\"DEL ALL\""
#define CNMI        "AT+CNMI=2,1,0,0,0"
#define CMIC        "AT+CMIC=0,15"
#define CSMP        "AT+CSMP=17,169,0,0"
#define ATE0        "ATE0"
#define CMGF        "AT+CMGF=1"
#define ATE0V1      "ATE0V1"
#define CFUN        "AT+CFUN=1"
#define CUSD        "AT+CUSD=1"
#define CREG        "AT+CREG=1"
#define ATA         "ATA"        
#define ACT_        "AT+VTS=3;+VTS=3;+VTS=3"
#define DISACT_     "AT+VTS=3"


#define SIZE_GSM_RX_BUFFER  200
unsigned char gsm_rx_buffer[SIZE_GSM_RX_BUFFER];    
unsigned char gsm_rx_counter=0; 

unsigned char status_sim=0;
struct {
        unsigned char dir;
        unsigned char stat;
        unsigned char status;
        unsigned char number[14];
       }S_CLCC;

bit GSM_0D=0;
bit GSM_0A=0;
bit GSM_PACK=0;

#pragma used+

void SimPowerOn(void);                
void SimPowerOff(void);                
void SimRst(void);  
void Receive_gsm(void);
unsigned char HandlerEventGSM(void);            
unsigned char C_SendSimpleCommand(flash unsigned char *,unsigned int);
unsigned char C_ATH(void);  
unsigned char C_ATDD(unsigned char *);
unsigned char C_CMGLU(unsigned char *);
unsigned char SendData(char *);
unsigned char C_CLCC(void);  
unsigned char C_USSD(unsigned char *);
unsigned char C_CREGG(void);
unsigned char C_CGATT(void);
void SendCommand(flash unsigned char *);
void Send_textf(flash char *);
unsigned char SendSMS(char *,char *);
unsigned char SendSMSU(flash char *,flash char *);

#pragma used-
#endif
