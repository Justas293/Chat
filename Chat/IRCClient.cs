using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Chat
{
    public class IRCClient
    {
        private TcpClient tcpClient;
        private StreamReader inputStream;
        private StreamWriter outputStream;

        public string userName;
        private string channel;
        private string password;

        private int port = 6667;

        public IRCClient(string ip, string username, string channel, string password = "")
        {
            try
            {
                this.userName = username;
                this.password = password;

                tcpClient = new TcpClient(ip, port);
                inputStream = new StreamReader(tcpClient.GetStream());
                outputStream = new StreamWriter(tcpClient.GetStream());

                outputStream.WriteLine("PASS " + password);
                outputStream.WriteLine("NICK " + username);
                outputStream.WriteLine("USER " + username + " : " + username);
                outputStream.WriteLine("JOIN #" + channel);
                outputStream.Flush();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to connect! ERROR: " + ex.ToString());
            }

        }

        private void SendIRCMessage(string message)
        {
            outputStream.WriteLine(message);
            outputStream.Flush();
        }
        /*
        private void Connect()
        {
            outputStream.WriteLine("PASS " + password);
            outputStream.WriteLine("NICK " + username);
            outputStream.WriteLine("USER " + username + " : " + username);
            outputStream.Flush();      
        }
        */
        private void JoinChannel(string channel)
        {
            this.channel = channel;
            outputStream.WriteLine(string.Format("JOIN #{0}", channel));
            outputStream.Flush();
        }

        public void SendChatMessage(string message)
        {
            SendIRCMessage(string.Format("PRIVMSG {0} :{1}", channel, message));
        }

        public string ReadMessage()
        {
            string message = inputStream.ReadLine();
            return message;
        }

        public void StartClient()
        {
        }
    }
}
