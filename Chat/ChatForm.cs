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
    public partial class ChatForm : Form
    {
        public ChatForm()
        {
            InitializeComponent();
        }

        private void CheckForEnter(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                buttonConnect.PerformClick();
            }
        }

        private IRCClient Connect()
        {
            IRCClient client = new IRCClient(textBoxAddress.Text, textBoxUsername.Text, textBoxChannel.Text);
            return client;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            IRCClient client = Connect();
            if(client != null)
            {
                this.Hide();
                MainChatForm mainChat = new MainChatForm(client);
                mainChat.ShowDialog();
                this.Show(); 
            }
            else
            {
                MessageBox.Show("Unable to connect!", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckForEnter(e);
        }

        private void textBoxChannel_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckForEnter(e);
        }

        private void textBoxAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckForEnter(e);
        }
    }
}
