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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.NumericUpDown();
            this.txtFrom = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDownloadParts = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_cover)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).BeginInit();
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
            this.label_name.Click += new System.EventHandler(this.Label1_Click);
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
            this.button_toc.Location = new System.Drawing.Point(424, 326);
            this.button_toc.Name = "button_toc";
            this.button_toc.Size = new System.Drawing.Size(75, 23);
            this.button_toc.TabIndex = 10;
            this.button_toc.Text = "切换";
            this.button_toc.UseVisualStyleBackColor = true;
            this.button_toc.Click += new System.EventHandler(this.Button_toc_Click);
            // 
            // button_download
            // 
            this.button_download.Location = new System.Drawing.Point(424, 350);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(75, 23);
            this.button_download.TabIndex = 11;
            this.button_download.Text = "全本下载";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.Button_download_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "章节列表:";
            // 
            // listview_chapers
            // 
            this.listview_chapers.FullRowSelect = true;
            this.listview_chapers.Location = new System.Drawing.Point(21, 385);
            this.listview_chapers.Name = "listview_chapers";
            this.listview_chapers.Size = new System.Drawing.Size(478, 141);
            this.listview_chapers.TabIndex = 14;
            this.listview_chapers.UseCompatibleStateImageBehavior = false;
            this.listview_chapers.View = System.Windows.Forms.View.Details;
            this.listview_chapers.DoubleClick += new System.EventHandler(this.Listview_chapers_DoubleClick);
            // 
            // progressbar_download
            // 
            this.progressbar_download.Location = new System.Drawing.Point(74, 326);
            this.progressbar_download.Name = "progressbar_download";
            this.progressbar_download.Size = new System.Drawing.Size(335, 26);
            this.progressbar_download.Step = 1;
            this.progressbar_download.TabIndex = 15;
            this.progressbar_download.Visible = false;
            // 
            // label_downloadinfo
            // 
            this.label_downloadinfo.AutoSize = true;
            this.label_downloadinfo.Location = new System.Drawing.Point(86, 311);
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
            this.backgroundworker_download.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Backgroundworker_download_DoWork);
            this.backgroundworker_download.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Backgroundworker_download_ProgressChanged);
            this.backgroundworker_download.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Backgroundworker_download_RunWorkerCompleted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "输入接受的邮箱地址";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(155, 114);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(70, 21);
            this.textBox1.TabIndex = 18;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(155, 12);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(70, 21);
            this.textBox2.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "输入发送的邮箱地址";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(155, 76);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(70, 21);
            this.textBox3.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "输入发送邮箱密码";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(155, 39);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(70, 21);
            this.textBox4.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "输入发送邮件服务器地址";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Location = new System.Drawing.Point(271, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(228, 183);
            this.panel1.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "输入接受的邮箱用户名";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(156, 148);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(70, 21);
            this.textBox5.TabIndex = 26;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(193, 173);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "是否发送";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 361);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 31;
            this.label7.Text = "从：";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(221, 358);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(100, 21);
            this.txtTo.TabIndex = 30;
            this.txtTo.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(92, 358);
            this.txtFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(100, 21);
            this.txtFrom.TabIndex = 29;
            this.txtFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(198, 360);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "到";
            // 
            // btnDownloadParts
            // 
            this.btnDownloadParts.Location = new System.Drawing.Point(424, 297);
            this.btnDownloadParts.Name = "btnDownloadParts";
            this.btnDownloadParts.Size = new System.Drawing.Size(75, 23);
            this.btnDownloadParts.TabIndex = 27;
            this.btnDownloadParts.Text = "部分下载";
            this.btnDownloadParts.UseVisualStyleBackColor = true;
            this.btnDownloadParts.Click += new System.EventHandler(this.btnDownloadParts_Click);
            // 
            // FormBookDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 538);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnDownloadParts);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.panel1);
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
            this.MaximumSize = new System.Drawing.Size(527, 577);
            this.MinimumSize = new System.Drawing.Size(527, 577);
            this.Name = "FormBookDetail";
            this.Text = "书籍详情";
            this.Load += new System.EventHandler(this.FormBookDetail_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.P);
            ((System.ComponentModel.ISupportInitialize)(this.picturebox_cover)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown txtTo;
        private System.Windows.Forms.NumericUpDown txtFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnDownloadParts;
    }
}