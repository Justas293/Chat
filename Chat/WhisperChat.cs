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
        public WhisperChat(IRCClient client, string username)
        {
            InitializeComponent();
            this.userName = username;
            this.client = client;
        }

        private void WhisperChat_Load(object sender, EventArgs e)
        {
            this.Text = "Chat with " + userName;
            var c = Runchat();
        }

        private void WhisperChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void buttonWhisperSend_Click(object sender, EventArgs e)
        {
            SendMessage(richTextBoxWhisperMessage.Text);
            richTextBoxWhisperMessage.Clear();
        }

        private void SendMessage(string message)
        {
            client.SendPrivateMessage(this.userName, message);
            richTextBoxPrivateChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + "<" + client.userName + ">" + ": " + message);
        }

        private async Task Runchat()
        {
            string message, msg;
            try
            {
                while ((message = await client.ReadMessage()) != null)
                {
                    if (message.StartsWith(":" + this.userName) && message.Contains("PRIVMSG " + client.userName + " :"))
                    {
                        msg = message.Split(':')[2];
                        msg = string.Format("<{0}>: {1}", this.userName, msg);
                        richTextBoxPrivateChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + msg + Environment.NewLine, Color.Purple);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
