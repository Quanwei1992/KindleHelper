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
using System.Diagnostics;
using KindleSender.Service;
using GrapchLibrary;
using KindleHelper.lib;
namespace KindleHelper
{
    public partial class FormBookDetail : Form
    {
        public FormBookDetail()
        {
            InitializeComponent();
            a = Width;
            b = Height;
        }
        QueryBookInfo mBook;
        tocChaperInfo[] mChapers;
        TocSummmaryInfo[] mTocs;
        MixTocInfo mMixToc;
        List<tocChaperInfo> preDownLoadChapters = new List<tocChaperInfo>();
        int a, b;
        GrapchHelper c;
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
            ChangeToc(-1);
            this.Show();
            
        }



        void ChangeToc(int index)
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
                    ListViewItem item = new ListViewItem()
                    {
                        Text = chaper.title
                    };
                    item.SubItems.Add(chaper.link);
                    listview_chapers.Items.Add(item);
                }
                listview_chapers.EndUpdate();


            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void FormBookDetail_Load(object sender, EventArgs e)
        {
            listview_chapers.Columns.Add("章节名称", 120);
            listview_chapers.Columns.Add("链接", 280);
            LibImport.Class1 c = new LibImport.Class1();
            if (File.Exists(c.Path))
            {
                str=c.Read();
            }
            else
            {
                c.CreatAndWriteFile();
            }
        }
        internal static string str = "";
        private void Button_toc_Click(object sender, EventArgs e)
        {
            if (backgroundworker_download.IsBusy) return;
            FormTocList formToc = new FormTocList();
            formToc.ShowTocList(mMixToc, mTocs, (index) => {
                ChangeToc(index);
            });
        }

        private void Button_download_Click(object sender, EventArgs e)
        {

            if (backgroundworker_download.IsBusy && backgroundworker_download.CancellationPending) {
                return;
            }

            if (backgroundworker_download.IsBusy) {
                StopDownload();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Kindel（*.mobi）|*.mobi|文本文件（*.txt）|*.txt",
                FilterIndex = 0,
                FileName = mBook.title,
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() == DialogResult.OK) {
                string fn = sfd.FileName;
                StartDownload(fn);
            }
        }
        private void StartDownload(string savePath)
        {
            button_download.Text = "取消";
            label_downloadinfo.Text = "即将开始...";
            progressbar_download.Value = 0;
            progressbar_download.Maximum = mChapers.Length;
            progressbar_download.Visible = true;
            label_downloadinfo.Visible = true;
            backgroundworker_download.RunWorkerAsync(savePath);
        }

        private void StopDownload()
        {
            if (backgroundworker_download.IsBusy) {
                backgroundworker_download.CancelAsync();
                button_download.Text = "正在取消";
            }
        }

        private void DownloadComplate()
        {
            progressbar_download.Visible = false;
            label_downloadinfo.Visible = false;
           
            
            button_download.Text = "全本下载";
        }


        private void Backgroundworker_download_DoWork(object sender, DoWorkEventArgs e)
        {
            var chapters = mChapers;
            var pb = progressbar_download;
            var label = label_downloadinfo;
            string savePath = e.Argument.ToString();
            List<ChapterInfo> chaperInfoList = new List<ChapterInfo>();
            for (int i = 0; i < chapters.Length; i++) {
                if (backgroundworker_download.CancellationPending) return;
                var chapter = chapters[i];
                float progress = (i + 1) / (float)chapters.Length;
                string info = string.Format("正在下载:{0} {1}/{2} {3:F2}%", chapter.title, i + 1, chapters.Length+1,
                   progress * 100);
                backgroundworker_download.ReportProgress(i, info);

                while (true) {
                    bool downloadSucess = false;
                    string errMsg = "";
                    for (int j = 0; j < 3; j++) {
                        try {
                            var chapterInfo = LibZhuiShu.getChapter(chapter.link);
                            if (chapterInfo != null) {
                                chaperInfoList.Add(chapterInfo);
                                downloadSucess = true;
                                break;
                            }
                        } catch(Exception ex) {
                            errMsg = ex.Message;
                            loglibrary.LogHelper.Error(ex);
                            loglibrary.LogHelper.Flush();
                        }
                    }
                    if (!downloadSucess) {
                        var result = MessageBox.Show(errMsg, "章节 " + chapter.title + " 下载失败", MessageBoxButtons.AbortRetryIgnore);
                        if (result == DialogResult.Abort) {
                            return;
                        } else if (result == DialogResult.Ignore) {
                            var emptyChaper = new ChapterInfo()
                            {
                                title = chapter.title,
                                body = "本章下载失败了，失败原因:\n " + errMsg
                            };
                            chaperInfoList.Add(emptyChaper);
                            downloadSucess = true;
                            break;
                        }

                    } else {
                        break;
                    }
                }
            }
            backgroundworker_download.ReportProgress(chapters.Length, "正在生成电子书请稍后....");
            string ext = Path.GetExtension(savePath);
            Book book = new Book()
            {
                name = mBook.title,
                author = mBook.author,
                id = mBook._id,
                chapters = chaperInfoList.ToArray()
            };
            if (ext.ToLower() == ".txt") {
                Kindlegen.Book2Txt(book,savePath);
            } else if (ext.ToLower() == ".mobi") {
                Kindlegen.Book2Mobi(book, savePath);
            }
            MessageBox.Show("下载完成,文件保存在:" + savePath);
            if (checkBox1.Checked)
            {
            try
            {
                KindleSender.Service.Program.Main();
                Configuration config = new Configuration()
                {
                        SmtpServer = textBox4.Text,
                        SmtpPort = 25,
                        SmtpPassword = textBox3.Text,
                        SmtpUserName = textBox5.Text,
                        KindleMail = textBox1.Text,
                        FolderPath = savePath.Replace(Path.GetFileName(savePath),string.Empty)
                };
                FileSender sen = new FileSender(config);
                sen.Send(savePath);
                MessageBox.Show("已完成！");
            }
            catch (Exception ee)
            {
                loglibrary.LogHelper.Error(ee);
                loglibrary.LogHelper.Flush();
                MessageBox.Show("错误！\n 错误信息为：" + ee.Message);
            }
            }
        }

        private void Backgroundworker_download_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DownloadComplate();
        }

        private void Backgroundworker_download_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressbar_download.Value = e.ProgressPercentage;
            if (e.UserState != null) {
                label_downloadinfo.Text = e.UserState.ToString();
            }     
        }

        private void Listview_chapers_DoubleClick(object sender, EventArgs e)
        {
            if (listview_chapers.SelectedIndices.Count > 0) {
                int index = listview_chapers.SelectedIndices[0];
                var chapter = mChapers[index];
                Process.Start(chapter.link);
            }
        }

        private void P(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            c = new GrapchHelper(g);
            c.Draw(b, a);
            g.Dispose();
        }
        private void DownloadClik()
        {
            if (backgroundworker_download.IsBusy && backgroundworker_download.CancellationPending)
            {
                return;
            }

            if (backgroundworker_download.IsBusy)
            {
                stopDownload();
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Kindel（*.mobi）|*.mobi|文本文件（*.txt）|*.txt";
            sfd.FilterIndex = 0;
            sfd.FileName = mBook.title;
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
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
            if (backgroundworker_download.IsBusy)
            {
                backgroundworker_download.CancelAsync();
                button_download.Text = "正在取消";
            }
        }
        private void btnDownloadParts_Click(object sender, EventArgs e)
        {
            GetPreDownLoadChapters();

            DownloadClik();
        }
        private void GetPreDownLoadChapters()
        {
            int fromChapter = (int)txtFrom.Value - 1;
            int toChapter = (int)txtTo.Value - 1;
            if (fromChapter > toChapter)
            {
                MessageBox.Show("开始章节不能大于结束章节");
                return;
            }
            preDownLoadChapters.Clear();
            Array v = Array.CreateInstance(typeof(tocChaperInfo), toChapter - fromChapter);
            Array.Copy(mChapers, v, toChapter - fromChapter);
            preDownLoadChapters.AddRange((tocChaperInfo[])Convert.ChangeType(v,typeof(tocChaperInfo[])));
            mChapers = preDownLoadChapters.ToArray();
            //for (int i = fromChapter; i < toChapter; i++)
            //{
                
            //    preDownLoadChapters.Add(mChapers[i]);
            //}

        }

    }
}
