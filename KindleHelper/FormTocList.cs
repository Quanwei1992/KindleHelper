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
    public partial class FormTocList : Form
    {
        MixTocInfo mMixToc;
        TocSummmaryInfo[] mTocs;
        Action<int> mCallback;

        public FormTocList()
        {
            InitializeComponent();
        }

        private void FormTocList_Load(object sender, EventArgs e)
        {
            listview_toc.Columns.Add("书源",120);
            listview_toc.Columns.Add("章节数量", 80);
            listview_toc.Columns.Add("最后更新章节", 120);
            listview_toc.Columns.Add("最后更新时间", 120);
        }

        public void ShowTocList(MixTocInfo mixToc,TocSummmaryInfo[] tocs,Action<int> callback)
        {
            mMixToc = mixToc;
            mTocs = tocs;
            mCallback = callback;
            listview_toc.BeginUpdate();
            //添加混合源
            ListViewItem mixTocItem = new ListViewItem();
            mixTocItem.Text = "混合源";
            mixTocItem.SubItems.Add(mMixToc.chapters.Length.ToString());
            mixTocItem.SubItems.Add(mMixToc.chapters[mMixToc.chapters.Length - 1].title);
            mixTocItem.SubItems.Add(mMixToc.updated);
            listview_toc.Items.Add(mixTocItem);
            foreach (var toc in tocs) {
                ListViewItem item = new ListViewItem();
                item.Text = toc.name;
                item.SubItems.Add(toc.chaptersCount.ToString());
                item.SubItems.Add(toc.lastChapter);
                item.SubItems.Add(toc.updated);
                listview_toc.Items.Add(item);
            }

            listview_toc.EndUpdate();
            this.Show();
        }

        private void listview_toc_DoubleClick(object sender, EventArgs e)
        {
            if (listview_toc.SelectedIndices.Count > 0 && mCallback != null) {
                mCallback(listview_toc.SelectedIndices[0]-1);
                this.Close();
            }
        }
    }
}
