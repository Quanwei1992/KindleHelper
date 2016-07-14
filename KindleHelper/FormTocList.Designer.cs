namespace KindleHelper
{
    partial class FormTocList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTocList));
            this.listview_toc = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listview_toc
            // 
            this.listview_toc.FullRowSelect = true;
            this.listview_toc.Location = new System.Drawing.Point(12, 12);
            this.listview_toc.Name = "listview_toc";
            this.listview_toc.Size = new System.Drawing.Size(548, 453);
            this.listview_toc.TabIndex = 0;
            this.listview_toc.UseCompatibleStateImageBehavior = false;
            this.listview_toc.View = System.Windows.Forms.View.Details;
            this.listview_toc.DoubleClick += new System.EventHandler(this.listview_toc_DoubleClick);
            // 
            // FormTocList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 477);
            this.Controls.Add(this.listview_toc);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(588, 516);
            this.MinimumSize = new System.Drawing.Size(588, 516);
            this.Name = "FormTocList";
            this.Text = "选择书源";
            this.Load += new System.EventHandler(this.FormTocList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listview_toc;
    }
}