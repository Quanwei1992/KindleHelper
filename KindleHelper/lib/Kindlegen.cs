using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace KindleHelper
{
    public class Kindlegen
    {

        private static string tpl_cover;
        private static string tpl_book_toc;
        private static string tpl_chapter;
        private static string tpl_content;
        private static string tpl_style;
        private static string tpl_toc;
        private static bool tplIsLoaded = false;


        public static void book2Txt(Book book,string savePath)
        {
            string txt = "";
            for (int i = 0; i < book.chapters.Length; i++) {
                var chapterInfo = book.chapters[i];
                txt += chapterInfo.title + "\r\n";
                txt += chapterInfo.body + "\r\n";
            }

            string saveDir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(saveDir)) {
                Directory.CreateDirectory(saveDir);
            } 
            File.WriteAllText(savePath, txt);
        }

        public static void book2Mobi(Book book,string savePath)
        {
            loadTemplates();
            //create tmp
            if (Directory.Exists("./tmp")) {
                Directory.Delete("./tmp",true);
            }
            Directory.CreateDirectory("./tmp");
            createCover(book);
            createChapters(book);
            createStyle(book);
            createBookToc(book);
            createNaxToc(book);
            createOpf(book);
            gen(savePath);
        }


        static void createCover(Book book)
        {
            string path = "./tmp/cover.html";
            string content = tpl_cover;
            content = content.Replace("___BOOK_NAME___", book.name);
            content = content.Replace("___BOOK_AUTHOR___", book.author);
            File.WriteAllText(path, content);
        }


        static void createChapters(Book book)
        {
            for (int i = 0; i < book.chapters.Length; i++) {
                var chapter = book.chapters[i];
                string path = "./tmp/chapter" + i+".html";
                string content = tpl_chapter;
                content = content.Replace("___CHAPTER_ID___","Chapter " + i);
                content = content.Replace("___CHAPTER_NAME___", chapter.title);
                string chapterContent = chapter.body;
                chapterContent = chapterContent.Replace("\r", "");
                var ps = chapterContent.Split('\n');
                chapterContent = "";
                foreach (var p in ps) {
                    string pStr = "<p class=\"a\">";
                    pStr += "　　" + p;
                    pStr += "</p>";
                    chapterContent += pStr;
                }
                content = content.Replace("___CONTENT___",chapterContent);
                File.WriteAllText(path, content);
            }

        }

        static void createStyle(Book book)
        {
            string path = "./tmp/style.css";
            File.WriteAllText(path,tpl_style);
        }

        static void createBookToc(Book book)
        {
            string path = "./tmp/book-toc.html";
            string content = tpl_book_toc;
            string tocContent = "";
            for (int i = 0; i < book.chapters.Length; i++) {
                var chapter = book.chapters[i];
                string tocLine =string.Format("<dt class=\"tocl2\"><a href=\"chapter{0}.html\">{1}</a></dt>\r\n",
                    i,chapter.title);
                tocContent += tocLine;
            }
            content = content.Replace("___CONTENT___", tocContent);
            File.WriteAllText(path, content);
        }

        static void createNaxToc(Book book)
        {
            string path = "./tmp/toc.ncx";
            string content = tpl_toc;
            content = content.Replace("___BOOK_ID___", book.id);
            content = content.Replace("___BOOK_NAME___", book.name);
            string tocContent = "";
            for (int i = 0; i < book.chapters.Length; i++) {
                var chapter = book.chapters[i];
                string tocLine = string.Format("<navPoint id=\"chapter{0}\" playOrder=\"{1}\">\r\n", i, i+1);
                tocLine += string.Format("<navLabel><text>{0}</text></navLabel>\r\n",chapter.title);
                tocLine += string.Format("<content src=\"chapter{0}.html\"/>\r\n</navPoint>\r\n", i);
                tocContent += tocLine;
            }
            content = content.Replace("___NAV___", tocContent);
            File.WriteAllText(path, content);
        }

        static void createOpf(Book book) {
            string path = "./tmp/content.opf";
            string content = tpl_content;
            content = content.Replace("___BOOK_ID___", book.id);
            content = content.Replace("___BOOK_NAME___", book.name);
            string manifest = "";
            string spine = "";
            for (int i = 0; i < book.chapters.Length; i++) {
                var chapter = book.chapters[i];
                // mainifest
                string tocLine = "";
                tocLine = string.Format("<item id=\"chapter{0}\" href=\"chapter{0}.html\" media-type=\"application/xhtml+xml\"/>\r\n", i);
                manifest += tocLine;
                // spine
                string spineLine = "";
                spineLine = string.Format("<itemref idref=\"chapter{0}\" linear=\"yes\"/>\r\n", i);
                spine += spineLine;


            }
            content = content.Replace("___MANIFEST___", manifest);
            content = content.Replace("___SPINE___", spine);
            File.WriteAllText(path, content);
        }

        static void loadTemplates()
        {
            if (tplIsLoaded) return;
            tpl_cover = File.ReadAllText("./tpls/tpl_cover.html");
            tpl_book_toc = File.ReadAllText("./tpls/tpl_book_toc.html");
            tpl_chapter = File.ReadAllText("./tpls/tpl_chapter.html");
            tpl_content = File.ReadAllText("./tpls/tpl_content.opf");
            tpl_style = File.ReadAllText("./tpls/tpl_style.css");
            tpl_toc = File.ReadAllText("./tpls/tpl_toc.ncx");
            tplIsLoaded = true;
        }

        static void gen(string savePath)
        {
            string binPath = Directory.GetCurrentDirectory() + "/tools/kindlegen.exe";
            string param = "content.opf -c1 -o book.mobi";
            ProcessStartInfo p = null;
            Process Proc;
            p = new ProcessStartInfo(binPath, param);
            p.WorkingDirectory = Directory.GetCurrentDirectory() + "/tmp";
            Proc = Process.Start(p);//调用外部程序
            Proc.WaitForExit();
            if (File.Exists("./tmp/book.mobi")) {
                if (File.Exists(savePath)) {
                    File.Delete(savePath);
                }
                File.Move("./tmp/book.mobi", savePath);
            }
            Directory.Delete("./tmp", true);
        }
    }
}
