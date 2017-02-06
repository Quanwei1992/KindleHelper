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
using System.IO;
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
            FormSearchResult form_result = new FormSearchResult();
            var results = LibZhuiShu.fuzzySearch(textbox_search.Text, 0, 100);
            if (results == null || results.Length < 1) {
                MessageBox.Show("没有找到:" + textbox_search.Text);
                return;
            }
            form_result.ShowResult(results);
        }

        private void textbox_search_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textbox_search.Text)) return;
            listbox_autocomplate.Items.Clear();
            RunAsync(()=> {
                var words = LibZhuiShu.autoComplate(textbox_search.Text);
                RunInMainthread(()=> {
                    if (words != null && words.Length > 0) {
                        listbox_autocomplate.Items.AddRange(words);
                        listbox_autocomplate.Visible = true;
                    } else {
                        listbox_autocomplate.Visible = false;
                    }
                });
            });


        }

        private void listbox_autocomplate_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listbox_autocomplate.SelectedItem == null) return;

            textbox_search.Text = listbox_autocomplate.SelectedItem.ToString();
            listbox_autocomplate.Visible = false;
            btn_search_Click(sender,e);
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {
            this.Text += " V" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }


        void RunAsync(Action action)
        {
            ((Action)(delegate () {
                action?.Invoke();
            })).BeginInvoke(null, null);
        }

        void RunInMainthread(Action action)
        {
            this.BeginInvoke((Action)(delegate () {
                action?.Invoke();
            }));
        }

    }
}
