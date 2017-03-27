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

namespace Chat
{
    public partial class MainChatForm : Form
    {

        private int port = 6667;
        private string username, channel;
        private string password = "oauth:740navv2c22dgjoffxwjb3r6w93jer";


        TcpClient tcpClient;
        StreamReader reader;
        StreamWriter writer;

        bool joined;

        public MainChatForm()
        {
            InitializeComponent();
            textBoxAddress.Text = "irc.freenode.net";
        }

        private void ReadMessages()
        {
            string sender, msg;
            var message = reader.ReadLine();
            richTextBoxChat.AppendText(message + Environment.NewLine);
            if (message.Contains("PRIVMSG"))
            {
                sender = message.Split('!')[0];
                msg = message.Split(':')[2];
                sender = sender.Substring(1);
                msg = string.Format("<{0}>: {1}", sender, msg);
                richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + msg + Environment.NewLine);
            }
            else
            {
                richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + message + Environment.NewLine);
            }
            
        }

        private void SendMessage(string message)
        {
            writer.WriteLine($":{username}!{username}@{username} PRIVMSG #{channel} : {message}");
            writer.Flush();
            richTextBoxChat.AppendText("[" + DateTime.Now.ToString("hh:mm") + "]" + "<" + username + ">" + ": " + message);
            
        }

        private void Reconnect()
        {
            tcpClient = new TcpClient(textBoxAddress.Text, port);
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());

            this.username = textBoxUsername.Text;
            this.channel = textBoxChannel.Text;

            writer.WriteLine("PASS " + this.password + Environment.NewLine +
                             "USER " + this.username + " 8 * :" + this.username + Environment.NewLine + 
                             "NICK " + this.username);
            writer.Flush();
            joined = false;
        }

        private void CheckForEnter(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                buttonSend.PerformClick();
            }
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            writer.WriteLine("QUIT");
            writer.Flush();
        }

        private void richTextBoMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckForEnter(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!tcpClient.Connected)
            {
                Reconnect();
            }

            if (tcpClient.Available > 0 || reader.Peek() >= 0)
            {
                ReadMessages();
            }
            else if (!joined)
            {
                writer.WriteLine("JOIN #" + this.channel);
                writer.Flush();
                joined = true;
            }
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
            //messagePrefix = $":{username}!{username}@{username}.tmi.twitch.tv PRIVMSG #{channel} :";
            Reconnect();
            timer1.Enabled = true;          
        }
    }
}
