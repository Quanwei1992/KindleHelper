using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libZhuishu;
using System.Diagnostics;

namespace KindleHelper
{
    public partial class FormSearch : Form
    {
        public FormSearch()
        {
            InitializeComponent();
        }


        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void textbox_search_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textbox_search.Text)) return;
            listbox_autocomplate.Items.Clear();
            var words = LibZhuiShu.autoComplate(textbox_search.Text);
            if (words != null && words.Length > 0) {
                listbox_autocomplate.Items.AddRange(words);
                listbox_autocomplate.Visible = true;
            }
        }

        private void listbox_autocomplate_SelectedValueChanged(object sender, EventArgs e)
        {
            textbox_search.Text = listbox_autocomplate.SelectedItem.ToString();
            listbox_autocomplate.Visible = false;
        }
    }
}
