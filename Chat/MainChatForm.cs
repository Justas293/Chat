using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Chat
{
    public partial class MainChatForm : Form
    {
        IRCClient client;

        private delegate void ReadMessagesInvoker();

        public MainChatForm()
        {
            InitializeComponent();
            
        }

        private void ReadMessages()
        {
            while (true)
            {
                richTextBoxChat.AppendText(client.ReadMessage());
            }
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

        private void buttonSend_Click(object sender, EventArgs e)
        {
            
        }
    }
}
