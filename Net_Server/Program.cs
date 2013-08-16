using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Net_Server
{
    class Program
    {
        static short PORT_NUMBER = 15224;
        static List<Clients> clients = new List<Clients>();

        static void Main(string[] args)
        {
            byte[] buffer = new byte[1024];

            TcpListener serverSocket = new TcpListener(IPAddress.Any, PORT_NUMBER);
            TcpClient clientSocket = default(TcpClient);

            serverSocket.Start();
            Console.WriteLine(" Server Started!");

            while (true)
            {
                clientSocket = serverSocket.AcceptTcpClient();

                Console.WriteLine(" Client Connected!");

                Clients client = new Clients();
                client.startClientThread(clientSocket);
            }
        }
    }
}
