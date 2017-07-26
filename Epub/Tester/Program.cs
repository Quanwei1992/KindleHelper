using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using eBdb.EpubReader;

namespace Tester {
	class Program {
		static void Main(string[] args) {
			string[] files = Directory.GetFiles(@"c:\Inetpub\ePubReader\temp\load tests\");
			Console.WriteLine("Started");
			foreach (var file in files) {
				try {
					Epub epub = new Epub(file);
				} catch (Exception e) {
					Console.WriteLine("FileName: " + file + ", Exception: " + e.Message);
				}
			}
			Console.WriteLine("Finished");
			Console.ReadLine();
		}
	}
}
