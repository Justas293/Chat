using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using Chat;
using System.Threading.Tasks;

namespace Chat
{
    public partial class MainChatForm : Form
    {
        #region Variables
        #region pass
        private string password = "oauth:740navv2c22dgjoffxwjb3r6w93jer";
        #endregion

        IRCClient client;
        List<string> userlist;
        #endregion

        public MainChatForm()
        {
            InitializeComponent();
            
        }

        private void MainChatForm_Load(object sender, EventArgs e)
        {
            textBoxAddress.Text = "irc.freenode.net";
            client = new IRCClient();
        }

        private async Task Runchat()
        {
            string message, msg;
            string sender;
            try
            {
                while((message = await client.ReadMessageAsync()) != null)
                {
                    richTextBoxChat.AppendText(message + Environment.NewLine);
                    if (message.Contains("@ #" + client.channel + " :"))
                    {
                        string[] delimiter = new string[] { $"@ #{client.channel} :" };
                        string users = message.Split(delimiter, StringSplitOptions.RemoveEmptyEntries)[1];
                        userlist = users.Split(' ').ToList();
                        listBoxUsers.DataSource = userlist;
                        labelSatus.Text = "Status: connected";
                    }
                    if (message.Contains("JOIN") || message.Contains("QUIT") || message.Contains("NICK"))
                    {
                        client.GetNames();
                    }
                    if (message.Contains("PING"))
                    {
                        client.PongServer(message);
                    }
                    else if (message.Contains("PRIVMSG " + client.userName + " :"))
                    {
                        sender = message.Split('!')[0];
                        msg = message.Split(':')[2];
                        sender = sender.Substring(1);
                        msg = string.Format("<{0}>: {1}", sender, msg);
                        richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + msg + Environment.NewLine, Color.Purple);
                    }

                    else if (message.Contains("PRIVMSG #" + client.channel + " :"))
                    {
                        sender = message.Split('!')[0];
                        msg = message.Split(':')[2];
                        sender = sender.Substring(1);
                        msg = string.Format("<{0}>: {1}", sender, msg);
                        richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + msg + Environment.NewLine, Color.Black);
                    }
                }
            }
            catch
            {

            }
        }

        private void SendMessage(string message)
        {
            client.SendChatMessage(richTextBoMessage.Text);
            richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + "<" + client.userName + ">" + ": " + message + Environment.NewLine);
        }

        private void GetUserList()
        {
            //writer.WriteLine("NAMES #" + channel);
            //writer.Flush();
        }

        private void Reconnect()
        {
            if (!client.Connected)
            {
                client.Connect(textBoxAddress.Text, textBoxUsername.Text, password);
            }
        }

        private void CheckForEnter(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                richTextBoMessage.Text = richTextBoMessage.Text.Substring(0, (richTextBoMessage.Text.Length - 1));
                buttonSend.PerformClick();
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (client.Connected)
            {
                listBoxUsers.DataSource = null;
                richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + "Disconnected!" + Environment.NewLine);
                client.Disconnect();
            }
            else
            {
                richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + "You are not connected yet!" + Environment.NewLine);
            }
            labelSatus.Text = "Status: not connected";        
        }

        private void richTextBoMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckForEnter(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendMessage(richTextBoMessage.Text);
            richTextBoMessage.Clear();
        }

        private void richTextBoxChat_TextChanged(object sender, EventArgs e)
        {
            richTextBoxChat.SelectionStart = richTextBoxChat.Text.Length;
            richTextBoxChat.ScrollToCaret();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + "Connecting..." + Environment.NewLine);
            labelSatus.Text = "Status: connecting...";
            if (client.Connected)
            {
                client.Disconnect();
            }
            client.Connect(textBoxAddress.Text, textBoxUsername.Text, password);
            while (!client.Joined)
            {
                client.JoinChannel(textBoxChannel.Text);
            }
            client.GetNames();
            var c = Runchat();
            
        }

        private void buttonWhisper_Click(object sender, EventArgs e)
        {
            
            WhisperChat privateChat = new WhisperChat(client, listBoxUsers.SelectedItem.ToString(), this.richTextBoxChat);
            Thread t = new Thread(() => privateChat.ShowDialog());
            t.Start();
        }

        private void MainChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client.Connected)
            {
                client.Disconnect();
            }
            
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonChangeUserName_Click(object sender, EventArgs e)
        {
            if(client.Connected && client.Joined)
            {
                string value = client.userName;
                if (InputBox("New Username", "Enter new username:", ref value) == DialogResult.OK)
                {
                   client.ChangeUserName(value);
                   textBoxUsername.Text = client.userName;
                }
            }
            else
            {
                richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + "You are not connected to the server!" + Environment.NewLine);
            }
            
        }

        private void buttonJoinChannel_Click(object sender, EventArgs e)
        {
            if (client.Connected)
            {
                string value = client.userName;
                if (InputBox("New channel", "Enter new channel:", ref value) == DialogResult.OK)
                {
                    client.JoinChannel(value);
                    textBoxChannel.Text = client.channel;
                }
            }
            else
            {
                richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + "You are not connected to the server!" + Environment.NewLine);
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}
