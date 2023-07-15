namespace PROJECT_TRANSFER_FILE
{
    partial class Controller
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
            this.btn_download = new System.Windows.Forms.Button();
            this.btn_filebrowser = new System.Windows.Forms.Button();
            this.lwFile = new System.Windows.Forms.ListView();
            this.btn_upload = new System.Windows.Forms.Button();
            this.process1 = new System.Diagnostics.Process();
            this.tbLink = new System.Windows.Forms.TextBox();
            this.lbFilePath = new System.Windows.Forms.Label();
            this.lw_friend = new System.Windows.Forms.ListView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lb_token = new System.Windows.Forms.Label();
            this.tbToken = new System.Windows.Forms.TextBox();
            this.btnAuto = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUdtime = new System.Windows.Forms.TextBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn_download
            // 
            this.btn_download.Location = new System.Drawing.Point(12, 445);
            this.btn_download.Name = "btn_download";
            this.btn_download.Size = new System.Drawing.Size(138, 50);
            this.btn_download.TabIndex = 1;
            this.btn_download.Text = "Download";
            this.btn_download.UseVisualStyleBackColor = true;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // btn_filebrowser
            // 
            this.btn_filebrowser.Location = new System.Drawing.Point(336, 8);
            this.btn_filebrowser.Name = "btn_filebrowser";
            this.btn_filebrowser.Size = new System.Drawing.Size(112, 39);
            this.btn_filebrowser.TabIndex = 2;
            this.btn_filebrowser.Text = "...";
            this.btn_filebrowser.UseVisualStyleBackColor = true;
            this.btn_filebrowser.Click += new System.EventHandler(this.btn_filebrowser_Click);
            // 
            // lwFile
            // 
            this.lwFile.HideSelection = false;
            this.lwFile.LabelEdit = true;
            this.lwFile.Location = new System.Drawing.Point(13, 53);
            this.lwFile.MultiSelect = false;
            this.lwFile.Name = "lwFile";
            this.lwFile.Size = new System.Drawing.Size(762, 386);
            this.lwFile.TabIndex = 6;
            this.lwFile.UseCompatibleStateImageBehavior = false;
            this.lwFile.View = System.Windows.Forms.View.Details;
            this.lwFile.SelectedIndexChanged += new System.EventHandler(this.lwUrl_SelectedIndexChanged);
            // 
            // btn_upload
            // 
            this.btn_upload.Location = new System.Drawing.Point(156, 445);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(141, 50);
            this.btn_upload.TabIndex = 1;
            this.btn_upload.Text = "Upload";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // tbLink
            // 
            this.tbLink.Location = new System.Drawing.Point(12, 18);
            this.tbLink.Name = "tbLink";
            this.tbLink.ReadOnly = true;
            this.tbLink.Size = new System.Drawing.Size(318, 22);
            this.tbLink.TabIndex = 9;
            // 
            // lbFilePath
            // 
            this.lbFilePath.AutoSize = true;
            this.lbFilePath.Location = new System.Drawing.Point(454, 18);
            this.lbFilePath.Name = "lbFilePath";
            this.lbFilePath.Size = new System.Drawing.Size(33, 16);
            this.lbFilePath.TabIndex = 7;
            this.lbFilePath.Text = "path";
            // 
            // lw_friend
            // 
            this.lw_friend.HideSelection = false;
            this.lw_friend.LabelEdit = true;
            this.lw_friend.Location = new System.Drawing.Point(781, 53);
            this.lw_friend.MultiSelect = false;
            this.lw_friend.Name = "lw_friend";
            this.lw_friend.Size = new System.Drawing.Size(763, 386);
            this.lw_friend.TabIndex = 6;
            this.lw_friend.UseCompatibleStateImageBehavior = false;
            this.lw_friend.View = System.Windows.Forms.View.Details;
            this.lw_friend.SelectedIndexChanged += new System.EventHandler(this.lwUrl_SelectedIndexChanged);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(781, 445);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(145, 50);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRS_Click);
            // 
            // lb_token
            // 
            this.lb_token.AutoSize = true;
            this.lb_token.Location = new System.Drawing.Point(9, 549);
            this.lb_token.Name = "lb_token";
            this.lb_token.Size = new System.Drawing.Size(75, 16);
            this.lb_token.TabIndex = 7;
            this.lb_token.Text = "your token: ";
            // 
            // tbToken
            // 
            this.tbToken.Location = new System.Drawing.Point(90, 549);
            this.tbToken.Name = "tbToken";
            this.tbToken.ReadOnly = true;
            this.tbToken.Size = new System.Drawing.Size(376, 22);
            this.tbToken.TabIndex = 9;
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(1479, 445);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(65, 28);
            this.btnAuto.TabIndex = 5;
            this.btnAuto.Text = "Auto";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1338, 549);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "update at:";
            // 
            // tbUdtime
            // 
            this.tbUdtime.Location = new System.Drawing.Point(1410, 546);
            this.tbUdtime.Name = "tbUdtime";
            this.tbUdtime.ReadOnly = true;
            this.tbUdtime.Size = new System.Drawing.Size(183, 22);
            this.tbUdtime.TabIndex = 9;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(9, 523);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(102, 16);
            this.lbUsername.TabIndex = 7;
            this.lbUsername.Text = "your username: ";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(111, 523);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.ReadOnly = true;
            this.tbUsername.Size = new System.Drawing.Size(376, 22);
            this.tbUsername.TabIndex = 9;
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1605, 574);
            this.Controls.Add(this.tbUsername);
            this.Controls.Add(this.tbToken);
            this.Controls.Add(this.tbUdtime);
            this.Controls.Add(this.tbLink);
            this.Controls.Add(this.lbUsername);
            this.Controls.Add(this.lb_token);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbFilePath);
            this.Controls.Add(this.lw_friend);
            this.Controls.Add(this.lwFile);
            this.Controls.Add(this.btnAuto);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btn_filebrowser);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.btn_download);
            this.Name = "Controller";
            this.Text = "TRANSFER FILE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Controller_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_download;
        private System.Windows.Forms.Button btn_filebrowser;
        private System.Windows.Forms.ListView lwFile;
        private System.Windows.Forms.Button btn_upload;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.TextBox tbLink;
        private System.Windows.Forms.Label lbFilePath;
        private System.Windows.Forms.ListView lw_friend;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lb_token;
        private System.Windows.Forms.TextBox tbToken;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.TextBox tbUdtime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label lbUsername;
    }
}

