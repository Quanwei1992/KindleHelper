namespace KindleHelper
{
    partial class FormSearchResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchResult));
            this.listview_result = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listview_result
            // 
            this.listview_result.FullRowSelect = true;
            this.listview_result.Location = new System.Drawing.Point(12, 12);
            this.listview_result.Name = "listview_result";
            this.listview_result.Size = new System.Drawing.Size(860, 527);
            this.listview_result.TabIndex = 0;
            this.listview_result.UseCompatibleStateImageBehavior = false;
            this.listview_result.View = System.Windows.Forms.View.Details;
            this.listview_result.DoubleClick += new System.EventHandler(this.listview_result_DoubleClick);
            // 
            // FormSearchResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 551);
            this.Controls.Add(this.listview_result);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 590);
            this.MinimumSize = new System.Drawing.Size(900, 590);
            this.Name = "FormSearchResult";
            this.Text = "搜索结果";
            this.Load += new System.EventHandler(this.FormSearchResult_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listview_result;
    }
}