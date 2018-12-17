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
        int ready_count = 0;

        int counter = 0;
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
            //if (socketList.Count == 2 && guess == "")
            //{
            //    //ask_question();
            //}
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

        //void ask_question()
        //{
        //    byte[] buffer = Encoding.Default.GetBytes("ask a question");
        //    socketList[counter % 2].Send(buffer);
        //}

        private void Accept()
        {
            while (accept)
            {
                try
                {
                    socketList.Add(server.Accept());
                    richTextBox1.AppendText("\nNew Client connected.");
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
            string text = Encoding.Default.GetString(buffer);
            richTextBox1.AppendText("\n" + text);
            if (text[0] == 'N')
            {
                userName = text.Substring(1, text.IndexOf("\0"));
                is_question = false;
                is_probe = false;
                richTextBox1.AppendText("\nUser entered");
            }
            else if (text.Substring(0,5) == "ready")
            {
                // user is ready.
                richTextBox1.AppendText("\nUser " + text.Substring(4) + " is ready to start");
                ready_count++;
            }
            else if (text.Contains("?")) // if client sends Q & A
            {
                question = text.Substring(0, text.IndexOf("~"));
                realanswer = text.Substring(text.IndexOf("~"), text.IndexOf("\0"));
                is_question = true;
                is_probe = false;
                richTextBox1.AppendText("\nUser " + userName + " has asked the following question: " + question);
                richTextBox1.AppendText("\nAnd declared the correct answer as: " + realanswer);
            }
            else
            {
                probe = text.Substring(0, text.IndexOf("\0"));
                richTextBox1.AppendText("\nElse");
                is_probe = true;
                is_question = false;
            }

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

            if (connected)
            {
                richTextBox1.AppendText("\nUser name: " + userName + " connected.");
                richTextBox1.AppendText("\n" + userNameList.Count);
                if (userNameList.Count == 1 ) // because array starts from 0 therefore when 2 clients connected the number is 1 instead of 2
                {
                    byte[] t_buf = Encoding.Default.GetBytes("ask");
                    socketList[(socketList.Count + counter) % 2].Send(t_buf);
                    counter++;
                }
            //}
            //while (connected)
            //{
                try
                {
                    //richTextBox1.AppendText("\nUser name: " + userName + " connected.");

                    userNameList.Add(userName);

                    buffer = new byte[64];
                    rec = n.Receive(buffer);

                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }

                    if (is_question)
                    {
                        richTextBox1.AppendText("\nUser " + userName + " has asked the following question: " + question);
                        richTextBox1.AppendText("\nAnd declared the correct answer as: " + realanswer);
                        Socket other = socketList[socketList.Count];
                        other.Send(Encoding.Default.GetBytes(question));
                    }
                    {
                        //if (userNameList.Count == 2)
                        //{
                        //    //byte[] t_buf = Encoding.Default.GetBytes("ask a question");
                        //    //socketList[counter % 2].Send(t_buf);

                        //    if (Encoding.Default.GetString(buffer) == "question and answer")
                        //    {
                        //        //ask_question();
                        //        //mut.WaitOne();
                        //        // Simulate some work.
                        //        //Thread.Sleep(500);
                        //        byte[] tempbuffer = new byte[124];
                        //        int tempreceive = socketList[counter % 2].Receive(tempbuffer);
                        //        question = Encoding.Default.GetString(buffer);
                        //        tempbuffer = new byte[124];
                        //        tempreceive = socketList[counter % 2].Receive(tempbuffer);
                        //        answer = Encoding.Default.GetString(buffer);
                        //        //mut.ReleaseMutex();

                        //        //byte[] t_buffer = Encoding.Default.GetBytes("aswer the question");
                        //        //socketList[(counter + 1) % 2].Send(t_buffer);

                        //        richTextBox1.AppendText("\nQuestion to be asked: " + question + "\nThe Correct answer: " + answer);
                        //        richTextBox1.AppendText("\nSending Question " + question + " to " + userNameList[(counter + 1) % 2]);
                        //    }
                        //    else if (Encoding.Default.GetString(buffer) == "Answer")
                        //    {
                        //        richTextBox1.AppendText("\n Asked the question: " + question + "to user " + userNameList[(counter + 1) % 2]);

                        //        mut.WaitOne();
                        //        // Simulate some work.
                        //        //Thread.Sleep(500);
                        //        byte[] tempbuffer = new byte[124];
                        //        int tempreceive = socketList[(counter + 1) % 2].Receive(tempbuffer);
                        //        guess = Encoding.Default.GetString(buffer);
                        //        mut.ReleaseMutex();
                        //        richTextBox1.AppendText("\n" + userNameList[(counter + 1) % 2] + " guessed " + guess);

                        //        if (guess.ToLower() == answer.ToLower())
                        //        {
                        //            BroadCastMessage("\n" + userNameList[(counter + 1) % 2] + " guessed correctly");
                        //        }
                        //        else
                        //        {
                        //            BroadCastMessage("\n" + userNameList[(counter + 1) % 2] + " guessed correctly");
                        //        }
                        //        counter++;
                        //        guess = "";
                        //    }
                        //    else
                        //    {
                        //        richTextBox1.AppendText("\nSent \"ask a question\" to user: " + userNameList[0]);
                        //    }
                        //}
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
                            {
                                userNameList.Remove(userName);
                            }
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
