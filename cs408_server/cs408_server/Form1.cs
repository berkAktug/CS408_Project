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
        bool accept = true, is_question = false, is_probe = false;
        Socket server;
        List<Socket> socketList = new List<Socket>();
        private string userName = "", question = "", probe = "", realanswer = "";
        int ready_count = -1;


        int i = 0;
        int counter = 0;
        int serverPort;
        Thread thrAccept;
        Thread thrReceive;
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
                richTextBox1.Text = "\nStarted listening for incoming connections.";
                server.Listen(3); //the parameter here is maximum length of the pending connections queue
                thrAccept = new Thread(new ThreadStart(Accept));
                thrAccept.Start();

                btn_start.Enabled = false;
                btn_close.Enabled = true;
                terminating = false;
            }
            catch
            {
                RTB_Writer("\nCannot create a server with the specified port number\n Check the port number and try again.\nTerminating...");
                //richTextBox1.AppendText("\nCannot create a server with the specified port number\n Check the port number and try again.\nTerminating...");
            }
        }

        delegate void RTB_WriterDelegate(string text);
        private void RTB_Writer(string text)
        {
            try
            {
                if (richTextBox1.InvokeRequired)
                {
                    RTB_WriterDelegate del = new RTB_WriterDelegate(RTB_Writer);
                    richTextBox1.Invoke(del, new object[] { text });
                }
                else
                {
                    richTextBox1.AppendText(text);
                }
            }
            catch
            {
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
                    RTB_Writer("\nNew Client connected.");

                    thrReceive = new Thread(new ThreadStart(Receive));
                    thrReceive.Start();
                }
                catch
                {
                    if (terminating)
                        accept = false;
                    else
                        RTB_Writer("\nListening socket has stopped working...\n");
                    //richTextBox1.AppendText("\nListening socket has stopped working...\n");
                }
            }
        }

        private void Receive()
        {

            bool connected = true;
            while (connected)
            {
                Socket n = socketList[socketList.Count - 1];
                byte[] buffer = new byte[1024];
                int rec = n.Receive(buffer);

                if (rec <= 0)
                {
                    throw new SocketException();
                }
                string type = Encoding.Default.GetString(buffer);
                RTB_Writer("\n" + type);

                if (type == "$nick")
                {

                    userName = type.Substring(1, type.IndexOf("\0"));
                    is_question = false;
                    is_probe = false;
                }
                //else if (text[0] == 'R')
                else if (type == "$ready")
                {
                    // user is ready.
                    //richTextBox1.AppendText("\nUser " + text.Substring(1, text.IndexOf("\0")) + " is ready to start");
                    //RTB_Writer("\nUser " + text.Substring(1) + " is ready to start");
                    RTB_Writer("\nUser " + " is ready to start");

                    ready_count++;
                }
                else if (type == "$question") // if client sends Q & A
                {
                    buffer = new byte[1024];
                    rec = n.Receive(buffer);
                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }

                    type = Encoding.Default.GetString(buffer);

                    question = type.Substring(0, type.IndexOf("~"));
                    realanswer = type.Substring(type.IndexOf("~"), type.IndexOf("\0"));
                    is_question = true;
                    is_probe = false;
                    RTB_Writer("\nUser " + userName + " has asked the following question: " + question);
                    RTB_Writer("\nAnd declared the correct answer as: " + realanswer);
                }
                else if (type == "$answer")
                {
                    buffer = new byte[1024];
                    rec = n.Receive(buffer);
                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }

                    type = Encoding.Default.GetString(buffer);

                    probe = type.Substring(0, type.IndexOf("\0"));
                    RTB_Writer("\nProgram has hit breakpoint \"Else\"");
                    is_probe = true;
                    is_question = false;
                }

                userNameList.Sort();
                //if (i != 0)
                for (int k = 0; k < userNameList.Capacity; k++)
                {
                    if ((userNameList[k] == userName) && i != k)
                    {
                        userNameList[k] = "";
                        //connected = false;
                        RTB_Writer("\nUser: " + userName + " is already connected. Therefore the connection request been declined.");
                        n.Send(Encoding.Default.GetBytes("dublicateNick"));
                        n.Close();
                        return;
                    }
                }
                RTB_Writer("\nUser name: " + userName + " connected.");
                RTB_Writer("\nUser count:" + userNameList.Count);

                //while (connected)
                //{
                try
                {

                    byte[] buffertemp = new byte[64];
                    //string type = Encoding.Default.GetString(buffertemp);
                    rec = n.Receive(buffer);

                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }
                    string text = Encoding.Default.GetString(buffer);

                    RTB_Writer("\n" + text);
                    //if (userNameList.Count == ready_count) // because array starts from 0 therefore when 2 clients connected the number is 1 instead of 2
                    if (text == "$ready")
                    {
                        ready_count++;
                        RTB_Writer("\nIT IS ALIVEEEE!!!");
                    }
                    if (userNameList.Count >= 1) // because array starts from 0 therefore when 2 clients connected the number is 1 instead of 2
                    {
                        byte[] t_buf = Encoding.Default.GetBytes("ask");
                        socketList[(socketList.Count + counter) % 2].Send(t_buf);
                        counter++;
                    }
                    userNameList.Add(userName);

                    buffer = new byte[64];
                    rec = n.Receive(buffer);

                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }

                    if (is_question)
                    {
                        RTB_Writer("\nUser " + userName + " has asked the following question: " + question);
                        RTB_Writer("\nAnd declared the correct answer as: " + realanswer);
                        Socket other = socketList[socketList.Count - 1];
                        other.Send(Encoding.Default.GetBytes(question));
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        RTB_Writer("\nUser: " + userName + " has disconnected...\n");
                        for (int i = 0; i < userNameList.Count; i++)
                        {
                            if (userNameList[i] == userName)
                            {
                                userNameList.Remove(userName);
                            }
                        }
                    }
                    n.Close();
                    socketList.Remove(n);
                    connected = false;
                }
                //}
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            server.Close();
            thrAccept.Abort();
            RTB_Writer("\nServer has been closed...");
            btn_close.Enabled = false;
            btn_start.Enabled = true;
            terminating = true;
        }
    }
}
