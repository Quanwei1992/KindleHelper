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

namespace KindleHelper
{
    public partial class FormSearchResult : Form
    {

        private QueryBookInfo[] cacheResults;

        public FormSearchResult()
        {
            InitializeComponent();
        }

        private void FormSearchResult_Load(object sender, EventArgs e)
        {
            ColumnHeader nameHeader = new ColumnHeader();
            listview_result.Columns.Add("书名",120, HorizontalAlignment.Left);
            listview_result.Columns.Add("作者", 80, HorizontalAlignment.Left);
            listview_result.Columns.Add("网站", 80, HorizontalAlignment.Left);
            listview_result.Columns.Add("字数", 60, HorizontalAlignment.Left);
            listview_result.Columns.Add("留存率", 60, HorizontalAlignment.Left);
            listview_result.Columns.Add("最后更新章节", 180, HorizontalAlignment.Left);
            listview_result.Columns.Add("简介", 250, HorizontalAlignment.Left);

            
        }


        public void ShowResult(QueryBookInfo[] results)
        {          
            if (results == null) return;
            listview_result.BeginUpdate();
            foreach (var result in results) {
                ListViewItem item = new ListViewItem();
                item.Text = result.title;
                item.SubItems.Add(result.author);
                item.SubItems.Add(result.site);
                item.SubItems.Add(formatWordCount(result.wordCount));
                if (!string.IsNullOrWhiteSpace(result.retentionRatio)) {
                    item.SubItems.Add(result.retentionRatio + "%");
                } else {
                    item.SubItems.Add("无");
                }

                item.SubItems.Add(result.lastChapter);
                item.SubItems.Add(result.shortIntro);
                listview_result.Items.Add(item);
            }
            listview_result.EndUpdate();
            this.Show();
            cacheResults = results;
        }


        static string formatWordCount(long wordCount)
        {
            if (wordCount > 10000) {
                return (wordCount / 10000) + " 万";
            }
            return wordCount.ToString();
        }

        private void listview_result_DoubleClick(object sender, EventArgs e)
        {
            var lv = listview_result;
            if (lv.SelectedIndices.Count > 0) {
                var index = lv.SelectedIndices[0];
                var detailForm = new FormBookDetail();
                detailForm.ShowBook(cacheResults[index]);
            }
        }
    }
}
