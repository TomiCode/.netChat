﻿using System;
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
            Console.WriteLine(" Close this application with the \"!exit\" command!");

            MainClient client = new MainClient();

            client.createClient(IPAddress.Loopback, PORT_NUMBER);
        }
    }
}
