using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private StreamReader reader;
        private StreamWriter writer;

        public string userName { get; set; }
        public string channel { get; set; }
        private string password;

        public bool Connected { get; set; }
        public bool Joined { get; set; }

        private int port = 6667;

        public IRCClient()
        {
            Connected = false;
            Joined = false;
        }

        public void Connect(string ip, string username, string password = "")
        {
            try
            {
                this.userName = username;
                this.password = password;

                tcpClient = new TcpClient(ip, port);
                reader = new StreamReader(tcpClient.GetStream());
                writer = new StreamWriter(tcpClient.GetStream());

                writer.WriteLine("PASS " + this.password + Environment.NewLine +
                             "USER " + this.userName + " 8 * :" + this.userName + Environment.NewLine +
                             "NICK " + this.userName);
                writer.Flush();
                Connected = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect! ERROR: " + ex.ToString());
            }
        }

        public void Disconnect()
        {
            writer.WriteLine("QUIT");
            writer.Flush();
            Connected = false;
            Joined = false;
        }

        public void LeaveChannel()
        {
            if (Joined)
            {
                writer.WriteLine("QUIT #" + this.channel);
                writer.Flush();
                Joined = false;
            }
        }

        public void JoinChannel(string channel)
        {
            if (Joined)
            {
                LeaveChannel();
            }
            this.channel = channel;
            writer.WriteLine("JOIN #" + this.channel);
            writer.Flush();
            Joined = true;
        }

        public void ChangeChannel(string channel)
        {
            JoinChannel(channel);
        }

        private void SendIRCMessage(string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public void SendChatMessage(string message)
        {
            SendIRCMessage(string.Format("PRIVMSG #{0} :{1}", channel, message));
        }

        public void ChangeUserName(string username)
        {
            this.userName = userName;
            writer.WriteLine("NICK " + username);
            writer.Flush();
        }

        public void SendPrivateMessage(string username, string message)
        {
            SendIRCMessage(string.Format("PRIVMSG {0} :{1}", username, message));
        }

        public Task<string> ReadMessageAsync()
        {
            return reader.ReadLineAsync();
        }

        public void GetNames()
        {
            writer.WriteLine("NAMES #" + this.channel);
            writer.Flush();
        }

        public void PongServer(string message)
        {
            string pongMsg = message.Split(':')[1];
            writer.WriteLine("PONG " + pongMsg);
            writer.Flush();
        }

    }
}
