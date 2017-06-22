﻿namespace EasyEncryption
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
            this.SelectedFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SelectedSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SelectedPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UploadBtn = new System.Windows.Forms.Button();
            this.contactsbtn = new System.Windows.Forms.Button();
            this.groupsbtn = new System.Windows.Forms.Button();
            this.chatbtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.myFiles = new System.Windows.Forms.ListView();
            this.myFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mySize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.myGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.myOwner = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Refreshbtn = new System.Windows.Forms.Button();
            this.DownloadBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
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
            this.SelectedFilename,
            this.SelectedSize,
            this.SelectedPath});
            this.selectedFiles.Location = new System.Drawing.Point(-8, -11);
            this.selectedFiles.Name = "selectedFiles";
            this.selectedFiles.Size = new System.Drawing.Size(941, 531);
            this.selectedFiles.TabIndex = 1;
            this.selectedFiles.UseCompatibleStateImageBehavior = false;
            this.selectedFiles.View = System.Windows.Forms.View.Details;
            // 
            // SelectedFilename
            // 
            this.SelectedFilename.Text = "Filename";
            this.SelectedFilename.Width = 155;
            // 
            // SelectedSize
            // 
            this.SelectedSize.Text = "Size";
            this.SelectedSize.Width = 155;
            // 
            // SelectedPath
            // 
            this.SelectedPath.Text = "Path";
            this.SelectedPath.Width = 627;
            // 
            // UploadBtn
            // 
            this.UploadBtn.Location = new System.Drawing.Point(879, 830);
            this.UploadBtn.Name = "UploadBtn";
            this.UploadBtn.Size = new System.Drawing.Size(156, 57);
            this.UploadBtn.TabIndex = 3;
            this.UploadBtn.Text = "Upload";
            this.UploadBtn.UseVisualStyleBackColor = true;
            this.UploadBtn.Click += new System.EventHandler(this.uploadbtn_Click);
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
            this.groupsbtn.Click += new System.EventHandler(this.groupsbtn_Click);
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
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
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
            this.myFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.myFilename,
            this.mySize,
            this.myGroup,
            this.myOwner});
            this.myFiles.Location = new System.Drawing.Point(-8, -11);
            this.myFiles.MultiSelect = false;
            this.myFiles.Name = "myFiles";
            this.myFiles.Size = new System.Drawing.Size(941, 531);
            this.myFiles.TabIndex = 2;
            this.myFiles.UseCompatibleStateImageBehavior = false;
            this.myFiles.View = System.Windows.Forms.View.Details;
            // 
            // myFilename
            // 
            this.myFilename.Text = "Filename";
            this.myFilename.Width = 150;
            // 
            // mySize
            // 
            this.mySize.Text = "Size";
            this.mySize.Width = 150;
            // 
            // myGroup
            // 
            this.myGroup.Text = "Group";
            this.myGroup.Width = 150;
            // 
            // myOwner
            // 
            this.myOwner.Text = "Owner";
            this.myOwner.Width = 487;
            // 
            // Refreshbtn
            // 
            this.Refreshbtn.Location = new System.Drawing.Point(1052, 246);
            this.Refreshbtn.Name = "Refreshbtn";
            this.Refreshbtn.Size = new System.Drawing.Size(109, 44);
            this.Refreshbtn.TabIndex = 8;
            this.Refreshbtn.Text = "Refresh";
            this.Refreshbtn.UseVisualStyleBackColor = true;
            this.Refreshbtn.Visible = false;
            this.Refreshbtn.Click += new System.EventHandler(this.Refreshbtn_Click);
            // 
            // DownloadBtn
            // 
            this.DownloadBtn.Location = new System.Drawing.Point(690, 830);
            this.DownloadBtn.Name = "DownloadBtn";
            this.DownloadBtn.Size = new System.Drawing.Size(156, 57);
            this.DownloadBtn.TabIndex = 9;
            this.DownloadBtn.Text = "Download";
            this.DownloadBtn.UseVisualStyleBackColor = true;
            this.DownloadBtn.Click += new System.EventHandler(this.DownloadBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Location = new System.Drawing.Point(1052, 296);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(109, 44);
            this.clearBtn.TabIndex = 10;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 919);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.DownloadBtn);
            this.Controls.Add(this.Refreshbtn);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.chatbtn);
            this.Controls.Add(this.groupsbtn);
            this.Controls.Add(this.contactsbtn);
            this.Controls.Add(this.UploadBtn);
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
        private System.Windows.Forms.Button UploadBtn;
        private System.Windows.Forms.Button contactsbtn;
        private System.Windows.Forms.Button groupsbtn;
        private System.Windows.Forms.Button chatbtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView myFiles;
        private System.Windows.Forms.ColumnHeader SelectedFilename;
        private System.Windows.Forms.ColumnHeader SelectedSize;
        private System.Windows.Forms.ColumnHeader SelectedPath;
        private System.Windows.Forms.ColumnHeader myFilename;
        private System.Windows.Forms.ColumnHeader mySize;
        private System.Windows.Forms.ColumnHeader myGroup;
        private System.Windows.Forms.ColumnHeader myOwner;
        private System.Windows.Forms.Button Refreshbtn;
        private System.Windows.Forms.Button DownloadBtn;
        private System.Windows.Forms.Button clearBtn;
    }
}