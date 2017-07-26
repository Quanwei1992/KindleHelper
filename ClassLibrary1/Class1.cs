using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
namespace ClassLibrary1
{
    [Guid("154BD6A6-5AB8-4d7d-A343-0A68AB79470B")]

    public interface MyCom_Interface
    {

        [DispId(1)]

        int Add(int a, int b);

    }



    [Guid("D11FEA37-AC57-4d39-9522-E49C4F9826BB"),

    InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]

    public interface MyCom_Events
    {



    }



    [Guid("2E3C7BAD-1051-4622-9C4C-215182C6BF58"),

      ClassInterface(ClassInterfaceType.None),

      ComSourceInterfaces(typeof(MyCom_Events))]

    public class Class1 : MyCom_Interface
    {

        public int Add(int a, int b)
        {

            return a + b;

        }

    }
    [Guid("B764EE62-6EEF-4DDF-8045-F0CB1438EF36")]

    public interface MyCom_Interface_2
    {

        [DispId(1)]
        HttpWebRequest CreateHttp(string requestUriString);

    }

    [Guid("8583E0D5-820F-4B19-9F04-208DCE6C9366")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface MyCom_Events_2
    {



    }
    [Guid("B8B3B408-EA74-449F-9A0D-1C43492BCCC6"),
      ClassInterface(ClassInterfaceType.None),
      ComSourceInterfaces(typeof(MyCom_Events_2))]
    public class Class2:MyCom_Interface_2
    {
        public  HttpWebRequest CreateHttp(string requestUriString)
        {
            return creathttp.CreateHttp(requestUriString);
        }
    }
}
