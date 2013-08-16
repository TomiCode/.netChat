﻿using System;
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
        static ServerCore core;

        static void Main(string[] args)
        {
            MainFunctions.ConsoleWrite(" Loading Server..", ConsoleColor.Yellow);

            core = new ServerCore();
            core.initServer(PORT_NUMBER);
        }

        public static void updateClientList()
        {
            for (int i = 0; i < core.clientList.Count; i++)
            {
                if (core.clientList[i].isConnected() == false)
                {
                    core.clientList.Remove(core.clientList[i]);
                }
            }
            MainFunctions.ConsoleWrite(" New client count: " + core.clientList.Count);
        }
    }
}
