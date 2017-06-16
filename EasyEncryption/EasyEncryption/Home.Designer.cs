namespace EasyEncryption
{
    partial class Home
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
            this.AddFiles = new System.Windows.Forms.Button();
            this.selectedFiles = new System.Windows.Forms.ListView();
            this.transferbtn = new System.Windows.Forms.Button();
            this.contactsbtn = new System.Windows.Forms.Button();
            this.groupsbtn = new System.Windows.Forms.Button();
            this.chatbtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.myFiles = new System.Windows.Forms.ListView();
            this.Filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Path = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddFiles
            // 
            this.AddFiles.Location = new System.Drawing.Point(94, 95);
            this.AddFiles.Name = "AddFiles";
            this.AddFiles.Size = new System.Drawing.Size(128, 44);
            this.AddFiles.TabIndex = 0;
            this.AddFiles.Text = "Files";
            this.AddFiles.UseVisualStyleBackColor = true;
            this.AddFiles.Click += new System.EventHandler(this.AddFiles_Click);
            // 
            // selectedFiles
            // 
            this.selectedFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Filename,
            this.Size,
            this.Path});
            this.selectedFiles.Location = new System.Drawing.Point(-8, -11);
            this.selectedFiles.Name = "selectedFiles";
            this.selectedFiles.Size = new System.Drawing.Size(941, 531);
            this.selectedFiles.TabIndex = 1;
            this.selectedFiles.UseCompatibleStateImageBehavior = false;
            this.selectedFiles.View = System.Windows.Forms.View.Details;
            // 
            // transferbtn
            // 
            this.transferbtn.Location = new System.Drawing.Point(879, 830);
            this.transferbtn.Name = "transferbtn";
            this.transferbtn.Size = new System.Drawing.Size(156, 57);
            this.transferbtn.TabIndex = 3;
            this.transferbtn.Text = "Transfer";
            this.transferbtn.UseVisualStyleBackColor = true;
            this.transferbtn.Click += new System.EventHandler(this.transferbtn_Click);
            // 
            // contactsbtn
            // 
            this.contactsbtn.Location = new System.Drawing.Point(359, 95);
            this.contactsbtn.Name = "contactsbtn";
            this.contactsbtn.Size = new System.Drawing.Size(128, 44);
            this.contactsbtn.TabIndex = 4;
            this.contactsbtn.Text = "Contacts";
            this.contactsbtn.UseVisualStyleBackColor = true;
            // 
            // groupsbtn
            // 
            this.groupsbtn.Location = new System.Drawing.Point(626, 95);
            this.groupsbtn.Name = "groupsbtn";
            this.groupsbtn.Size = new System.Drawing.Size(128, 44);
            this.groupsbtn.TabIndex = 5;
            this.groupsbtn.Text = "Groups";
            this.groupsbtn.UseVisualStyleBackColor = true;
            // 
            // chatbtn
            // 
            this.chatbtn.Location = new System.Drawing.Point(907, 95);
            this.chatbtn.Name = "chatbtn";
            this.chatbtn.Size = new System.Drawing.Size(128, 44);
            this.chatbtn.TabIndex = 6;
            this.chatbtn.Text = "Chat";
            this.chatbtn.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(94, 207);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(941, 559);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.selectedFiles);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(925, 512);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Selected Files";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.myFiles);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(925, 512);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "My Files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // myFiles
            // 
            this.myFiles.Location = new System.Drawing.Point(-8, -11);
            this.myFiles.Name = "myFiles";
            this.myFiles.Size = new System.Drawing.Size(941, 531);
            this.myFiles.TabIndex = 2;
            this.myFiles.UseCompatibleStateImageBehavior = false;
            // 
            // Filename
            // 
            this.Filename.Text = "Filename";
            this.Filename.Width = 155;
            // 
            // Size
            // 
            this.Size.Text = "Size";
            this.Size.Width = 155;
            // 
            // Path
            // 
            this.Path.Text = "Path";
            this.Path.Width = 627;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 919);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.chatbtn);
            this.Controls.Add(this.groupsbtn);
            this.Controls.Add(this.contactsbtn);
            this.Controls.Add(this.transferbtn);
            this.Controls.Add(this.AddFiles);
            this.Name = "Home";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddFiles;
        private System.Windows.Forms.ListView selectedFiles;
        private System.Windows.Forms.Button transferbtn;
        private System.Windows.Forms.Button contactsbtn;
        private System.Windows.Forms.Button groupsbtn;
        private System.Windows.Forms.Button chatbtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView myFiles;
        private System.Windows.Forms.ColumnHeader Filename;
        private System.Windows.Forms.ColumnHeader Size;
        private System.Windows.Forms.ColumnHeader Path;
    }
}