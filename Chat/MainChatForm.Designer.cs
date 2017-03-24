namespace Chat
{
    partial class MainChatForm
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
            this.richTextBoxChat = new System.Windows.Forms.RichTextBox();
            this.richTextBoMessage = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.listBoxUsers = new System.Windows.Forms.ListBox();
            this.buttonWhisper = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxChat
            // 
            this.richTextBoxChat.Location = new System.Drawing.Point(228, 59);
            this.richTextBoxChat.Name = "richTextBoxChat";
            this.richTextBoxChat.ReadOnly = true;
            this.richTextBoxChat.Size = new System.Drawing.Size(452, 291);
            this.richTextBoxChat.TabIndex = 0;
            this.richTextBoxChat.Text = "";
            // 
            // richTextBoMessage
            // 
            this.richTextBoMessage.Location = new System.Drawing.Point(229, 371);
            this.richTextBoMessage.Name = "richTextBoMessage";
            this.richTextBoMessage.Size = new System.Drawing.Size(366, 79);
            this.richTextBoMessage.TabIndex = 1;
            this.richTextBoMessage.Text = "";
            this.richTextBoMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBoMessage_KeyPress);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(601, 371);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(78, 78);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "SEND";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // listBoxUsers
            // 
            this.listBoxUsers.FormattingEnabled = true;
            this.listBoxUsers.Location = new System.Drawing.Point(30, 59);
            this.listBoxUsers.Name = "listBoxUsers";
            this.listBoxUsers.Size = new System.Drawing.Size(175, 290);
            this.listBoxUsers.TabIndex = 3;
            // 
            // buttonWhisper
            // 
            this.buttonWhisper.Location = new System.Drawing.Point(30, 355);
            this.buttonWhisper.Name = "buttonWhisper";
            this.buttonWhisper.Size = new System.Drawing.Size(175, 28);
            this.buttonWhisper.TabIndex = 4;
            this.buttonWhisper.Text = "Whisper";
            this.buttonWhisper.UseVisualStyleBackColor = true;
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(59, 405);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(104, 31);
            this.buttonSettings.TabIndex = 5;
            this.buttonSettings.Text = "Settings...";
            this.buttonSettings.UseVisualStyleBackColor = true;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(59, 447);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(103, 28);
            this.buttonDisconnect.TabIndex = 6;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // MainChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 490);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.buttonWhisper);
            this.Controls.Add(this.listBoxUsers);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.richTextBoMessage);
            this.Controls.Add(this.richTextBoxChat);
            this.Name = "MainChatForm";
            this.Text = "IRC Chat client";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxChat;
        private System.Windows.Forms.RichTextBox richTextBoMessage;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ListBox listBoxUsers;
        private System.Windows.Forms.Button buttonWhisper;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonDisconnect;
    }
}