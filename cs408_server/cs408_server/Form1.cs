using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs408_server
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool accept = true;
        Socket server;
        List<Socket> socketList = new List<Socket>();
        int serverPort = 0;
        Thread thrAccept;

        string username_1, username_2;

        static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static List<Socket> clientSockets = new List<Socket>();

        private const int BUFFER_SIZE = 2048;

        private static readonly byte[] buffer = new byte[BUFFER_SIZE];

        public Form1()
        {
            InitializeComponent();
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
                box_report.Text = "Started listening for incoming connections.";
                server.Listen(3); //the parameter here is maximum length of the pending connections queue
                thrAccept = new Thread(new ThreadStart(Accept));
                thrAccept.Start();
                btn_start.Enabled = false;
                btn_close.Enabled = true;

                terminating = false;

                //                thrServer = new Thread(new ThreadStart(infiniteServerInput));
                //                thrServer.Start();

            }
            catch
            {
                box_report.Text = "Cannot create a server with the specified port number\n Check the port number and try again.";
                box_report.Text = "terminating...";
            }

        }

        private void Accept()
        {
            while (accept)
            {
                try
                {
                    socketList.Add(server.Accept());
                    Thread thrReceive;
                    thrReceive = new Thread(new ThreadStart(Receive));
                    thrReceive.Start();
                }
                catch
                {
                    if (terminating)
                        accept = false;
                    else
                        box_report.Text = "Listening socket has stopped working...\n";
                }
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            server.Close();
            thrAccept.Abort();
            box_report.Text = "Server has been closed...";
            btn_close.Enabled = false;
            btn_start.Enabled = true;
            terminating = true;
        }

        private void Receive()
        {
            bool connected = true;
            Socket n = socketList[socketList.Count - 1];

            while (connected)
            {
                try
                {
                    Byte[] buffer = new byte[64];
                    int rec = n.Receive(buffer);

                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }

                    string newmessage = Encoding.Default.GetString(buffer);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));
                    Console.Write("Client: " + newmessage + "\r\n");
                }
                catch
                {
                    if (!terminating)
                        Console.Write("Client has disconnected...\n");
                    n.Close();
                    socketList.Remove(n);
                    connected = false;
                }
            }
        }


        private void infiniteServerInput()
        {
            if (terminating)
            {
                box_report.Text = "Server has been closed.";
                server.Close();
            }

            /*            while (!terminating)
                        {
                            string message = "";
                            try
                            {
                                if (message != "")
                                    BroadCastMessage(message);
                            }
                            catch
                            {
                                terminating = true;
                                server.Close();
                            }
                        }   */
        }
    }
}
