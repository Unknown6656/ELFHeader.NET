// Dies ist die Haupt-DLL.

#include "stdafx.h"

#include "REG_TEST.h"


int* get_registers()
{
	int reg[4];

	__asm
	{
		MOV reg[0], EAX
		MOV reg[1], EBX
		MOV reg[2], ECX
		MOV reg[3], EDX
	}

	return reg;
}

int* REG_TEST::Class1::get_regs()
{
	return get_registers();
}
