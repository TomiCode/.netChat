using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Net_Client
{
    class Program
    {
        static short PORT_NUMBER = 15224;
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();

            client.Connect(IPAddress.Loopback, PORT_NUMBER);
            NetworkStream stream  = client.GetStream();

            byte[] buffer;
            buffer = Encoding.UTF8.GetBytes(Console.ReadLine());

            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
        }
    }
}
