using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace cs408_server
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool accept = true;
        Socket server;
        List<Socket> socketList = new List<Socket>();

        int counter = 0;
        bool connected1 = false, connected2 = false;
        int serverPort;
        Thread thrAccept;

        List<string> userNameList = new List<string>();

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            box_ip.Text = GetMyIP();
            box_ip.ReadOnly = true;
            btn_close.Enabled = false;
        }

        //public void LoopClients()
        //{
        //    while (_isRunning)
        //    {
        //        // wait for client connection
        //        TcpClient newClient = _server.AcceptTcpClient();

        //        // client found.
        //        // create a thread to handle communication
        //        Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
        //        t.Start(newClient);
        //    }
        //}

        private string GetMyIP()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            serverPort = Int32.Parse(box_port.Text);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Bind(new IPEndPoint(IPAddress.Any, serverPort));
                richTextBox1.AppendText("\nStarted listening for incoming connections.");
                server.Listen(3); //the parameter here is maximum length of the pending connections queue
                thrAccept = new Thread(new ThreadStart(Accept));
                thrAccept.Start();
                btn_start.Enabled = false;
                btn_close.Enabled = true;
                terminating = false;
                //    thrServer = new Thread(new ThreadStart(infiniteServerInput));
                //    thrServer.Start();
            }
            catch
            {
                richTextBox1.AppendText("\nCannot create a server with the specified port number\n Check the port number and try again.");
                richTextBox1.AppendText("\nterminating...");
            }

        }

        private void infiniteServerInput()
        {
            if (terminating)
            {
                richTextBox1.AppendText("\nServer has been closed.");
                server.Close();
            }
        }

        void BroadCastMessage(string message)
        {
            byte[] buffer = Encoding.Default.GetBytes(message);

            //broadcast the message to all clients
            foreach (Socket s in socketList)
            {
                s.Send(buffer);
            }
        }

        private void Accept()
        {
            while (accept)
            {
                try
                {
                    socketList.Add(server.Accept());
                    richTextBox1.AppendText("\nNew Client connected.\n");
                    Thread thrReceive;
                    thrReceive = new Thread(Receive);
                    thrReceive.Start();
                }
                catch
                {
                    if (terminating)
                        accept = false;
                    else
                        richTextBox1.AppendText("\nListening socket has stopped working...\n");
                }
            }
        }

        private void Receive()
        {
            bool connected = true;
            Socket n = socketList[socketList.Count - 1];
            Byte[] buffer = new byte[64];
            int rec = n.Receive(buffer);

            if (rec <= 0)
            {
                throw new SocketException();
            }

            string userName = Encoding.Default.GetString(buffer);
            userName = userName.Substring(0, userName.IndexOf("\0"));

            foreach (string s in userNameList)
            {
                if (s == userName)
                {
                    connected = false;
                    richTextBox1.AppendText("\nUser: " + userName + " is already connected.");
                    buffer = Encoding.Default.GetBytes("dublicateNick");
                    n.Send(buffer);
                    Thread.Sleep(500);
                    n.Close();
                }
            }

            while (connected)
            {
                try
                {
                    //if(socketList[0].Connected && !connected1)
                    //{
                    richTextBox1.AppendText("\nUser name: " + userName + " connected.");
                    connected1 = true;
                    //}
                    userNameList.Add(userName);

                    buffer = new byte[64];
                    rec = n.Receive(buffer);

                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }
                    if (userNameList.Count == 2)
                    {
                        if (counter %6 == 0)
                        {
                            richTextBox1.AppendText("\nSent \"ask a question\" to user: " + userNameList[0]);
                            socketList[0].Send(Encoding.Default.GetBytes("ask a question"));
                            counter++;
                        }
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        richTextBox1.AppendText("\nUser: " + userName + " has disconnected...\n");
                        for (int i = 0; i < userNameList.Count; i++)
                        {
                            if (userNameList[i] == userName)
                                userNameList.Remove(userName);
                        }
                    }
                    n.Close();
                    socketList.Remove(n);
                    connected = false;
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            server.Close();
            thrAccept.Abort();
            richTextBox1.AppendText("\nServer has been closed...");
            btn_close.Enabled = false;
            btn_start.Enabled = true;
            terminating = true;
        }
    }
}
