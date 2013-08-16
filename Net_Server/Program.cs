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
                NetworkStream stream = clientSocket.GetStream();

                string clientData;
                int bytesRead = 0;

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    clientData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(@" Newline: {1}, Received: {0}", clientData, clientData.Contains("\n"));

                    stream.Write(buffer, 0, bytesRead);
                    stream.Flush();
                }
            }
        }
    }
}
