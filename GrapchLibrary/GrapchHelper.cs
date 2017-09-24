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
    /// <summary>
    /// 支持界面，第四个核心类
    /// </summary>
    public class GrapchHelper:IDisposable, IGrapchHelper
    {
        /// <summary>
        /// 图片文件路径
        /// </summary>
        private string filepath;
        /// <summary>
        /// 图片文件路径
        /// </summary>
        public string Filepath { get { return filepath; } set { filepath = value; } }
        /// <summary>
        /// 内部绘制对象
        /// </summary>
        private Graphics g;
        /// <summary>
        /// 绘制对象
        /// </summary>
        public Graphics G { get { return g; } set { g = value; } }
        /// <summary>
        /// 用一个参数初始化新实例
        /// </summary>
        /// <param name="g">绘制对象</param>
        public GrapchHelper(Graphics g)
        {
            this.g = g;
        }
        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="height">高度</param>
        /// <param name="width">宽度</param>
        public void Draw(int height,int width)
        {
            try
            {
                string path;
                if (filepath!=null)
                {
                    path = filepath;
                }
                else
                {
                    path = Environment.CurrentDirectory + "\\test.jpg";
                }
                if (File.Exists(path))
                {
                Image i = Image.FromFile(path);
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
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                LogHelper.Flush();
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            g.Dispose();
            
        }
    }
}
