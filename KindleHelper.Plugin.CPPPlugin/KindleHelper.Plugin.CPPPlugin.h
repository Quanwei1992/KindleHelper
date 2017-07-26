// KindleHelper.Plugin.CPPPlugin.h

#pragma once

using namespace System;
using namespace KindleHelper::Plugin::Interface;
namespace KindleHelper {
	namespace Plugin
	{
	namespace CPPPlugin
	{
	public ref class CPPPlugin:public IPlugin
	{
	public:
		// TODO: 在此处添加此类的方法。
		virtual String^ Show()
		{return "CPPPlugin";};
        virtual void ShowForm()
		{};
        virtual void ShowFormAsDialog()
	    {};
        virtual String^ version()
		{return "1.0.0.0";};
        virtual Object^ getresult(){return "123";};
		virtual Object^ run(){return "123";};
		virtual System::Diagnostics::Stopwatch^ Watch() {auto watch=gcnew System::Diagnostics::Stopwatch();watch->Start();for(long i=0;i<10000;i++){long j=j+i;}watch->Stop();return watch;};
		virtual bool IsNotNeedToShowAsDialog(){return false;}
	};
	}
	}
}
