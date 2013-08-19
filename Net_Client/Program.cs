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
        static MainClient client = new MainClient();

        static void Main(string[] args)
        {
            Console.WriteLine(" Close this application with the \"!exit\" command!");

            client.createClient(IPAddress.Loopback, PORT_NUMBER);
        }

        public static void updateMsg()
        {
            client.updateUsrMsg();
        }
    }
}
