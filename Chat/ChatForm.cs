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

        private bool Connect()
        {
            return false;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if(Connect() == true)
            {
                this.Hide();
                MainChatForm mainChat = new MainChatForm();
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
