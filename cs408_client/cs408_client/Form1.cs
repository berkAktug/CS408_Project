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
        static Socket client;

        static bool isAnswer = false;
        static bool isQuestion = false;

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

            box_report.AppendText("\nPlease Send your username to the server.");

            btn_disconnect.Enabled = false;
            btn_send.Enabled = false;
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                IP = box_ip.Text;
                PORT = Int32.Parse(box_port.Text);

                client.Connect(IP, PORT);
                thrReceive = new Thread(Receive);
                thrReceive.Start();

                //SendString("$nick");
                SendString("$name" + box_nick.Text);

                box_report.AppendText("\nConnected to server.");
                box_nick.Enabled = false;

                btn_send.Enabled = true;
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

                    string server_response = Encoding.Default.GetString(buffer);
                    server_response = server_response.Substring(0, server_response.IndexOf("\0"));

                    if (server_response == "dublicateNick") // error case
                    {
                        box_report.AppendText("\nUsername is already exist.");

                        client.Close();

                        btn_connect.Enabled = true;
                        btn_disconnect.Enabled = false;

                        btn_send.Enabled = false;
                        box_nick.Enabled = true;
                    }
                    else if (server_response == "#ask") // state 1
                    {
                        box_report.AppendText("\nYour turn to ask a question and give the answer.");

                        box_question.Enabled = true;
                        box_answer.Enabled = true;

                        isAnswer = false;
                        isQuestion = true;
                    }
                    else if (server_response == "#answer") // state 3
                    {
                        Byte[] buff = new byte[1024];
                        int r = client.Receive(buff);
                        if (r <= 0)
                        {
                            throw new SocketException();
                        }
                        string text = Encoding.Default.GetString(buff);
                        box_report.AppendText("\nPlease answer the fallowing question: " + text);

                        box_question.Enabled = false;
                        box_answer.Enabled = true;

                        isQuestion = false;
                        isAnswer = true;
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
                        btn_send.Enabled = false;
                    }
                    connected = false;
                }
            }
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            terminating = true;
            box_report.AppendText("\nGoodbye.");
            //SendString("$disconnect");
            // add functionality for server to notice when user disconnects.
            client.Close();

            btn_disconnect.Enabled = false;
            btn_connect.Enabled = true;
            btn_send.Enabled = false;
        }

        /// <summary>
        /// Sends a string to the server with ASCII encoding.
        /// </summary>
        void SendString(string text)
        {
            byte[] buffer = new byte[1024];
            buffer = Encoding.ASCII.GetBytes(text);
            client.Send(buffer);
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            box_report.AppendText("\nSendclick been clicked.");
            if (isQuestion)
            {
                if (!box_question.Text.Contains("?"))
                {
                    box_report.AppendText("\nPlease add \"?\" to end of your question. Otherwise the system will not accept your input.");
                }
                else
                {
                    SendString("$question");
                    string tmp = box_question.Text.Normalize() /*+ "?"*/ + box_answer.Text.Normalize();
                    box_report.AppendText("\n" + tmp);
                    SendString(tmp);
                    box_report.AppendText("\nQuestion and answer been sent.");
                }
            }
            else if (isAnswer)
            {
                SendString("$answer");
                SendString(box_answer.Text);
                box_report.AppendText("\nAnwer has been sent.");
            }
            else
            {
                box_report.AppendText("\nInside else??!?!?");
            }
        }

        private void btn_ready_Click(object sender, EventArgs e)
        {
            box_report.AppendText("\nUser is ready, sir!");
            SendString("$ready");
            btn_send.Enabled = true;
            btn_connect.Enabled = true;
        }
    }
}



