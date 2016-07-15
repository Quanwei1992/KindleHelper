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
using System.Net;
using System.Threading;
using System.IO;
namespace KindleHelper
{
    public partial class FormBookDetail : Form
    {


        public FormBookDetail()
        {
            InitializeComponent();
        }
        QueryBookInfo mBook;
        tocChaperInfo[] mChapers;
        TocSummmaryInfo[] mTocs;
        MixTocInfo mMixToc;

        public void ShowBook(QueryBookInfo book)
        {
            mBook = book;
            label_name.Text = book.title;
            string wordCount = book.wordCount + "字";
            if (book.wordCount >= 10000) {
                wordCount = (book.wordCount / 10000) + " 万字";
            }
            label_baseinfo.Text = book.author + " | " + book.cat + " | " + wordCount;
            if (!string.IsNullOrWhiteSpace(book.retentionRatio)) {
                label_retentionRatio.Text = "追书留存率:" + book.retentionRatio + "%";
            } else {
                label_retentionRatio.Text = "追书留存率:无数据";
            }

            label_latelyFollower.Text = "追书人数:" + book.latelyFollower + " 人";
            label_lastChapter.Text = "最后更新章节:" + book.lastChapter;
            label_site.Text = "首发网站:" + book.site;
            textBox_shortIntro.Text = book.shortIntro;
            string url = book.cover;
            int urlStartIndex = url.IndexOf("http:");
            if (urlStartIndex >= 0) {
                url = url.Substring(urlStartIndex);
                picturebox_cover.ImageLocation = url;
            }

            mMixToc = LibZhuiShu.getMixToc(mBook._id);
            mTocs = LibZhuiShu.getTocSummary(mBook._id);
            changeToc(-1);
            this.Show();
            
        }



        void changeToc(int index)
        {
            if (index < 0) {
                //混合源
                if (mMixToc != null) {
                    mChapers = mMixToc.chapters;
                }
                label_toc.Text = "当前书源:混合源";
            } else {
                var toc = mTocs[index];
                var info = LibZhuiShu.getChaperList(toc._id);
                if (info != null) {
                    mChapers = info.chapters;
                }
                label_toc.Text = "当前书源:" + toc.name;
            }

            if (mChapers != null) {
                listview_chapers.BeginUpdate();
                listview_chapers.Items.Clear();
                foreach (var chaper in mChapers) {
                    ListViewItem item = new ListViewItem();
                    item.Text = chaper.title;
                    item.SubItems.Add(chaper.link);
                    listview_chapers.Items.Add(item);
                }
                listview_chapers.EndUpdate();


            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormBookDetail_Load(object sender, EventArgs e)
        {
            listview_chapers.Columns.Add("章节名称", 120);
            listview_chapers.Columns.Add("链接", 280);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_toc_Click(object sender, EventArgs e)
        {
            if (backgroundworker_download.IsBusy) return;
            FormTocList formToc = new FormTocList();
            formToc.ShowTocList(mMixToc, mTocs, (index) => {
                changeToc(index);
            });
        }

        private void button_download_Click(object sender, EventArgs e)
        {

            if (backgroundworker_download.IsBusy && backgroundworker_download.CancellationPending) {
                return;
            }

            if (backgroundworker_download.IsBusy) {
                stopDownload();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件（*.txt）|*.txt|Kindel（*.mobi）|*.mobi";
            sfd.FilterIndex = 0;
            sfd.FileName = mBook.title;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK) {
                string fn = sfd.FileName;
                startDownload(fn);
            }
        }
        private void startDownload(string savePath)
        {
            button_download.Text = "取消";
            label_downloadinfo.Text = "即将开始...";
            progressbar_download.Value = 0;
            progressbar_download.Maximum = mChapers.Length;
            progressbar_download.Visible = true;
            label_downloadinfo.Visible = true;
            backgroundworker_download.RunWorkerAsync(savePath);
        }

        private void stopDownload()
        {
            if (backgroundworker_download.IsBusy) {
                backgroundworker_download.CancelAsync();
                button_download.Text = "正在取消";
            }
        }

        private void downloadComplate()
        {
            progressbar_download.Visible = false;
            label_downloadinfo.Visible = false;
            button_download.Text = "全本下载";
        }


        private void backgroundworker_download_DoWork(object sender, DoWorkEventArgs e)
        {
            var chapters = mChapers;
            var pb = progressbar_download;
            var label = label_downloadinfo;
            string savePath = e.Argument.ToString();
            List<ChapterInfo> chaperInfoList = new List<ChapterInfo>();
            for (int i = 0; i < chapters.Length; i++) {
                if (backgroundworker_download.CancellationPending) return;    
                var chapter = chapters[i];
                float progress = (float)(i + 1) / (float)chapters.Length;
                string info = string.Format("正在下载:{0} {1}/{2} {3:F2}%", chapter.title, i + 1, chapters.Length,
                   progress * 100);
                backgroundworker_download.ReportProgress(i, info);
                try {
                    var chapterInfo = LibZhuiShu.getChapter(chapter.link);
                    if (chapterInfo != null) {
                        chaperInfoList.Add(chapterInfo);
                    } else {
                        MessageBox.Show("下载失败:" + chapter.title);
                        return;
                    }
                } catch (Exception exc) {
                    MessageBox.Show("下载失败,请切换书源后重试:" + exc);
                    return;
                }
                
            }
            backgroundworker_download.ReportProgress(chapters.Length, "正在生成电子书请稍后....");
            string ext = Path.GetExtension(savePath);
            Book book = new Book();
            book.name = mBook.title;
            book.author = mBook.author;
            book.id = mBook._id;
            book.chapters = chaperInfoList.ToArray();
            if (ext.ToLower() == ".txt") {
                Kindlegen.book2Txt(book,savePath);
            } else if (ext.ToLower() == ".mobi") {
                Kindlegen.book2Mobi(book, savePath);
            }
            MessageBox.Show("下载完成,文件保存在:" + savePath);
        }

        private void backgroundworker_download_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            downloadComplate();
        }

        private void backgroundworker_download_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressbar_download.Value = e.ProgressPercentage;
            if (e.UserState != null) {
                label_downloadinfo.Text = e.UserState.ToString();
            }     
        }

        private void listview_chapers_DoubleClick(object sender, EventArgs e)
        {
            if (listview_chapers.SelectedIndices.Count > 0) {
                int index = listview_chapers.SelectedIndices[0];
                var chapter = mChapers[index];
                System.Diagnostics.Process.Start(chapter.link);
            }
        }
    }
}
