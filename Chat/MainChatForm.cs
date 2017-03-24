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
        IRCClient client;
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
            textBoxAddress.Text = "irc.twitch.tv";
        }

        private void ReadMessages()
        {
            
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
            this.Close();
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
                var message = reader.ReadLine();
                richTextBoxChat.AppendText(message + Environment.NewLine);
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
            
        }

        private void richTextBoxChat_TextChanged(object sender, EventArgs e)
        {
            richTextBoxChat.SelectionStart = richTextBoxChat.Text.Length;
            richTextBoxChat.ScrollToCaret();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Reconnect();
            timer1.Enabled = true;          
        }
    }
}
