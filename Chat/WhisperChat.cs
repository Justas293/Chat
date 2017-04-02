using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat
{
    public partial class WhisperChat : Form
    {
        private string userName;
        private IRCClient client;
        private RichTextBox tb;

        public WhisperChat(IRCClient client, string username, RichTextBox tb)
        {
            InitializeComponent();
            this.userName = username;
            this.client = client;
            this.tb = tb;
        }

        private void WhisperChat_Load(object sender, EventArgs e)
        {
            this.Text = "Chat with " + userName;
        }

        private void WhisperChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void buttonWhisperSend_Click(object sender, EventArgs e)
        {
            SendMessage(richTextBoxWhisperMessage.Text);
            richTextBoxWhisperMessage.Clear();
            this.Dispose();
        }

        private void SendMessage(string message)
        {
            Invoke(new Action(() =>
            {
                tb.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + "<To " + this.userName + ">" + ": " + message + Environment.NewLine, Color.Blue);
            }));
            client.SendPrivateMessage(this.userName, message);
            
        }
    }
}
