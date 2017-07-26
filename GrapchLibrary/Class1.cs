using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using loglibrary;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace GrapchLibrary
{
    public class Class1:IDisposable
    {
        private Graphics g;
        public Graphics G { get { return g; } set { g = value; } }
        public Class1(Graphics g)
        {
            this.g = g;
        }
        public void Draw(int height,int width)
        {
            try
            {         
                if (File.Exists(Environment.CurrentDirectory + "\\test.jpg"))
                {
                Image i = Image.FromFile(Environment.CurrentDirectory + "\\test.jpg");
                g.DrawImage(i,0.0f,0.0f,width,height);
                g.Flush();
                i.Dispose();
                g.Dispose();  
                }
                else
                {
                  MessageBox.Show("无法打开文件，原因是找不到该文件");
                  throw new FileNotFoundException();
                }
            }
            catch (Exception ee)
            {
                LogHelper.Error(ee);
                LogHelper.Flush();
            }
        }

        public void Dispose()
        {
            g.Dispose();
            
        }
    }
}
