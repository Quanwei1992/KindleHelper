using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibImport;
using System.Windows.Forms;
using loglibrary;
namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Class1 c = new Class1();
            c.Creat();
            string str = c.Read();
            LogHelper.Info(str);
            LogHelper.Flush();
        }
    }
}
