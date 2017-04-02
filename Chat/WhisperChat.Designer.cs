namespace Chat
{
    partial class WhisperChat
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
            this.components = new System.ComponentModel.Container();
            this.richTextBoxWhisperMessage = new System.Windows.Forms.RichTextBox();
            this.buttonWhisperSend = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // richTextBoxWhisperMessage
            // 
            this.richTextBoxWhisperMessage.Location = new System.Drawing.Point(12, 3);
            this.richTextBoxWhisperMessage.Name = "richTextBoxWhisperMessage";
            this.richTextBoxWhisperMessage.Size = new System.Drawing.Size(373, 115);
            this.richTextBoxWhisperMessage.TabIndex = 0;
            this.richTextBoxWhisperMessage.Text = "";
            
            // 
            // buttonWhisperSend
            // 
            this.buttonWhisperSend.Location = new System.Drawing.Point(135, 124);
            this.buttonWhisperSend.Name = "buttonWhisperSend";
            this.buttonWhisperSend.Size = new System.Drawing.Size(130, 43);
            this.buttonWhisperSend.TabIndex = 1;
            this.buttonWhisperSend.Text = "Send";
            this.buttonWhisperSend.UseVisualStyleBackColor = true;
            this.buttonWhisperSend.Click += new System.EventHandler(this.buttonWhisperSend_Click);
            // 
            // WhisperChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 174);
            this.Controls.Add(this.buttonWhisperSend);
            this.Controls.Add(this.richTextBoxWhisperMessage);
            this.Name = "WhisperChat";
            this.Text = "WhisperChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WhisperChat_FormClosing);
            this.Load += new System.EventHandler(this.WhisperChat_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxWhisperMessage;
        private System.Windows.Forms.Button buttonWhisperSend;
        private System.Windows.Forms.Timer timer1;
    }
}