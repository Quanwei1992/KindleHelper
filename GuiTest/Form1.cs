using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapchLibrary;
namespace GuiTest
{
    public partial class Form1 : Form
    {
        int a, b;
        GrapchHelper c;
        private void P(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            c = new GrapchHelper(g);
            c.Draw(a, b);
            g.Dispose();
        }

        public Form1()
        {
            InitializeComponent();
            a = Width;
            b = Height;
        }

    }
}
