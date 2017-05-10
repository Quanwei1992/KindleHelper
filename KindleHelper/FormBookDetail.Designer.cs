namespace KindleHelper
{
    partial class FormBookDetail
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBookDetail));
            this.picturebox_cover = new System.Windows.Forms.PictureBox();
            this.label_name = new System.Windows.Forms.Label();
            this.label_baseinfo = new System.Windows.Forms.Label();
            this.label_retentionRatio = new System.Windows.Forms.Label();
            this.label_latelyFollower = new System.Windows.Forms.Label();
            this.label_lastChapter = new System.Windows.Forms.Label();
            this.label_site = new System.Windows.Forms.Label();
            this.label_jianjie = new System.Windows.Forms.Label();
            this.textBox_shortIntro = new System.Windows.Forms.TextBox();
            this.label_toc = new System.Windows.Forms.Label();
            this.button_toc = new System.Windows.Forms.Button();
            this.button_download = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listview_chapers = new System.Windows.Forms.ListView();
            this.progressbar_download = new System.Windows.Forms.ProgressBar();
            this.label_downloadinfo = new System.Windows.Forms.Label();
            this.backgroundworker_download = new System.ComponentModel.BackgroundWorker();
            this.btnDownloadParts = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.NumericUpDown();
            this.txtTo = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblChapterCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_cover)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).BeginInit();
            this.SuspendLayout();
            // 
            // picturebox_cover
            // 
            this.picturebox_cover.Location = new System.Drawing.Point(12, 12);
            this.picturebox_cover.Name = "picturebox_cover";
            this.picturebox_cover.Size = new System.Drawing.Size(120, 152);
            this.picturebox_cover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picturebox_cover.TabIndex = 0;
            this.picturebox_cover.TabStop = false;
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_name.Location = new System.Drawing.Point(139, 13);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(88, 25);
            this.label_name.TabIndex = 1;
            this.label_name.Text = "完美世界";
            this.label_name.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_baseinfo
            // 
            this.label_baseinfo.AutoSize = true;
            this.label_baseinfo.Location = new System.Drawing.Point(144, 51);
            this.label_baseinfo.Name = "label_baseinfo";
            this.label_baseinfo.Size = new System.Drawing.Size(131, 12);
            this.label_baseinfo.TabIndex = 2;
            this.label_baseinfo.Text = "天使 | 玄幻 | 64 万字";
            // 
            // label_retentionRatio
            // 
            this.label_retentionRatio.AutoSize = true;
            this.label_retentionRatio.Location = new System.Drawing.Point(144, 73);
            this.label_retentionRatio.Name = "label_retentionRatio";
            this.label_retentionRatio.Size = new System.Drawing.Size(101, 12);
            this.label_retentionRatio.TabIndex = 3;
            this.label_retentionRatio.Text = "读者留存率:19.4%";
            // 
            // label_latelyFollower
            // 
            this.label_latelyFollower.AutoSize = true;
            this.label_latelyFollower.Location = new System.Drawing.Point(144, 94);
            this.label_latelyFollower.Name = "label_latelyFollower";
            this.label_latelyFollower.Size = new System.Drawing.Size(77, 12);
            this.label_latelyFollower.TabIndex = 4;
            this.label_latelyFollower.Text = "追书人数:251";
            // 
            // label_lastChapter
            // 
            this.label_lastChapter.AutoSize = true;
            this.label_lastChapter.Location = new System.Drawing.Point(144, 115);
            this.label_lastChapter.Name = "label_lastChapter";
            this.label_lastChapter.Size = new System.Drawing.Size(101, 12);
            this.label_lastChapter.TabIndex = 5;
            this.label_lastChapter.Text = "最近更新:第100章";
            // 
            // label_site
            // 
            this.label_site.AutoSize = true;
            this.label_site.Location = new System.Drawing.Point(144, 136);
            this.label_site.Name = "label_site";
            this.label_site.Size = new System.Drawing.Size(59, 12);
            this.label_site.TabIndex = 6;
            this.label_site.Text = "网站:起点";
            // 
            // label_jianjie
            // 
            this.label_jianjie.AutoSize = true;
            this.label_jianjie.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_jianjie.Location = new System.Drawing.Point(13, 167);
            this.label_jianjie.Name = "label_jianjie";
            this.label_jianjie.Size = new System.Drawing.Size(55, 25);
            this.label_jianjie.TabIndex = 7;
            this.label_jianjie.Text = "简介:";
            // 
            // textBox_shortIntro
            // 
            this.textBox_shortIntro.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_shortIntro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_shortIntro.Location = new System.Drawing.Point(12, 195);
            this.textBox_shortIntro.Multiline = true;
            this.textBox_shortIntro.Name = "textBox_shortIntro";
            this.textBox_shortIntro.ReadOnly = true;
            this.textBox_shortIntro.Size = new System.Drawing.Size(487, 139);
            this.textBox_shortIntro.TabIndex = 8;
            this.textBox_shortIntro.Text = "一粒尘可填海，一根草斩尽日月星辰，弹指间天翻地覆";
            // 
            // label_toc
            // 
            this.label_toc.AutoSize = true;
            this.label_toc.Location = new System.Drawing.Point(144, 157);
            this.label_toc.Name = "label_toc";
            this.label_toc.Size = new System.Drawing.Size(95, 12);
            this.label_toc.TabIndex = 9;
            this.label_toc.Text = "当前书源:混合源";
            // 
            // button_toc
            // 
            this.button_toc.Location = new System.Drawing.Point(246, 151);
            this.button_toc.Name = "button_toc";
            this.button_toc.Size = new System.Drawing.Size(75, 23);
            this.button_toc.TabIndex = 10;
            this.button_toc.Text = "切换";
            this.button_toc.UseVisualStyleBackColor = true;
            this.button_toc.Click += new System.EventHandler(this.button_toc_Click);
            // 
            // button_download
            // 
            this.button_download.Location = new System.Drawing.Point(424, 350);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(75, 23);
            this.button_download.TabIndex = 11;
            this.button_download.Text = "全本下载";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 358);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "章节列表:";
            // 
            // listview_chapers
            // 
            this.listview_chapers.FullRowSelect = true;
            this.listview_chapers.Location = new System.Drawing.Point(21, 428);
            this.listview_chapers.Name = "listview_chapers";
            this.listview_chapers.Size = new System.Drawing.Size(478, 141);
            this.listview_chapers.TabIndex = 14;
            this.listview_chapers.UseCompatibleStateImageBehavior = false;
            this.listview_chapers.View = System.Windows.Forms.View.Details;
            this.listview_chapers.DoubleClick += new System.EventHandler(this.listview_chapers_DoubleClick);
            // 
            // progressbar_download
            // 
            this.progressbar_download.Location = new System.Drawing.Point(77, 350);
            this.progressbar_download.Name = "progressbar_download";
            this.progressbar_download.Size = new System.Drawing.Size(335, 23);
            this.progressbar_download.Step = 1;
            this.progressbar_download.TabIndex = 15;
            this.progressbar_download.Visible = false;
            // 
            // label_downloadinfo
            // 
            this.label_downloadinfo.AutoSize = true;
            this.label_downloadinfo.Location = new System.Drawing.Point(75, 337);
            this.label_downloadinfo.Name = "label_downloadinfo";
            this.label_downloadinfo.Size = new System.Drawing.Size(179, 12);
            this.label_downloadinfo.TabIndex = 16;
            this.label_downloadinfo.Text = "正在下载:第三百章 111/999 95%";
            this.label_downloadinfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_downloadinfo.Visible = false;
            // 
            // backgroundworker_download
            // 
            this.backgroundworker_download.WorkerReportsProgress = true;
            this.backgroundworker_download.WorkerSupportsCancellation = true;
            this.backgroundworker_download.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundworker_download_DoWork);
            this.backgroundworker_download.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundworker_download_ProgressChanged);
            this.backgroundworker_download.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundworker_download_RunWorkerCompleted);
            // 
            // btnDownloadParts
            // 
            this.btnDownloadParts.Location = new System.Drawing.Point(424, 380);
            this.btnDownloadParts.Name = "btnDownloadParts";
            this.btnDownloadParts.Size = new System.Drawing.Size(75, 23);
            this.btnDownloadParts.TabIndex = 17;
            this.btnDownloadParts.Text = "部分下载";
            this.btnDownloadParts.UseVisualStyleBackColor = true;
            this.btnDownloadParts.Click += new System.EventHandler(this.btnDownloadParts_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(191, 391);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "到";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(85, 385);
            this.txtFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(100, 21);
            this.txtFrom.TabIndex = 22;
            this.txtFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtFrom.ValueChanged += new System.EventHandler(this.txtFrom_ValueChanged);
            this.txtFrom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFrom_KeyUp);
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(214, 385);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(100, 21);
            this.txtTo.TabIndex = 23;
            this.txtTo.ValueChanged += new System.EventHandler(this.txtTo_ValueChanged);
            this.txtTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTo_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 391);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "从：";
            // 
            // lblChapterCount
            // 
            this.lblChapterCount.AutoSize = true;
            this.lblChapterCount.Location = new System.Drawing.Point(320, 391);
            this.lblChapterCount.Name = "lblChapterCount";
            this.lblChapterCount.Size = new System.Drawing.Size(0, 12);
            this.lblChapterCount.TabIndex = 25;
            // 
            // FormBookDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 581);
            this.Controls.Add(this.lblChapterCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDownloadParts);
            this.Controls.Add(this.label_downloadinfo);
            this.Controls.Add(this.progressbar_download);
            this.Controls.Add(this.listview_chapers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_download);
            this.Controls.Add(this.button_toc);
            this.Controls.Add(this.label_toc);
            this.Controls.Add(this.textBox_shortIntro);
            this.Controls.Add(this.label_jianjie);
            this.Controls.Add(this.label_site);
            this.Controls.Add(this.label_lastChapter);
            this.Controls.Add(this.label_latelyFollower);
            this.Controls.Add(this.label_retentionRatio);
            this.Controls.Add(this.label_baseinfo);
            this.Controls.Add(this.label_name);
            this.Controls.Add(this.picturebox_cover);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(527, 620);
            this.MinimumSize = new System.Drawing.Size(527, 620);
            this.Name = "FormBookDetail";
            this.Text = "书籍详情";
            this.Load += new System.EventHandler(this.FormBookDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_cover)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picturebox_cover;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Label label_baseinfo;
        private System.Windows.Forms.Label label_retentionRatio;
        private System.Windows.Forms.Label label_latelyFollower;
        private System.Windows.Forms.Label label_lastChapter;
        private System.Windows.Forms.Label label_site;
        private System.Windows.Forms.Label label_jianjie;
        private System.Windows.Forms.TextBox textBox_shortIntro;
        private System.Windows.Forms.Label label_toc;
        private System.Windows.Forms.Button button_toc;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listview_chapers;
        private System.Windows.Forms.ProgressBar progressbar_download;
        private System.Windows.Forms.Label label_downloadinfo;
        private System.ComponentModel.BackgroundWorker backgroundworker_download;
        private System.Windows.Forms.Button btnDownloadParts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtFrom;
        private System.Windows.Forms.NumericUpDown txtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblChapterCount;
    }
}