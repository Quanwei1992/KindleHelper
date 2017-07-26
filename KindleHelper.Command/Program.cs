using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using libZhuishu;
using System.Threading;
namespace KindleHelperCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(new ParameterizedThreadStart((o) => { new testlib.Class1(); })).Start();   
            var result=LibZhuiShu.autoComplate("完美");
            var books = LibZhuiShu.fuzzySearch("完美世界", 0, 1); 
            foreach (var book in books)
            {
                Console.WriteLine(string.Format("{0}  {1}  {2}", book._id, book.title, book.author));
                var tocs = LibZhuiShu.getTocSummary(book._id);
                var chapertList = LibZhuiShu.getChaperList(tocs[0]._id);
                Console.WriteLine(LibZhuiShu.getChapter(chapertList.chapters[0].link).body);
            }
            foreach (var item in result)
            {
            Console.WriteLine("章节:" +item);    
            Console.ReadKey();
            }
            
        }
    }
}
