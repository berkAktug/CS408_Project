using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client
{
    class Program
    {
        //global variables
        static Socket clientSoc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static bool connected = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the IP address of the server");
            string serverIP = Console.ReadLine();
            Console.WriteLine("Enter the port number of the server");
            int portNum = int.Parse(Console.ReadLine());


            try
            {
                clientSoc.Connect(serverIP, portNum);
                connected = true;
                Console.WriteLine("Connection established...");
                Thread receiveThread = new Thread(Receive);
                receiveThread.Start();

                infiniteUserInput();
            }
            catch
            {

            }

        }

        static void infiniteUserInput()
        {
            string message = "start";
            while (message != "quit" && connected)
            {
                try
                {
                    Console.WriteLine("Enter your message:");
                    message = Console.ReadLine();

                    if (message == "quit")
                    {
                        Console.WriteLine("Bye...");
                        connected = false;
                        clientSoc.Close();
                    }

                    clientSoc.Send(Encoding.Default.GetBytes(message));
                    Console.WriteLine("Your message has been sent.");
                }
                catch
                {
                    Console.WriteLine("There is a problem! Check the connection...");
                    connected = false;
                    clientSoc.Close();

                }
            }

            connected = false;
            Console.WriteLine("Bye...");
            clientSoc.Close();

        }

        static void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    clientSoc.Receive(buffer);
                    Console.WriteLine("Server: " + Encoding.Default.GetString(buffer));
                }
                catch
                {
                    Console.WriteLine("There is a problem! Check the connection...");
                    connected = false;
                    clientSoc.Close();
                }
            }

        }

    }
}
