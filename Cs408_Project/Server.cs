using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace server
{
    class Program
    {
        static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static List<Socket> clientSockets = new List<Socket>();
        static bool terminating = false;
        static bool accept = true;

        static void Main(string[] args)
        {
            int portNum;

            Console.WriteLine("Enter a port number to listen:");
            portNum = int.Parse(Console.ReadLine());

            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, portNum);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);
                Console.WriteLine("Server is listening...");

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                infiniteServerInput();

            }
            catch
            {
                Console.WriteLine("There is a problem! Check the port number and try again!");
            }
        }

        static void infiniteServerInput()
        {
            string message = "start";
            while (message != "quit" && !terminating)
            {
                Console.WriteLine("You can enter a message to broadcast:");
                message = Console.ReadLine();
                Byte[] buffer = Encoding.Default.GetBytes(message);

                if (message == "quit")
                {
                    terminating = true;
                    serverSocket.Close();
                    Console.WriteLine("Bye...");
                }

                else
                {

                    foreach (Socket client in clientSockets)
                    {
                        try
                        {
                            client.Send(buffer);
                        }
                        catch
                        {
                            Console.WriteLine("There is a problem! Check the connection...");
                            terminating = true;
                            serverSocket.Close();
                        }
                    }
                }


            }
        }

        //new clients will be able to connect even though server is busy with sending messages
        static void Accept()
        {
            while (accept)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                    Console.WriteLine("New client is connected");

                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        Console.WriteLine("Server stopped working...");
                        accept = false;
                    }
                    else
                    {
                        Console.WriteLine("Problem occured in accept function...");
                    }
                }
            }
        }

        static void Receive()
        {
            bool connected = true;
            int lenClientSoc = clientSockets.Count();
            Socket thisClient = clientSockets[lenClientSoc - 1];

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);

                    Console.WriteLine("Client: " + Encoding.Default.GetString(buffer));

                }
                catch
                {
                    connected = false;
                    if (!terminating)
                    {
                        Console.WriteLine("Client has disconnected...");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                }
            }


        }
    }
}
