/*****************************************************
This program was produced by the
CodeWizardAVR V1.25.3 Professional
Automatic Program Generator
© Copyright 1998-2007 Pavel Haiduc, HP InfoTech s.r.l.
http://www.hpinfotech.com

Project : GSM Ringer Lite (USB)
Version : 1.0.1
Date    : 11.11.2008
Author  : T.Drozdovsky
Company : Smart Logic                              
Comments: 

Chip type           : ATmega168
Program type        : Application
Clock frequency     : 7,372800 MHz
Memory model        : Small
External SRAM size  : 0
Data Stack size     : 256
*****************************************************/

#include <mega168.h>                                             
#include <board.h>                                
#include <sim300.h>
#include <delay.h>                     
#include <string.h>
#include <stdlib.h>
#include <ctype.h> 

eeprom unsigned char *pEEPROM;

#define DELAY_C         45                               // время в сек. на исходящие соединение
#define DELAY_CC        120                              // время в сек. на входящее соединение
#define DELAYMC240      10                               // количество стробов на АЦП

#define NUMCALL         5                                // количество попыток дозвона
#define AMOUNT_ATTEMPT_SMS  10                           // количество попыток отправки SMS

#define F_SETACTIVEZONE     "SYSAZ OK"                   // зоны активированы
#define F_SETUSERNUMBER     "SYSUN OK"                   // номер установлен
#define F_SETTIMEACTIVE     "SYSTA OK"                   // время активации установлено
#define F_SETTIMEDISACTIVE  "SYSTD OK"                   // время деактивации установлено
#define F_SETCONFIGURATION  "SYSCS OK"                   // конфигурация установлена
#define F_COMMANDNOTSUPPORT "SYCOMMAND NOT SUPPORT"      // команда не поддерживается
#define F_VERSIA            "SYVER GSM Ringer Lite v1.0.1" // версия програмного обеспечения

flash char tascii[]={"0123456789ABCDEF"};                // таблица ASCII символов
flash char str_zone[4][7]={" Zone1"," Zone2"," Zone3"," Zone4"}; 
              
// Declare variable setting in EEPROM and initial value             
eeprom unsigned char SETSE=0x0A;                        // время активации системы
eeprom unsigned char SMSE=0x00;                         // параметры оповещения по SMS
eeprom unsigned char GETSE=0x0A;                        // время деактивации системы
eeprom unsigned char CALE=0x00;                         // параметры оповещиния по Call
eeprom unsigned char ZoneMaskE=0x0F;                    // маска зон
eeprom unsigned char ZonaE=0;                           // зоны которые сработали
eeprom unsigned char sign_E=0;                          
eeprom unsigned char NUMAE[NUMA_NUMBER][11]={"0000000000",             // номера дозвонов
                                             "0000000000",
                                             "0000000000",
                                             "0000000000",
                                             "0000000000"};              
eeprom unsigned char PWDE[]="000000";

#define MIN_VOLTAGE     0x7F
#define MAX_VOLTAGE     0xBD

// Declare your variables in RAM here
register unsigned int  CounterGSM=0;                    // счетчик тиков для модема
register unsigned char ZonaS=0;                         // текущее состояние зон
register unsigned char TimeGuard=0;
unsigned char counter_PC=0;                             // счетчик принятых байт от компьютера
unsigned char buffer_PC[64];                            // прийомный буфер пакетов от компьютера

unsigned char SETSR=0;                                  // время для активации системы   
unsigned char GETSR;                                    // время на дективацию системы 
unsigned char NUMAR[NUMA_NUMBER][11];                   // номера абонентов
unsigned char rdy_counter=25;                           // время ожидания CRDY
unsigned char work_modem=120;                           // время до подачи команды АТ
unsigned char ZoneMask;                                 // маска рабочих зон
unsigned int  gsm_counter_AC=0;                         // время активного звонка
unsigned char StatusCall=0;                             // состояние дозвона абонентам
unsigned char StatusSMS=0;                              // состояние отправки SMS абонентам
unsigned char AttemptSMS=0;                             // количество попыток отправки SMS
unsigned char AttemptCall=0;                            // количество попыток дозвона
unsigned char ChangeRegNetwork=240;                     // время проверки регистрации
unsigned char Zona=0;                                   // статус зоны при активированной системе
char FHandContr[12];                                    // идентификатор контроллера

bit RCallRDY=0;                                         // прохождение сообщения готовности модема
bit StartFlagPC=0;                                      // старт пакета от компьютера
bit sign=0;                                             // состояние системы (активирована/деактивирована)

void Receive_PC(void);                                  // прием пакета от компьютера
void SendAnswer(flash char *);                          // отправка строки с Flash памяти
void SendAnswerF(flash char *);                         // отправка ответа с Flash памяти
void SendAnswerR(char *);                               // отправка ответа с RAM памяти
void SetFactorySetting(void);                           // установка настроек от производителя
void eeprom2ram(void);                                  // считывание параметров с EEPROM в RAM
void SendInfo(void);                                    
unsigned char SMSF(void);
unsigned char F_Ring(void);                             // ????? ????????? ??????
unsigned char F_SMS(void);                              // ????? ???????? SMS
unsigned char F_Crdy(void);                             // ????????????? GSM ??????
void SendCall(void);                                    // ???????? ?????????? ????????
void SetUserNumber(unsigned char *);
void GetUserNumber(unsigned char *);
void SetTimeActive(unsigned char *);
void GetTimeActive(void);
void SetTimeDisactive(unsigned char *);
void GetTimeDisactive(void);
void SetConfiguration(unsigned char *);
void GetConfiguration(void);
void SetActiveZone(unsigned char *);
void GetActiveZone(void);
unsigned char cmp_digit(unsigned char *,unsigned char);
unsigned char  chartohexh(unsigned char);
unsigned char  chartohexl(unsigned char);

unsigned char counter_adc4_h=0;
unsigned char counter_adc4_m=0;
unsigned char counter_adc4_l=0;
bit level_adc4_l=0;
bit level_adc4_m=0;
bit level_adc4_h=0;
                                         
unsigned char counter_adc5_h=0;
unsigned char counter_adc5_m=0;
unsigned char counter_adc5_l=0;
bit level_adc5_l=0;
bit level_adc5_m=0;
bit level_adc5_h=0;

unsigned char counter_adc6_h=0;
unsigned char counter_adc6_m=0;
unsigned char counter_adc6_l=0;
bit level_adc6_l=0;
bit level_adc6_m=0;
bit level_adc6_h=0;

unsigned char counter_adc7_h=0;
unsigned char counter_adc7_m=0;
unsigned char counter_adc7_l=0;
bit level_adc7_l=0;
bit level_adc7_m=0;
bit level_adc7_h=0;                                             

unsigned char counter_but1_l=0;            
bit level_but1_l=0;            
unsigned char counter_but1_h=0;            
bit level_but1_h=0;            

bit SInfo=0;
bit CurrentNetwork=0;                           
bit sign_on=0;
      
#define RXB8 1
#define TXB8 0
#define UPE 2
#define OVR 3
#define FE 4
#define UDRE 5
#define RXC 7

#define FRAMING_ERROR (1<<FE)
#define PARITY_ERROR (1<<UPE)
#define DATA_OVERRUN (1<<OVR)
#define DATA_REGISTER_EMPTY (1<<UDRE)
#define RX_COMPLETE (1<<RXC)                            

// USART Receiver buffer
#define RX_BUFFER_SIZE0 168
char rx_buffer0[RX_BUFFER_SIZE0];

#if RX_BUFFER_SIZE0<256
unsigned char rx_wr_index0,rx_rd_index0,rx_counter0;
#else
unsigned int rx_wr_index0,rx_rd_index0,rx_counter0;
#endif

// This flag is set on USART Receiver buffer overflow
bit rx_buffer_overflow0;

// USART Receiver interrupt service routine
interrupt [USART_RXC] void usart_rx_isr(void)
{
char status,data;
status=UCSR0A;
data=UDR0;
if ((status & (FRAMING_ERROR | PARITY_ERROR | DATA_OVERRUN))==0)
   {      
   TimeGuard=3;
   rx_buffer0[rx_wr_index0]=data;
   if (++rx_wr_index0 == RX_BUFFER_SIZE0) rx_wr_index0=0;
   if (++rx_counter0 == RX_BUFFER_SIZE0)
      {   
      rx_counter0=0;
      rx_buffer_overflow0=1;
      };
   };
}

#ifndef _DEBUG_TERMINAL_IO_
// Get a character from the USART Receiver buffer
#define _ALTERNATE_GETCHAR_
#pragma used+
char getchar(void)
{
char data;
while (rx_counter0==0);
data=rx_buffer0[rx_rd_index0];
if (++rx_rd_index0 == RX_BUFFER_SIZE0) rx_rd_index0=0;
#asm("cli")
--rx_counter0;
#asm("sei")
return data;
}
#pragma used-
#endif

// USART Transmitter buffer
#define TX_BUFFER_SIZE0 64
char tx_buffer0[TX_BUFFER_SIZE0];

#if TX_BUFFER_SIZE0<256
unsigned char tx_wr_index0,tx_rd_index0,tx_counter0;
#else
unsigned int tx_wr_index0,tx_rd_index0,tx_counter0;
#endif

// USART Transmitter interrupt service routine
interrupt [USART_TXC] void usart_tx_isr(void)
{
if (tx_counter0)
   {
   --tx_counter0;
   UDR0=tx_buffer0[tx_rd_index0];
   if (++tx_rd_index0 == TX_BUFFER_SIZE0) tx_rd_index0=0;
   };
}

#ifndef _DEBUG_TERMINAL_IO_
// Write a character to the USART Transmitter buffer
#define _ALTERNATE_PUTCHAR_
#pragma used+
void putchar(char c)
{
while (tx_counter0 == TX_BUFFER_SIZE0);
#asm("cli")
if (tx_counter0 || ((UCSR0A & DATA_REGISTER_EMPTY)==0))
   {
   tx_buffer0[tx_wr_index0]=c;
   if (++tx_wr_index0 == TX_BUFFER_SIZE0) tx_wr_index0=0;
   ++tx_counter0;
   }
else
   UDR0=c;
#asm("sei")
}
#pragma used-
#endif

// Standard Input/Output functions
#include <stdio.h>

// PUSART1 Receiver buffer
#define RX_BUFFER_SIZE1 64
char rx_buffer1[RX_BUFFER_SIZE1];
#if RX_BUFFER_SIZE1<256
unsigned char rx_wr_index1,rx_rd_index1,rx_counter1;
#else
unsigned int rx_wr_index1,rx_rd_index1,rx_counter1;
#endif

// This flag is set on PUSART1 Receiver buffer overflow
bit rx_buffer_overflow1;

unsigned char UartswRxData;
unsigned char UartswRxBitNum;
char getchar1(void);

// External Interrupt 0 service routine
interrupt [EXT_INT0] void ext_int0_isr(void)
{
// Place your code here
 EIMSK&=~0x01;
 OCR2A=TCNT2+36;          
 TIFR2|=0x02;
 TIMSK2|=0x02;
 UartswRxBitNum=0;
 UartswRxData=0;
}
// Timer 2 output compare interrupt service routine
interrupt [TIM2_COMPA] void timer2_compa_isr(void)
{
// Place your code here
 UartswRxData >>=1;
 if(PIND.2) UartswRxData |= 0x80;
 UartswRxBitNum++;
 OCR2A+=24;          
 if(UartswRxBitNum >= 8)
 {               
    rx_buffer1[rx_wr_index1]=UartswRxData;
    if (++rx_wr_index1 == RX_BUFFER_SIZE1)rx_wr_index1=0;
    if (++rx_counter1 == RX_BUFFER_SIZE1)
    {
        rx_counter1=0;
        rx_buffer_overflow1=1;
    }
    TIMSK2&=~0x02; 
    EIFR|=0x01;
    EIMSK|=0x01;
 }
}
char getchar1(void)
{
char data;
while (rx_counter1==0);
data=rx_buffer1[rx_rd_index1];
if (++rx_rd_index1 == RX_BUFFER_SIZE1) rx_rd_index1=0;
#asm("cli")
--rx_counter1;
#asm("sei")
return data;
}

// USART Transmitter buffer
#define TX_BUFFER_SIZE1 64
char tx_buffer1[TX_BUFFER_SIZE1];

#if TX_BUFFER_SIZE1<256
unsigned char tx_wr_index1,tx_rd_index1,tx_counter1;
#else
unsigned int tx_wr_index1,tx_rd_index1,tx_counter1;
#endif

unsigned char UartswTxData;
unsigned char UartswTxBitNum;
bit UartswTxBusy=0; 

// Timer 2 output compare interrupt service routine
interrupt [TIM2_COMPB] void timer2_compb_isr(void)
{
// Place your code here
	if(UartswTxBitNum)
	{
		if(UartswTxBitNum > 1)
		{
			if(UartswTxData & 0x01) PORTC.0=1;
			else PORTC.0=0;
			UartswTxData>>=1;
		}
		else
		{                                     
		    PORTC.0=1;
		}
		UartswTxBitNum--;
		OCR2B+=24;
	}
	else
	{                  
	    if (tx_counter1)
        {
            --tx_counter1;
            UartswTxData = tx_buffer1[tx_rd_index1];;
	        UartswTxBitNum = 9;	
            OCR2B=TCNT2+24;
            TIFR2|=0x04;
            TIMSK2|=0x04;
            PORTC.0=0;
            if (++tx_rd_index1 == TX_BUFFER_SIZE1) tx_rd_index1=0;
        }
        else 
        {
            UartswTxBusy = 0;
            TIMSK2&=~0x04;            
        }    
	}
}

void putchar1(char c)
{
while (tx_counter1 == TX_BUFFER_SIZE1);
 #asm("cli")
if (tx_counter1 || (UartswTxBusy))
   {
   tx_buffer1[tx_wr_index1]=c;
   if (++tx_wr_index1 == TX_BUFFER_SIZE1) tx_wr_index1=0;
   ++tx_counter1;
   }
else
   {
    UartswTxBusy = 1;
	UartswTxData = c;
	UartswTxBitNum = 9;	
    TIFR2|=0x04;
    TIMSK2|=0x04;
    OCR2B=TCNT2+24;   
    PORTC.0=0;
   }
#asm("sei")
}          

// Timer 1 output compare A interrupt service routine
interrupt [TIM1_COMPA] void timer1_compa_isr(void)
{
// Place your code here
 LED_ON                            
 if(gsm_counter_AC) gsm_counter_AC--; 
 if(!sign)
 {
    if(sign_on)
    {
        if(!SETSR)
        {
            if(((ZonaS&ZoneMask)==ZoneMask)&&(ZoneMask)&&(!Zona))
               SETSR=SETSE; 
        }
        else
        {           
            BUZ_TOG
            SETSR--;
            if(!SETSR)
            {         
                if(((ZonaS&ZoneMask)==ZoneMask)&&(ZoneMask)&&(!Zona)) 
                {
                    sign=1;
                    sign_E=1;
                }
                BUZ_OFF                
            }     
        }
    }
 }
 else
 {
    if(Zona&ZoneMask)
    {
        if(sign_on)
        {
            if(!GETSR)
            {
                GETSR=GETSE; 
            }
            else
            {
                GETSR--;
                if(!GETSR)
                {
                    SInfo=1;
                    StatusSMS=SMSE;
                    StatusCall=CALE;
                    AttemptCall=NUMCALL;   
                    AttemptSMS=AMOUNT_ATTEMPT_SMS;
                    sign=0;
                    sign_E=0;
                }   
            }
        }
    }
    else
    {
        if(!sign_on)
        {
            sign=0;
            sign_E=0;
        }
    }   
 }    
 if(!RCallRDY) 
     if(rdy_counter) rdy_counter--;
 if(work_modem) work_modem--;
 if(ChangeRegNetwork) ChangeRegNetwork--;
}

// Timer 1 output compare B interrupt service routine
interrupt [TIM1_COMPB] void timer1_compb_isr(void)
{
// Place your code here
 if(!sign) LED_OFF
}

// Timer 0 output compare A interrupt service routine
interrupt [TIM0_COMPA] void timer0_compa_isr(void)
{
// Place your code here
 if(CounterGSM) CounterGSM--;
 if(TimeGuard)TimeGuard--;
 if(BUT)
 {
    counter_but1_h=0;
    if(!level_but1_l)
    {
        if(counter_but1_l>=DELAYMC240) 
        {
            level_but1_l=1;
            level_but1_h=0;
            sign_on=1; //set
            GETSR=0;
            if(!SInfo)
            {
                Zona=0;
                ZonaE=0;                
            }
        }
        else
        {
            counter_but1_l++;    
        }    
    }
 }            
 else
 {           
    counter_but1_l=0;            
    if(!level_but1_h)
    {
        if(counter_but1_h>=DELAYMC240) 
        {
            level_but1_h=1;
            level_but1_l=0;            
            sign_on=0;      
            SETSR=0;
            BUZ_OFF
            if(sign)
            {
                Zona=0;
                ZonaE=0;    
            }
        }
        else
        {
            counter_but1_h++;    
        }    
   }
 }            
}

#define FIRST_ADC_INPUT 4
#define LAST_ADC_INPUT 7
unsigned char adc_data[LAST_ADC_INPUT-FIRST_ADC_INPUT+1];

#define ADC_VREF_TYPE 0x20

// ADC interrupt service routine
// with auto input scanning
interrupt [ADC_INT] void adc_isr(void)
{
register static unsigned char input_index=0;
// Read the 8 most significant bits
// of the AD conversion result
adc_data[input_index]=ADCH;
// Select next ADC input
if (++input_index > (LAST_ADC_INPUT-FIRST_ADC_INPUT))
   input_index=0;
ADMUX=ADC_VREF_TYPE | ((FIRST_ADC_INPUT+input_index) & 0x07);
if ((FIRST_ADC_INPUT+input_index) & 0x08) ADCSRB |= 0x10;
else ADCSRB &= 0xef;
switch (input_index) {
    case 0 : {                 
                if(MIN_VOLTAGE>adc_data[input_index])
                {
                 counter_adc4_m=0;                       
                 counter_adc4_h=0;            
                 if(!level_adc4_l)
                 {
                    if(counter_adc4_l>=DELAYMC240) 
                        {
                            level_adc4_l=1;
                            level_adc4_m=0;
                            level_adc4_h=0;  
                            LED3_OFF
                            ZonaS&=~(1<<2);
                            if(sign&&(ZoneMask&(1<<2)))
                            {
                                Zona|=(1<<2);
                                ZonaE=Zona;
                            }
                        }
                    else
                        {
                            counter_adc4_l++;    
                        }    
                 }
                }
                else
                {            
                 if((adc_data[input_index]>MIN_VOLTAGE)&&(adc_data[input_index]<MAX_VOLTAGE))
                 {           
                  counter_adc4_l=0;            
                  counter_adc4_h=0;            
                  if(!level_adc4_m)
                  {
                     if(counter_adc4_m>=DELAYMC240) 
                         {
                             level_adc4_m=1;
                             level_adc4_l=0;            
                             level_adc4_h=0;
                             LED3_ON
                             ZonaS|=(1<<2);
                         }
                     else
                         {
                             counter_adc4_m++;    
                        }    
                  }
                 }
                 else
                 {            
                  if(MAX_VOLTAGE<adc_data[input_index])
                  {           
                   counter_adc4_l=0;            
                   counter_adc4_m=0;            
                   if(!level_adc4_h)
                   {
                      if(counter_adc4_h>=DELAYMC240) 
                        {
                            level_adc4_h=1;
                            level_adc4_m=0;
                            level_adc4_l=0;
                            LED3_OFF
                            ZonaS&=~(1<<2);
                            if(sign&&(ZoneMask&(1<<2)))
                            {
                                Zona|=(1<<2);
                                ZonaE=Zona;
                            }
                        }
                      else
                        {
                            counter_adc4_h++;    
                        }
                   }             
                  }
                 }
                }            
              }
    break;
    case 1 : 
    {
                if(MIN_VOLTAGE>adc_data[input_index])
                {
                 counter_adc5_m=0;                       
                 counter_adc5_h=0;            
                 if(!level_adc5_l)
                 {
                    if(counter_adc5_l>=DELAYMC240) 
                        {
                            level_adc5_l=1;
                            level_adc5_m=0;
                            level_adc5_h=0;  
                            LED4_OFF
                            ZonaS&=~(1<<3);
                            if(sign&&(ZoneMask&(1<<3)))
                            {
                                Zona|=(1<<3);
                                ZonaE=Zona;
                            }
                        }
                    else
                        {
                            counter_adc5_l++;    
                        }    
                 }
                }
                else
                {            
                 if((adc_data[input_index]>MIN_VOLTAGE)&&(adc_data[input_index]<MAX_VOLTAGE))
                 {           
                  counter_adc5_l=0;            
                  counter_adc5_h=0;            
                  if(!level_adc5_m)
                  {
                     if(counter_adc5_m>=DELAYMC240) 
                         {
                             level_adc5_m=1;
                             level_adc5_l=0;            
                             level_adc5_h=0;
                             LED4_ON
                             ZonaS|=(1<<3);
                         }                
                     else
                         {
                             counter_adc5_m++;    
                        }    
                  }
                 }
                 else
                 {            
                  if(MAX_VOLTAGE<adc_data[input_index])
                  {           
                   counter_adc5_l=0;            
                   counter_adc5_m=0;            
                   if(!level_adc5_h)
                   {
                      if(counter_adc5_h>=DELAYMC240) 
                        {
                            level_adc5_h=1;
                            level_adc5_m=0;
                            level_adc5_l=0;
                            LED4_OFF
                            ZonaS&=~(1<<3);
                            if(sign&&(ZoneMask&(1<<3)))
                            {
                                Zona|=(1<<3);
                                ZonaE=Zona;
                            }
                        }
                      else
                        {
                            counter_adc5_h++;    
                        }
                   }             
                  }
                 }
                }                
    }

    break;         
    case 2 : 
    {
                if(MIN_VOLTAGE>adc_data[input_index])
                {
                 counter_adc6_m=0;                       
                 counter_adc6_h=0;            
                 if(!level_adc6_l)
                 {
                    if(counter_adc6_l>=DELAYMC240) 
                        {
                            level_adc6_l=1;
                            level_adc6_m=0;
                            level_adc6_h=0;  
                            LED1_OFF
                            ZonaS&=~(1<<0);
                            if(sign&&(ZoneMask&(1<<0)))
                            {
                                Zona|=(1<<0);
                                ZonaE=Zona;
                            }
                        }
                    else
                        {
                            counter_adc6_l++;    
                        }    
                 }
                }
                else
                {            
                 if((adc_data[input_index]>MIN_VOLTAGE)&&(adc_data[input_index]<MAX_VOLTAGE))
                 {           
                  counter_adc6_l=0;            
                  counter_adc6_h=0;            
                  if(!level_adc6_m)
                  {
                     if(counter_adc6_m>=DELAYMC240) 
                         {
                             level_adc6_m=1;
                             level_adc6_l=0;            
                             level_adc6_h=0;
                             LED1_ON
                             ZonaS|=(1<<0);
                         }
                     else
                         {
                             counter_adc6_m++;    
                        }    
                  }
                 }
                 else
                 {            
                  if(MAX_VOLTAGE<adc_data[input_index])
                  {           
                   counter_adc6_l=0;            
                   counter_adc6_m=0;            
                   if(!level_adc6_h)
                   {
                      if(counter_adc6_h>=DELAYMC240) 
                        {
                            level_adc6_h=1;
                            level_adc6_m=0;
                            level_adc6_l=0;
                            LED1_OFF
                            ZonaS&=~(1<<0);
                            if(sign&&(ZoneMask&(1<<0)))
                            {
                                Zona|=(1<<0);
                                ZonaE=Zona;
                            }
                        }
                      else
                        {
                            counter_adc6_h++;    
                        }
                   }             
                  }
                 }
                }            
    }
    break;
    case 3 :
    {
                if(MIN_VOLTAGE>adc_data[input_index])
                {
                 counter_adc7_m=0;                       
                 counter_adc7_h=0;            
                 if(!level_adc7_l)
                 {
                    if(counter_adc7_l>=DELAYMC240) 
                        {
                            level_adc7_l=1;
                            level_adc7_m=0;
                            level_adc7_h=0;  
                            LED2_OFF
                            ZonaS&=~(1<<1);
                            if(sign&&(ZoneMask&(1<<1)))
                            {
                                Zona|=(1<<1);
                                ZonaE=Zona;
                            }
                        }
                    else
                        {
                            counter_adc7_l++;    
                        }    
                 }
                }
                else
                {            
                 if((adc_data[input_index]>MIN_VOLTAGE)&&(adc_data[input_index]<MAX_VOLTAGE))
                 {           
                  counter_adc7_l=0;            
                  counter_adc7_h=0;            
                  if(!level_adc7_m)
                  {
                     if(counter_adc7_m>=DELAYMC240) 
                         {
                             level_adc7_m=1;
                             level_adc7_l=0;            
                             level_adc7_h=0;
                             LED2_ON
                             ZonaS|=(1<<1);
                         }
                     else
                         {
                             counter_adc7_m++;    
                        }    
                  }
                 }
                 else
                 {            
                  if(MAX_VOLTAGE<adc_data[input_index])
                  {           
                   counter_adc7_l=0;            
                   counter_adc7_m=0;            
                   if(!level_adc7_h)
                   {
                      if(counter_adc7_h>=DELAYMC240) 
                        {
                            level_adc7_h=1;
                            level_adc7_m=0;
                            level_adc7_l=0;
                            LED2_OFF
                            ZonaS&=~(1<<1);
                            if(sign&&(ZoneMask&(1<<1)))
                            {
                                Zona|=(1<<1);
                                ZonaE=Zona;
                            }
                        }
                      else
                        {
                            counter_adc7_h++;    
                        }
                   }             
                  }
                 }
                }            
    } ;
    break;
    }; 
TIFR0=0x02;   
}

// Declare your global variables here

void main(void)
{
// Declare your local variables here
unsigned char i;
// Crystal Oscillator division factor: 1
#pragma optsize-
CLKPR=0x80;
CLKPR=0x00;
#ifdef _OPTIMIZE_SIZE_
#pragma optsize+
#endif

// Input/Output Ports initialization
// Port B initialization
// Func7=In Func6=In Func5=In Func4=Out Func3=Out Func2=Out Func1=Out Func0=Out 
// State7=T State6=T State5=T State4=0 State3=0 State2=0 State1=0 State0=0 
PORTB=0x00;
DDRB=0x1F;

// Port C initialization
// Func6=In Func5=In Func4=In Func3=In Func2=Out Func1=In Func0=Out 
// State6=T State5=T State4=T State3=T State2=0 State1=T State0=1 
PORTC=0x01;
DDRC=0x05;

// Port D initialization
// Func7=Out Func6=In Func5=Out Func4=In Func3=In Func2=In Func1=In Func0=In 
// State7=0 State6=P State5=0 State4=P State3=P State2=T State1=T State0=T 
PORTD=0x08;
DDRD=0x40;

// Timer/Counter 0 initialization
// Clock source: System Clock
// Clock value: 7,200 kHz
// Mode: CTC top=OCR0A
// OC0A output: Disconnected
// OC0B output: Disconnected
TCCR0A=0x02;
TCCR0B=0x05;
TCNT0=0x00;
OCR0A=0x24;
OCR0B=0x00;

// Timer/Counter 1 initialization
// Clock source: System Clock
// Clock value: 7,200 kHz
// Mode: CTC top=OCR1A
// OC1A output: Discon.
// OC1B output: Discon.
// Noise Canceler: Off
// Input Capture on Falling Edge
// Timer 1 Overflow Interrupt: Off
// Input Capture Interrupt: Off
// Compare A Match Interrupt: On
// Compare B Match Interrupt: On
TCCR1A=0x00;
TCCR1B=0x0D;
TCNT1H=0x00;
TCNT1L=0x00;
ICR1H=0x00;
ICR1L=0x00;
OCR1AH=0x1C;
OCR1AL=0x20;
OCR1BH=0x00;
OCR1BL=0xFF;

// Timer/Counter 2 initialization
// Clock source: System Clock
// Clock value: 230,400 kHz
// Mode: Normal top=FFh
// OC2A output: Disconnected
// OC2B output: Disconnected
ASSR=0x00;
TCCR2A=0x00;
TCCR2B=0x03;
TCNT2=0x00;
OCR2A=0x00;
OCR2B=0x00;

// External Interrupt(s) initialization
// INT0: On
// INT0 Mode: Falling Edge
// INT1: Off
// Interrupt on any change on pins PCINT0-7: Off
// Interrupt on any change on pins PCINT8-14: Off
// Interrupt on any change on pins PCINT16-23: Off
EICRA=0x02;
EIMSK=0x01;
EIFR=0x01;
PCICR=0x00;

// Timer/Counter 0 Interrupt(s) initialization
TIMSK0=0x02;
// Timer/Counter 1 Interrupt(s) initialization
TIMSK1=0x06;
// Timer/Counter 2 Interrupt(s) initialization
TIMSK2=0x00;

// USART initialization
// Communication Parameters: 8 Data, 1 Stop, No Parity
// USART Receiver: On
// USART Transmitter: On
// USART0 Mode: Asynchronous
// USART Baud rate: 115200
UCSR0A=0x00;
UCSR0B=0x00;
UCSR0C=0x06;
UBRR0H=0x00;
UBRR0L=0x2F;

// Analog Comparator initialization
// Analog Comparator: Off
// Analog Comparator Input Capture by Timer/Counter 1: Off
ACSR=0x80;
ADCSRB=0x00;

// ADC initialization
// ADC Clock frequency: 460,800 kHz
// ADC Voltage Reference: AVCC pin
// ADC Auto Trigger Source: Timer0 Compare Match
// Only the 8 most significant bits of
// the AD conversion result are used
// Digital input buffers on ADC0: On, ADC1: On, ADC2: On, ADC3: On
// ADC4: Off, ADC5: Off
DIDR0=0x30;
ADMUX=FIRST_ADC_INPUT | (ADC_VREF_TYPE & 0xff);
ADCSRA=0xAC;
ADCSRB&=0xF8;
ADCSRB|=0x03;

// Watchdog Timer initialization
// Watchdog Timer Prescaler: OSC/256k
// Watchdog Timer interrupt: Off
#pragma optsize-
#asm("wdr")
WDTCSR=0x1F;
WDTCSR=0x0F;
#ifdef _OPTIMIZE_SIZE_
#pragma optsize+
#endif

SimPowerOff();
pEEPROM=0;
for(i=0;i!=11;i++)
 FHandContr[i]=(*(pEEPROM+HND+i));
FHandContr[11]=0;
eeprom2ram();
        
if(sign_E)
{
    sign=sign_on=1;
    GETSR=1;
}
if(Zona)
{
    SInfo=1;
    StatusSMS=SMSE;
    StatusCall=CALE;
    AttemptCall=NUMCALL;
    AttemptSMS=AMOUNT_ATTEMPT_SMS;
}

#asm("sei")
SendAnswer(&F_VERSIA[6]);
LED_ON 

SimPowerOn();  
UCSR0B=0xD8;

SendCommand("AT");        
delay_ms(1000);
SendCommand("AT");        

// Global enable interrupts
#asm("sei")
while (1)
      {           
#asm("wdr")
      // Check receive byte with GSM
      if(GSM_PACK)              HandlerEventGSM(); 
      else
      {
        if(rx_buffer_overflowGSM)   {gsm_rx_counter=0;rx_buffer_overflowGSM=0;}
        // Check receive byte with GSM
        if(rx_counterGSM)         Receive_gsm();
        // Check receive packet with GSM
        if(GSM_PACK) continue;
      }          
      if(rx_counterGSM) continue;
      if(status_sim&&(!TimeGuard)&&(!CounterGSM))
      {
        if(status_sim&S_RING)   if(F_Ring())status_sim&=~S_RING;
        if(status_sim&S_SMS)    if(F_SMS()) status_sim&=~S_SMS;
        if(status_sim&S_CRDY)   if(F_Crdy())status_sim&=~S_CRDY;
        //if(status_sim&S_ERROR)  {SimRst();RCallRDY=0;rdy_counter=30;CurrentNetwork=0;status_sim&=~S_ERROR;}
        if(status_sim&S_CREG)   {CurrentNetwork=1;status_sim&=~S_CREG;}        
        if(status_sim&S_CREGNOT){CurrentNetwork=0;status_sim&=~S_CREGNOT;}        
      }                
      if((!work_modem)&&(!TimeGuard)&&(!CounterGSM))
      {
        if(C_SendSimpleCommand(AT,100))
        {   
            SimRst();
            RCallRDY=0;
            rdy_counter=30;
            CurrentNetwork=0;
        }
        work_modem=120;
      }
      if((!ChangeRegNetwork)&&(!TimeGuard)&&(!CounterGSM))
      {
        switch (C_CREGG()) {
            case C_REG_NET : CurrentNetwork=1;
            break;
            case C_NOT_REG_NET :CurrentNetwork=0;
            break;       
        };               
        ChangeRegNetwork=240;
      }  
      if(!RCallRDY) 
        if(!rdy_counter) 
        {
            rdy_counter=30;
            SimRst();
        }                         
      if(SInfo&&(!TimeGuard)&&(!CounterGSM)) SendInfo();
      // Check buffer overflow with PC
      if(rx_buffer_overflowPC)  {StartFlagPC=0;rx_buffer_overflowPC=0;}
      // Check receive byte with PC
      if(rx_counterPC)          Receive_PC();  
      };
}

/**
 * Receive data from PC and shaping packet  
 **/
void Receive_PC(void)
{
 unsigned char data;
 #asm("wdr")
 data=getcharPC(); 
  switch (data) {
    case '$': { StartFlagPC=1; counter_PC=0; }
    break;
    case '\n': 
        if(StartFlagPC)
        {  
         LED_ON
         StartFlagPC=0;
         buffer_PC[counter_PC]=0;
         counter_PC=0;
         data=0;
         while((buffer_PC[counter_PC] != '*')&&(buffer_PC[counter_PC] != '\n')) data^=buffer_PC[counter_PC++];
       	 if((tascii[(data&0xF0)>>4]==buffer_PC[++counter_PC])&&(tascii[(data&0x0F)]==buffer_PC[++counter_PC]))
         {
            if(!strncmpf(buffer_PC, "PCHND", 5))
	        { SendAnswerR(FHandContr); return; }
    		if(!strncmpf(buffer_PC, "PCVER", 5))
            { SendAnswerF(F_VERSIA); return; }
		    if(!strncmpf(buffer_PC, "PCSUN", 5))
		    { SetUserNumber(&buffer_PC[6]);	return;	}
		    if(!strncmpf(buffer_PC, "PCGUN", 5))
		    { GetUserNumber(&buffer_PC[6]);	return;	}
   		    if(!strncmpf(buffer_PC, "PCSTA", 5))
		    { SetTimeActive(&buffer_PC[6]);	return;	}
   		    if(!strncmpf(buffer_PC, "PCGTA", 5))
		    { GetTimeActive();	return;	}
   		    if(!strncmpf(buffer_PC, "PCSTD", 5))
		    { SetTimeDisactive(&buffer_PC[6]);	return;	}
   		    if(!strncmpf(buffer_PC, "PCGTD", 5))
		    { GetTimeDisactive();	return;	}
   		    if(!strncmpf(buffer_PC, "PCSCS", 5))
		    { SetConfiguration(&buffer_PC[6]);	return;	}
   		    if(!strncmpf(buffer_PC, "PCGCS", 5))
		    { GetConfiguration();	return;	}
   		    if(!strncmpf(buffer_PC, "PCSAZ", 5))
		    { SetActiveZone(&buffer_PC[6]);	return;	}
   		    if(!strncmpf(buffer_PC, "PCGAZ", 5))
		    { GetActiveZone();	return;	}
   		    if(!strncmpf(buffer_PC, "PCSFS", 5))
		    { SetFactorySetting(); return;	}   	    
   			if(!strncmpf(buffer_PC, "PCUSD", 5))
		    { C_USSD(&buffer_PC[6]);	return;	}
    		if(!strncmpf(buffer_PC, "PBLFL", 5))
		    {        
		        #asm("cli")
                pEEPROM=0;
                *(pEEPROM+CRCEH)=0xFF;
                *(pEEPROM+CRCEL)=0xFF;
 	            while(1);
            }
	        SendAnswerF(F_COMMANDNOTSUPPORT);
	        return; 
		 }   
	}
    break;  
    default: 
        if(StartFlagPC)
        {
         buffer_PC[counter_PC++]=data;
        }  
    };
}   
                
/**
 * Send answer with Flash memory string.  
 *
 * @param	*data	a pointer to the string command 
 **/
void SendAnswer(flash char *data){
 unsigned char i=0;
  do {     
   #asm("wdr")
   putcharPC(data[i]);
  }while(data[++i]); 
}                               

/**
 * Send answer with Flash memory string  
 *
 * @param	*data	a pointer to the string command 
 **/
void SendAnswerF(flash char *data){
 unsigned char i=0;
 unsigned char checkbyte=0;
  putcharPC('$'); 
  do {     
   #asm("wdr")
   checkbyte^=data[i];
   putcharPC(data[i]);
  }while(data[++i]); 
   putcharPC('*');
   putcharPC(tascii[(checkbyte&0xF0)>>4]);
   putcharPC(tascii[checkbyte&0x0F]);
   putcharPC('\r');
   putcharPC('\n');     
}
/**
 * Send answer with Flash memory string  
 *
 * @param	*data	a pointer to the string command 
 **/
void SendAnswerR(char *data){
 unsigned char i=0;
 unsigned char checkbyte=0;
  putcharPC('$'); 
  do {     
   #asm("wdr")
   checkbyte^=data[i];
   putcharPC(data[i]);
  }while(data[++i]);
  putcharPC('*');
   putcharPC(tascii[(checkbyte&0xF0)>>4]);
   putcharPC(tascii[checkbyte&0x0F]);
   putcharPC('\r');
   putcharPC('\n');     
}

/**
 * Set delta move write coordinate event   
 **/      
void SetUserNumber(unsigned char *mas){
 unsigned char i;
 #asm("wdr")          
 if(0<=(mas[0]-0x30)<5)
 {
    for(i=0;i!=10;i++)
        NUMAR[mas[0]-0x30][i]=NUMAE[mas[0]-0x30][i]=mas[i+1];
    NUMAE[mas[0]-0x30][10]=0;        
    NUMAR[mas[0]-0x30][10]=0;        
 }                      
 else
 {
  SendAnswerF("SYSUN ERROR");
  return;
 }
 SendAnswerF(F_SETUSERNUMBER);
}
/**
 * Set delta move write coordinate event   
 **/      
void GetUserNumber(unsigned char *a){
 unsigned char i;
 unsigned char mas[18]; 
 #asm("wdr")          
 if(0<=(a[0]-0x30)<5)
 {      
    mas[0]='S';
    mas[1]='Y';
    mas[2]='G';
    mas[3]='U';
    mas[4]='N';
    mas[5]=' ';
    mas[6]=a[0];
    for(i=0;i!=10;i++)
        mas[i+7]=NUMAE[a[0]-0x30][i];
    mas[17]=0;        
    SendAnswerR(&mas[0]);    
 }
 else
 {
    SendAnswerF("SYGUN ERROR");   
 }
}

/**
 * Set delta move write coordinate event   
 **/      
void SetTimeActive(unsigned char *mas){
 #asm("wdr")  
 SETSE=chartohexh(mas[0])|chartohexl(mas[1]);
SendAnswerF(F_SETTIMEACTIVE);
}

/**
 * Get delta move write coordinate event   
**/      
void GetTimeActive(void){
 unsigned char mas[9]; 
 #asm("wdr")           
 mas[0]='S';
 mas[1]='Y';
 mas[2]='G';
 mas[3]='T';
 mas[4]='A';
 mas[5]=' ';
 mas[8]=SETSE;
 mas[6]=tascii[(mas[8]&0xF0)>>4];
 mas[7]=tascii[(mas[8]&0x0F)];
 mas[8]=0;
 SendAnswerR(mas);
}

/**
 * Set delta move write coordinate event   
 **/      
void SetTimeDisactive(unsigned char *mas){
 #asm("wdr")          
 GETSE=chartohexh(mas[0])|chartohexl(mas[1]);
 SendAnswerF(F_SETTIMEDISACTIVE);
}
/**
 * Get delta move write coordinate event   
 **/      
void GetTimeDisactive(){
 unsigned char mas[9];
 #asm("wdr")          
 mas[0]='S';
 mas[1]='Y';
 mas[2]='G';
 mas[3]='T';
 mas[4]='D';
 mas[5]=' ';
 mas[8]=GETSE;
 mas[6]=tascii[(mas[8]&0xF0)>>4];
 mas[7]=tascii[(mas[8]&0x0F)];
 mas[8]=0;
 SendAnswerR(mas);
}                                   


/**
 * Установка активных зон   
 **/      
void SetActiveZone(unsigned char *mas){
 #asm("wdr")          
 ZoneMaskE = ZoneMask = chartohexl(mas[0]);
 sign=0;        
 SendAnswerF(F_SETACTIVEZONE);
}
/**
 * Считатать активные зоны   
 **/      
void GetActiveZone(){
 unsigned char mas[8];
 #asm("wdr")          
 mas[0]='S';
 mas[1]='Y';
 mas[2]='G';
 mas[3]='A';
 mas[4]='Z';
 mas[5]=' ';
 mas[6]=tascii[(ZoneMask&0x0F)];
 mas[7]=0;
 SendAnswerR(mas);
}

/**
 * Set delta move write coordinate event   
 **/      
void SetConfiguration(unsigned char *mas){
 #asm("wdr")     
 SMSE=chartohexh(mas[0])|chartohexl(mas[1]);
 CALE=chartohexh(mas[2])|chartohexl(mas[3]);
 SendAnswerF(F_SETCONFIGURATION);
}
/**
 * Get delta move write coordinate event   
 **/      
void GetConfiguration(){
 unsigned char mas[11];
 #asm("wdr")          
 mas[0]='S';
 mas[1]='Y';
 mas[2]='G';
 mas[3]='C';
 mas[4]='S';
 mas[5]=' ';
 mas[10]=SMSE;
 mas[6]=tascii[(mas[10]&0xF0)>>4];
 mas[7]=tascii[(mas[10]&0x0F)];
 mas[10]=CALE;
 mas[8]=tascii[(mas[10]&0xF0)>>4];
 mas[9]=tascii[(mas[10]&0x0F)];
 mas[10]=0;
 SendAnswerR(mas);
}

/**
 * Load with EEPROM to RAM setting.  
 **/             
void eeprom2ram(void)
{
 unsigned char i,j;
 #asm("wdr")         
 for(j=0;j!=NUMA_NUMBER;j++)
  for(i=0;i!=11;i++)           
   NUMAR[j][i]=NUMAE[j][i];
 ZoneMask=ZoneMaskE;
 Zona=ZonaE;              
}             

void SetFactorySetting()
{
 unsigned char i,j;
 #asm("wdr")         
 for(j=0;j!=NUMA_NUMBER;j++)
 {
    for(i=0;i!=10;i++)           
        NUMAR[j][i]=NUMAE[j][i]='0';
    NUMAR[j][10]=NUMAE[j][10]=0;
 }      
 ZoneMaskE=ZoneMask=0x0F;
 sign=0;        
 SMSE=0; 
 CALE=0;
 for(i=0;i!=6;i++)           
  PWDE[i]='0';
 #asm("cli")      
 SETSE=10;
 GETSE=10;
 ZonaE=0;
 sign_E=0;    
 #asm("sei")                               
 SendAnswerF("SYSFS OK");
}

/**
 * Check status signaliz. if active, then 3 tones sending, disactive 1 tone sending  
 **/

unsigned char SMSF(void)
{                                                                  
 unsigned char i,a;
 unsigned char str1[33]; 
 str1[0]=0;
 strcatf(str1,"Alarm:");
 for(i=0;i!=NUMA_NUMBER;i++)
 {
  #asm("wdr")                          
    if((NUMAR[i][1]!=0x30)&&(StatusSMS&(1<<i)))
    {                          
        switch (C_CREGG()) {
            case C_REG_NET :
            {                    
                CurrentNetwork=1;
                str1[6]=0;
                for(a=0;a!=4;a++)
                    if((Zona&ZoneMask)&(1<<a)) strcatf(str1,&str_zone[a][0]);     
                if(SendSMS(str1,&NUMAR[i][0])) {AttemptSMS--;return 0;}
                else StatusSMS&=~(1<<i);
            }
            break;
            case C_NOT_REG_NET :
            {
                CurrentNetwork=0;
                return 0;   
            }
            break;
            default: return 0;       
         }; 
    }    
 }
 if(SMSE) C_SendSimpleCommand(CMGDA,800);  
 return 1;
}         

/**
 * Send abonents information SMS and Call abonents.  
 **/                 
void SendInfo(void)
{
    if(CurrentNetwork)
    {
        if(StatusSMS&&AttemptSMS)
        {        
            if(SMSF()) StatusSMS=0; 
            else  return;
        }
        if(StatusCall&&AttemptCall)
        {
            SendCall();
        }
    }
    if((!(StatusCall&&AttemptCall))&&(!(StatusSMS&&AttemptSMS)))
    {             
        #asm("cli")
        Zona=0;
        ZonaE=0;   
        SInfo=0;
        #asm("sei")
        sign=0;
        SETSR=0;
        sign_on=1;
    }
}                    

/**
 * Send abonents information SMS and Call abonents.  
 **/             
void SendCall(void)
{             
 unsigned char i;           
 while(StatusCall&&AttemptCall)
 {
 #asm("wdr")
 for(i=0;i!=NUMA_NUMBER;i++)
 {
  if((NUMAR[i][1]!=0x30)&&(StatusCall&(1<<i)))
  {          
   switch (C_CREGG()) {
        case C_REG_NET :
        {                    
            CurrentNetwork=1;
            gsm_counter_AC=10;
            while(1)
            {               
                #asm("wdr")
                if(gsm_counter_AC)
                {
                    if(!C_CLCC())
                        if(!(S_CLCC.status))    
                           break;
                }
                else
                    { AttemptCall--; return ;}
                delay_ms(1000);         
            }
            if(!(C_ATDD(&NUMAR[i][0])))
            {
                gsm_counter_AC=DELAY_C;
                GSM_PACK=0;        
                while((!GSM_PACK)&&(gsm_counter_AC))
                {          
                    #asm("wdr") 
                    if(CounterGSM)
                        Receive_gsm();
                    else
                    {
                        if(C_CLCC())return;
                        if(S_CLCC.stat=='0')
                        {
                            StatusCall&=~(1<<i);
                            CounterGSM=0;
                            #asm("sei")
                            break;    
                        }
                        if(S_CLCC.stat=='4') return;
                        CounterGSM=200;
                    }
                }                             
                while((!GSM_PACK)&&(gsm_counter_AC))
                {          
                    #asm("wdr") 
                    Receive_gsm();
                }                                     
                if(GSM_PACK)   
                {
                    if ((strstrf(&gsm_rx_buffer[0],"BUSY")!=NULL))           // Busy  
                    { 
                        if(S_CLCC.stat=='3')
                            StatusCall&=~(1<<i);
                    }
                }
                else
                {  
                    if(C_ATH())return;            
                }
                GSM_PACK=0;
                delay_ms(1000);
            }
            else return;
        }
        break;
        case C_NOT_REG_NET :
        {
            CurrentNetwork=0;
            return ;   
        }
        break;
        default: return ;       
   }; 
  }
 }             
 AttemptCall--;
 }          
}
             
unsigned char chartohexh(unsigned char byte){        
  #asm("wdr")
  switch (byte) 
  {
   case '0': return (0x00); 
   case '1': return (0x10); 
   case '2': return (0x20); 
   case '3': return (0x30); 
   case '4': return (0x40); 
   case '5': return (0x50); 
   case '6': return (0x60); 
   case '7': return (0x70); 
   case '8': return (0x80); 
   case '9': return (0x90); 
   case 'A': return (0xA0); 
   case 'B': return (0xB0); 
   case 'C': return (0xC0); 
   case 'D': return (0xD0); 
   case 'E': return (0xE0); 
   case 'F': return (0xF0);
  }
}  

unsigned char chartohexl(unsigned char byte){        
  #asm("wdr")
  switch (byte) 
  {
   case '0': return (0x00); 
   case '1': return (0x01); 
   case '2': return (0x02); 
   case '3': return (0x03); 
   case '4': return (0x04); 
   case '5': return (0x05); 
   case '6': return (0x06); 
   case '7': return (0x07); 
   case '8': return (0x08); 
   case '9': return (0x09); 
   case 'A': return (0x0A); 
   case 'B': return (0x0B); 
   case 'C': return (0x0C); 
   case 'D': return (0x0D); 
   case 'E': return (0x0E); 
   case 'F': return (0x0F);
  } 
}      
/**
 * Handler event as "RING".  
 *
 * @return	OK	if number register in base 
 **/
unsigned char F_Ring(void)
{      
    unsigned char i;
    #asm("wdr") 
    if(C_CLCC()) return;
    for(i=0;i!=NUMA_NUMBER;i++)
    { 
        if(strstr(&S_CLCC.number[0],&NUMAR[i][0])!=NULL)
        {
            GSM_PACK=0;
            C_SendSimpleCommand(ATA,40);  
            #asm("wdr")
            if(sign)C_SendSimpleCommand(ACT_,40);
            else C_SendSimpleCommand(DISACT_,40);    
            gsm_counter_AC=DELAY_CC;
            GSM_PACK=0;
            while(gsm_counter_AC)
            {
                while((!GSM_PACK)&&(gsm_counter_AC))
                {
                    Receive_gsm();
                    #asm("wdr");
                    if(Zona)gsm_counter_AC=0;
                }                             
                if(GSM_PACK)   
                {
                    if ((strstrf(&gsm_rx_buffer[0],"NO CARRIER")!=NULL))      // No Carrier  
                    { 
                        GSM_PACK=0; return;
                    }
                }
                else
                {  
                    if(C_ATH())return;            
                }
                GSM_PACK=0;
            } 
            gsm_counter_AC=0;
            return 1;                              
        }
    }
    C_ATH();
    return 1;                              
}

/**
 * Handler event as "Call ready".  
 *
 * @return	OK	if number register in base 
 **/
unsigned char F_Crdy(void)
{  
    #asm("wdr") 
    C_SendSimpleCommand(ATE0,40);
    C_SendSimpleCommand(CNMI,40);
    delay_ms(1000);
    C_SendSimpleCommand(CFUN,40);
    C_SendSimpleCommand(CMGF,40);
    C_SendSimpleCommand(CSMP,40);
    C_SendSimpleCommand(CREG,40);
    C_SendSimpleCommand(CMIC,40);
    C_SendSimpleCommand(ATE0V1,40);
    C_SendSimpleCommand(CUSD,40);
    RCallRDY=1;
    return 1;                              
}
/**
 * Handler event as "Receive SMS".  
 *
 * @return	OK	if number register in base 
 **/ 
unsigned char F_SMS(void)
{                     
    unsigned char i;           
    unsigned char mas[200];
    unsigned char *pointer;
    #asm("wdr") 
    C_CMGLU(&mas[0]);    
    
    pointer=strstrf(&mas[0],"CPWD");
    if(pointer!=NULL)
    { 
     if(cmp_digit((pointer+4),6))
     {   
     for(i=0;i!=6;i++)
     if(PWDE[i]!=(*(pointer+4+i)))break;
     if(i==6)    
     {     
         for(i=0;i!=6;i++)
            if(PWDE[i]!='0') break;
         if(i==6)
         {
            pointer=strstrf(&mas[0],"NUM1");
            if(pointer!=NULL)
            {
                if(cmp_digit((pointer+4),10))
                {
                    for(i=0;i!=10;i++)
                        NUMAR[0][i]=NUMAE[0][i]=*(pointer+4+i);   
                }
            }               
         }
         pointer=strstrf(&mas[0],"NPWD");
         if(pointer!=NULL)
         {
            if(cmp_digit((pointer+4),6))
            {
                for(i=0;i!=6;i++)
                 PWDE[i]=*(pointer+4+i);   
            }
         }
         pointer=strstrf(&mas[0],"NUM2");
         if(pointer!=NULL)
         {
            if(cmp_digit((pointer+4),10))
            {
                for(i=0;i!=10;i++)
                 NUMAR[1][i]=NUMAE[1][i]=*(pointer+4+i);   
            }
         }        
         #asm("wdr")         
         pointer=strstrf(&mas[0],"NUM3");
         if(pointer!=NULL)
         {
            if(cmp_digit((pointer+4),10))
            {
                for(i=0;i!=10;i++)
                 NUMAR[2][i]=NUMAE[2][i]=*(pointer+4+i);   
            }
         }
         pointer=strstrf(&mas[0],"NUM4");
         if(pointer!=NULL)
         {
            if(cmp_digit((pointer+4),10))
            {
                for(i=0;i!=10;i++)
                 NUMAR[3][i]=NUMAE[3][i]=*(pointer+4+i);   
            }
         }
         pointer=strstrf(&mas[0],"NUM5");
         if(pointer!=NULL)
         {
            if(cmp_digit((pointer+4),10))
            {
                for(i=0;i!=10;i++)
                 NUMAR[4][i]=NUMAE[4][i]=*(pointer+4+i);   
            }
         }
         pointer=strstrf(&mas[0],"ACTO"); // ?????
         if(pointer!=NULL)
         {
            SETSE=(unsigned char)atoi(pointer+4);
         }
         pointer=strstrf(&mas[0],"DACT"); // ?????
         if(pointer!=NULL)
         {
            GETSE=(unsigned char)atoi(pointer+4);
         }
         pointer=strstrf(&mas[0],"OPTC");      
         if(pointer!=NULL)
         {
            if(cmp_digit((pointer+4),5))
            {
                CALE=0x0;
                for(i=0;i!=5;i++)
                {                           
                    if(*(pointer+4+i)=='1') CALE|=(1<<i);   
                }
            }
         }                      
         pointer=strstrf(&mas[0],"OPTS");
         if(pointer!=NULL)
         {
            if(cmp_digit((pointer+4),5))
            {
                SMSE=0x0;
                for(i=0;i!=5;i++)
                {                           
                    if(*(pointer+4+i)=='1') SMSE|=(1<<i);   
                }
            }
         }
         pointer=strstrf(&mas[0],"ACTZ");
         if(pointer!=NULL)
         {
            if(cmp_digit((pointer+4),4))
            {
                ZoneMask=0x0;
                for(i=0;i!=4;i++)
                {                           
                    if(*(pointer+4+i)=='1') ZoneMask|=(1<<i);
                }
                ZoneMaskE=ZoneMask;
                sign=0;
                sign_E=0;
            }
         }
         mas[0]=mas[1]=0;
         strcatf(&mas[0]," NUM2");
         strcat(&mas[0],&NUMAR[1][0]);
         strcatf(&mas[0]," NUM3");
         strcat(&mas[0],&NUMAR[2][0]);
         strcatf(&mas[0]," NUM4");
         strcat(&mas[0],&NUMAR[3][0]);
         strcatf(&mas[0]," NUM5");
         strcat(&mas[0],&NUMAR[4][0]);
         strcatf(&mas[0]," OPTC");
         for(i=0;i!=5;i++)
         {
            if(CALE&(1<<i))  strcatf(&mas[0],"1");
            else   strcatf(&mas[0],"0");
         }
         strcatf(&mas[0]," OPTS");
         for(i=0;i!=5;i++)
         {
            if(SMSE&(1<<i))  strcatf(&mas[0],"1");
            else   strcatf(&mas[0],"0");
         }
         strcatf(&mas[0]," ACTZ");
         for(i=0;i!=4;i++)
         {
            if(ZoneMask&(1<<i))  strcatf(&mas[0],"1");
            else   strcatf(&mas[0],"0");
         }
         strcatf(&mas[0]," ACTO");
         i=SETSE;
         itoa(i,&mas[150]);
         strcat(&mas[0],&mas[150]);
         strcatf(&mas[0]," DACT");
         i=GETSE;
         itoa(i,&mas[150]);
         strcat(&mas[0],&mas[150]);
         strcatf(&mas[0]," CPWD");
         for(i=0;i!=6;i++)
            mas[150+i]=PWDE[i];
         mas[156]=0;
         strcat(&mas[0],&mas[150]);  
         SendSMS(&mas[0],&NUMAR[0][0]);
         delay_ms(2000);   
     }
     }
   }       
    C_SendSimpleCommand(CMGDA,800);
    return 1;                              
}
unsigned char cmp_digit(unsigned char *mas,unsigned char number)
{
    unsigned char i;         
    for(i=0;i!=number;i++)
        if(!(isdigit(mas[i]))) return 0;
    return 1;    
}                                                                          