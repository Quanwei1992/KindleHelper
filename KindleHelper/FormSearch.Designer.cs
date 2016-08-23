namespace KindleHelper
{
    partial class FormSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearch));
            this.btn_search = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_search = new System.Windows.Forms.TextBox();
            this.listbox_autocomplate = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(356, 127);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 24);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "搜索";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(114, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(333, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "请输入小说名字,作者名字进行搜索";
            // 
            // textbox_search
            // 
            this.textbox_search.Location = new System.Drawing.Point(144, 128);
            this.textbox_search.Name = "textbox_search";
            this.textbox_search.Size = new System.Drawing.Size(205, 21);
            this.textbox_search.TabIndex = 3;
            this.textbox_search.TextChanged += new System.EventHandler(this.textbox_search_TextChanged);
            // 
            // listbox_autocomplate
            // 
            this.listbox_autocomplate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listbox_autocomplate.FormattingEnabled = true;
            this.listbox_autocomplate.ItemHeight = 12;
            this.listbox_autocomplate.Location = new System.Drawing.Point(145, 150);
            this.listbox_autocomplate.Name = "listbox_autocomplate";
            this.listbox_autocomplate.Size = new System.Drawing.Size(205, 96);
            this.listbox_autocomplate.TabIndex = 4;
            this.listbox_autocomplate.Visible = false;
            this.listbox_autocomplate.SelectedValueChanged += new System.EventHandler(this.listbox_autocomplate_SelectedValueChanged);
            // 
            // FormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 310);
            this.Controls.Add(this.listbox_autocomplate);
            this.Controls.Add(this.textbox_search);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_search);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(593, 349);
            this.Name = "FormSearch";
            this.Text = "Kindle小说下载器(txt,mobi)";
            this.Load += new System.EventHandler(this.FormSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textbox_search;
        private System.Windows.Forms.ListBox listbox_autocomplate;
    }
}