/*
  CodeVisionAVR C Compiler
  (C) 2008 Taras Drozdovsky, My.
*/

#ifndef _BOARD_INCLUDED_
#define _BOARD_INCLUDED_

#include <mega168.h>
                
#define CRCEH       511
#define CRCEL       510
#define HND         496

#define LED_ON      PORTB.0=1;
#define LED_OFF     PORTB.0=0;
#define LED_TOD     PORTB.0=~PORTB.0;

#define LED1_ON     PORTB.1=1;
#define LED1_OFF    PORTB.1=0;
#define LED1_TOD    PORTB.1=~PORTB.1;

#define LED2_ON     PORTB.2=1;
#define LED2_OFF    PORTB.2=0;
#define LED2_TOD    PORTB.2=~PORTB.2;

#define LED3_ON     PORTB.3=1;
#define LED3_OFF    PORTB.3=0;
#define LED3_TOD    PORTB.3=~PORTB.3;

#define LED4_ON     PORTB.4=1;
#define LED4_OFF    PORTB.4=0;
#define LED4_TOD    PORTB.4=~PORTB.4;


#define BUZ_ON      PORTC.2=1;  
#define BUZ_OFF     PORTC.2=0;      
#define BUZ_TOG     PORTC.2=~PORTC.2;  

// Declare PIN POWER SIM
#define SIM_ON    PORTD.5
#define VDD_EXT   PIND.4
      
#define BUT       PIND.3
#define NUMA_NUMBER 5

#define  rx_buffer_overflowGSM rx_buffer_overflow0
#define rx_counterGSM  rx_counter0
#define tx_counterGSM  tx_counter0
#define putcharGSM     putchar
#define getcharGSM     getchar

#define  rx_buffer_overflowPC rx_buffer_overflow1
#define rx_counterPC  rx_counter1
#define tx_counterPC  tx_counter1
#define putcharPC     putchar1
#define getcharPC     getchar1

// Possible respons after executant command
#define C_OK              0             
#define C_ERROR           2
#define C_REG_NET         8
#define C_NOT_REG_NET     9
    
#endif         