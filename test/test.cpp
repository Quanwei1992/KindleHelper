// test.cpp: 主项目文件。

#include "stdafx.h"
#import"ClassLibrary1.tlb"
using namespace System;
using namespace System::Net;
int main(array<System::String ^> ^args)
{
    CoInitialize(NULL);  //注意初始化  
    ClassLibrary1::MyCom_InterfacePtr p(__uuidof(ClassLibrary1::Class1));  //创建智能指针  
    ClassLibrary1::MyCom_Interface *s = p;  
    int a = 3;  
    int b = 6;  
    int c = s->Add(a,b);  
    Console::WriteLine(c);
	Console::ReadKey();
    return 0;
}
