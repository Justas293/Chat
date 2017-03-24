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
    public partial class MainChatForm : Form
    {
        public MainChatForm()
        {
            InitializeComponent();
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
