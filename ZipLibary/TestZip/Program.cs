using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using Ionic.Zlib;

namespace TestZip
{
    class Program
    {
        static void Main(string[] args)
        {
            ZipFile file = new ZipFile("Desktop.zip");
            file.ExtractAll("./");
            Console.WriteLine("完成");
            Console.Read();

        }
    }
}
