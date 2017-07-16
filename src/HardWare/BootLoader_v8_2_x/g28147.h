/*
  CodeVisionAVR C Compiler
  (C) 2006 Taras Drozdovsky, My.
*/                 
#ifndef _GOST28147_INCLUDED_
#define _GOST28147_INCLUDED_

#pragma used+
void Prost_E(unsigned long *D,unsigned long *KZU,unsigned char *K);
void Gam_cD(unsigned long *D,unsigned long *KZU,unsigned char *K,unsigned char lenght);
void Gost_init(unsigned long *);
#pragma used-
#endif
