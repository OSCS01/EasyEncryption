namespace EasyEncryption
{
    partial class LogForm
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
            if (disposing && (components != null))
            {
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
            this.LogView = new System.Windows.Forms.ListView();
            this.Filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Owner = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UserDownload = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sharedGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // LogView
            // 
            this.LogView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Filename,
            this.Owner,
            this.UserDownload,
            this.Date,
            this.sharedGroup});
            this.LogView.Location = new System.Drawing.Point(13, 13);
            this.LogView.Name = "LogView";
            this.LogView.Size = new System.Drawing.Size(897, 403);
            this.LogView.TabIndex = 0;
            this.LogView.UseCompatibleStateImageBehavior = false;
            this.LogView.View = System.Windows.Forms.View.Details;
            // 
            // Filename
            // 
            this.Filename.Text = "Filename";
            // 
            // Owner
            // 
            this.Owner.Text = "Owner";
            // 
            // UserDownload
            // 
            this.UserDownload.Text = "Downloaded By";
            // 
            // Date
            // 
            this.Date.Text = "Date";
            // 
            // sharedGroup
            // 
            this.sharedGroup.Text = "Group";
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 482);
            this.Controls.Add(this.LogView);
            this.Name = "LogForm";
            this.Text = "LogForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LogView;
        private System.Windows.Forms.ColumnHeader Filename;
        private System.Windows.Forms.ColumnHeader Owner;
        private System.Windows.Forms.ColumnHeader UserDownload;
        private System.Windows.Forms.ColumnHeader Date;
        private System.Windows.Forms.ColumnHeader sharedGroup;
    }
}