// REG_TEST.h

#pragma once

using namespace System;

namespace REG_TEST {

	public ref class Class1
	{
	public:
		static int* get_regs();
	};
}

int* get_registers(void);
