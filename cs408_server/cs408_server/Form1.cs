using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace cs408_server
{
    public partial class Form1 : Form
    {
        bool terminating = false;
        bool accept = true, is_question = false, is_probe = false;
        //List<Socket> socketList = new List<Socket>();
        List<Client> client_list = new List<Client>();
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private string userName = "", question = "", probe = "", realanswer = "";
        int ready_count = 0;


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
            try
            {
                server.Bind(new IPEndPoint(IPAddress.Any, serverPort));
                richTextBox1.Text = "Started listening for incoming connections.";
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

        private void Accept()
        {
            while (accept)
            {
                try
                {
                    Client c1 = new Client();
                    c1.client_socket = server.Accept();

                    Byte[] buffer = new byte[128];
                    int rec = c1.client_socket.Receive(buffer);
                    string command = Encoding.Default.GetString(buffer);

                    if (command.Substring(0, 5) == "$name")
                    {
                        String name = command.Substring(5).Replace("\0", "");
                        if (client_list.Any(x => x.name == name))
                        {
                            RTB_Writer("Connection error - DublicateNick");
                            c1.client_socket.Send(Encoding.Default.GetBytes("dublicateNick"));
                            c1.client_socket.Close();
                        }
                        else
                        {
                            name = name.Replace("\0", "");
                            c1.name = name;
                            c1.isAlive = true;
                            c1.isReady = false;
                            c1.score = 0;
                            c1.guess = "";
                            c1.GameId = 0;
                            client_list.Add(c1);
                            int index = client_list.Count() - 1;
                            RTB_Writer("\n" + name + "connected. \n");
                            Thread thrReceive = new Thread(() => Receive(index));
                            thrReceive.Start();
                        }
                    }
                    else
                    {
                        RTB_Writer("Connection error - invalid request\n");
                        byte[] error = Encoding.Default.GetBytes("Invalid request!\n");
                        c1.client_socket.Send(error);
                        c1.client_socket.Close();
                    }
                    //socketList.Add(server.Accept());
                    //RTB_Writer("\nNew Client connected.");
                    //thrReceive = new Thread(new ThreadStart(Receive));
                    //thrReceive.Start();
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
            thrAccept.Abort();
        }

        private void Receive(int index)
        {
            Client n = client_list[index];
            bool connected = true;
            while (connected)
            {
                try
                {
                    Byte[] buffer = new byte[1024];
                    int rec = n.client_socket.Receive(buffer);
                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }
                    string command = Encoding.Default.GetString(buffer).Replace("\0", "");

                    if (command == "$ready")
                    {
                        n.isReady = true;
                        RTB_Writer("\n" + n.name + " is ready");
                        bool allready = true;
                        for (int i = 0; i < client_list.Count; i++)
                        {
                            if (client_list[i].isReady == false)
                            {
                                allready = false;
                                break;
                            }
                        }
                        if (allready)
                        {
                            //client_list.Sort();
                            RTB_Writer("\n" + client_list[counter].name + "'s turn to ask");
                            client_list[counter].client_socket.Send(Encoding.Default.GetBytes("#ask"));
                            counter++;
                        }
                    }
                    else if (command == "$question")
                    {
                        RTB_Writer("\n" + n.name + "'s turn\n");
                        Byte[] buff = new byte[1024];
                        int r = n.client_socket.Receive(buff);
                        if (r <= 0)
                        {
                            throw new SocketException();
                        }
                        string text = Encoding.Default.GetString(buff);
                        question = text.Substring(0, text.IndexOf("?"));
                        realanswer = text.Substring(text.IndexOf("?") + 1, text.IndexOf("\0"));
                        while (realanswer.Contains("\0"))
                        {
                            realanswer = realanswer.Replace("\0", "");
                        }

                        RTB_Writer(n.name + " asked fallowing question: " + question + "\nAnd provided answer as: " + realanswer + "\n");
                        for (int i = 0; i < client_list.Count; i++)
                        {
                            if (i != index)
                            {
                                client_list[i].client_socket.Send(Encoding.Default.GetBytes("#answer"));
                                client_list[i].client_socket.Send(Encoding.Default.GetBytes(question));
                            }
                        }
                    }
                    else if (command == "$answer")
                    {
                        Byte[] buff = new byte[1024];
                        int r = n.client_socket.Receive(buff);
                        if (r <= 0)
                        {
                            throw new SocketException();
                        }
                        string text = Encoding.Default.GetString(buff);
                        n.guess = text.Substring(0, text.IndexOf("\0"));
                        //if (n.guess.Contains(n.name) && n.guess.IndexOf(n.name) == (n.guess.Length - (n.name.Length + 1)))
                        //{
                        //    n.guess = n.guess.Substring(0, n.guess.IndexOf(n.name));
                        //}
                        RTB_Writer("\n" + n.name + " guessed " + n.guess + "\n");
                        bool allanswered = true;
                        for (int i = 0; i < client_list.Count; i++)
                        {
                            if (i != index && client_list[i].guess != "")
                            {
                                allanswered = false;
                            }
                        }
                        if (allanswered)
                        {
                            counter++;
                            //for (int i = 0; i < client_list.Count; i++)
                            //{
                            if (client_list[index].guess.ToLower() == realanswer.ToLower())
                            {
                                client_list[index].score++;
                                client_list[index].client_socket.Send(Encoding.Default.GetBytes("#win"));
                                RTB_Writer(client_list[index].name + " won \n");
                            }
                            else if (client_list[index].guess.ToLower() != realanswer.ToLower())
                            {
                                client_list[index].client_socket.Send(Encoding.Default.GetBytes("#lose"));
                                RTB_Writer(client_list[index].name + " lost \n");
                            }
                            //}
                            client_list[counter % (client_list.Count)].client_socket.Send(Encoding.Default.GetBytes("#ask"));
                            RTB_Writer("\n" + client_list[counter % client_list.Count] + "'s turn to ask");
                        }
                    }
                    else if (command == "$diconnected")
                    {
                        RTB_Writer(n.name + " has disconnected");
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
                    n.client_socket.Close();
                    client_list.Remove(n);
                    connected = false;
                }

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

namespace cs408_server
{
    class Client
    {

        public Socket client_socket { get; set; }
        public string name { get; set; }
        public int GameId { get; set; }
        public int round { get; set; }
        public string guess { get; set; }
        public int localScore = 0;
        public int difference = -1;
        public bool isAlive { get; set; }
        public int score { get; set; }
        //public int randomNum { get; set; }
        public bool isReady { get; set; }
    }
}