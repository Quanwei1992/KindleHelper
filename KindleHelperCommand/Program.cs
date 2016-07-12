using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using libZhuishu;
namespace KindleHelperCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            
 
            var books = LibZhuiShu.fuzzySearch("完美世界", 0, 1);
            Console.WriteLine("Result Count:" + books.Length);
            foreach (var book in books) {
                Console.WriteLine(string.Format("{0}  {1}  {2}",book._id,book.title,book.author));
                Console.WriteLine(LibZhuiShu.getTocSummary(book._id));
            }
            Console.ReadKey();
        }
    }
}
