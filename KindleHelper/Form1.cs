using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GrapchLibrary;
using System.Runtime.InteropServices;

namespace KindleHelper
{
    public partial class Form1 : Form
    {
        int a, b;
        Class1 c;
        public Form1()
        {
            InitializeComponent();
            a = Width;
            b = Height;
        }   
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void p(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            c = new Class1(g);
            c.Draw(b, a);
            g.Dispose();
        }

        private void l(object sender, EventArgs e)
        {
            a = Width;
            b = Height;
            Graphics g = Graphics.FromHwnd(Handle);
            c = new Class1(g);
            c.Draw(b, a);
            g.Dispose();
            c.Dispose();
            foreach (Control item in Controls)
            {
                item.Width = Width /3;
                item.Height = Height - 100;
            }
        }
    }
}
