using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace cs408_client
{
    public partial class Form1 : Form
    {
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static bool terminating = false;
        private static string IP;
        private static int PORT;
        Thread thrReceive;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            box_report.Text = "Please enter the server's IP, port number and your user name.";


            TextBox.CheckForIllegalCrossThreadCalls = false;

            btn_connect.Enabled = true;
            btn_disconnect.Enabled = false;
            btn_sendmessage.Enabled = false;
            box_nick.Enabled = true;
            box_ip.Enabled = true;
            box_port.Enabled = true;
            box_question.Enabled = false;
            box_answer.Enabled = false;
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            client.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }



        private void Receive()
        {
            bool connected = true;

            while (connected)
            {
                try
                {
                    byte[] buffer = new byte[64];

                    int rec = client.Receive(buffer);

                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }

                    string newmessage = Encoding.Default.GetString(buffer);
                    newmessage = newmessage.Substring(0, newmessage.IndexOf("\0"));

                    if (newmessage == "dublicateNick")
                    {
                        box_report.Text = "Username is already exist.";

                        client.Close();

                        box_nick.Enabled = true;
                        box_ip.Enabled = true;
                        box_port.Enabled = true;
                        btn_connect.Enabled = true;
                        btn_disconnect.Enabled = false;
                        btn_sendmessage.Enabled = false;
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        box_report.Text = "Connection has been terminated...";

                        client.Close();

                        box_nick.Enabled = true;
                        box_ip.Enabled = true;
                        box_port.Enabled = true;
                        btn_connect.Enabled = true;
                        btn_disconnect.Enabled = false;
                        btn_sendmessage.Enabled = false;
                    }

                    connected = false;
                }
            }
        }


        private void ReceiveResponse()
        {
            var buffer = new byte[2048];
            int received = client.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
        }


        private void btn_connect_Click(object sender, EventArgs e)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IP = box_ip.Text;
                PORT = Int32.Parse(box_port.Text);

                client.Connect(IP, PORT);
                SendNick(box_nick.Text);

                box_report.Text = "Connected to server.";

                thrReceive = new Thread(new ThreadStart(Receive));
                thrReceive.Start();

                box_report.Text = "Please select your file and press \"Upload\" button.";

                btn_sendmessage.Enabled = true;
                btn_disconnect.Enabled = true;
                btn_connect.Enabled = false;
            }
            catch
            {
                box_port.Text = "Cannot connected to the specified server.";
                box_report.Text = "terminating...";
            }
        }

        private void SendNick(string text)
        {
            byte[] buffer = Encoding.Default.GetBytes(text);
            client.Send(buffer);
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            terminating = true;
            box_report.Text = "Goodbye.";
            client.Close();

            btn_disconnect.Enabled = false;
            btn_connect.Enabled = true;
            btn_sendmessage.Enabled = false;
            box_ip.Enabled = true;
            box_port.Enabled = true;
            box_nick.Enabled = true;
            box_nick.Text = "";
            box_ip.Text = "";
            box_port.Text = "";
        }

    }
}
