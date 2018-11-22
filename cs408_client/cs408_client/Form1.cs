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
        bool terminating = false;
        Socket client;

        string IP;
        int PORT;
        Thread thrReceive;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //box_report.AppendText("Please enter the server's IP, port number and your user name.");

            TextBox.CheckForIllegalCrossThreadCalls = false;

            btn_disconnect.Enabled = false;
            btn_sendmessage.Enabled = false;
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IP = box_ip.Text;
                PORT = Int32.Parse(box_port.Text);

                client.Connect(IP, PORT);
                SendString(box_send.Text);

                box_report.AppendText("\nConnected to server.");

                thrReceive = new Thread(Receive);
                thrReceive.Start();

                //box_report.AppendText("Please ask your question.");

                btn_sendmessage.Enabled = true;
                btn_disconnect.Enabled = true;
                btn_connect.Enabled = false;
            }
            catch
            {
                box_report.AppendText("\nCannot connected to the specified server.");
                box_report.AppendText("\nterminating...");
            }
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
                        box_report.AppendText("\nUsername is already exist.");

                        client.Close();

                        btn_connect.Enabled = true;
                        btn_disconnect.Enabled = false;
                        btn_sendmessage.Enabled = false;
                    }
                    else if (newmessage == "ask a question")
                    {
                        box_report.AppendText("\n" + newmessage + " and give the answer as second input.");
                        SendString("question and answer");
                    }
                    else if (newmessage == "answer the question")
                    {
                        box_report.AppendText("\nPlease answer the following question:\n" + newmessage);
                        SendString("Answer");
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        box_report.AppendText("\nConnection has been terminated...");

                        client.Close();

                        btn_connect.Enabled = true;
                        btn_disconnect.Enabled = false;
                        btn_sendmessage.Enabled = false;
                    }
                    connected = false;
                }
            }
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            terminating = true;
            box_report.AppendText("\nGoodbye.");
            client.Close();

            btn_disconnect.Enabled = false;
            btn_connect.Enabled = true;
            btn_sendmessage.Enabled = false;
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

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        void SendString(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            client.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private void btn_sendmessage_Click(object sender, EventArgs e)
        {
            byte[] buffer = Encoding.Default.GetBytes(box_send.Text);
            client.Send(buffer);
        }
    }
}
