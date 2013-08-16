using Net_Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Net_Server
{
    class ServerCore
    {
        TcpListener serverSocket = default(TcpListener);
        TcpClient   clientSocket = default(TcpClient);

        public List<Clients> clientList = new List<Clients>();

        public void initServer(int portNumber)
        {
            serverSocket = new TcpListener(IPAddress.Any, portNumber);
            serverSocket.Start();

            MainFunctions.ConsoleWrite(" Server Started!");

            mainServerThread();
        }

        private void mainServerThread()
        {
            while (true)
            {
                clientSocket = serverSocket.AcceptTcpClient();
                MainFunctions.ConsoleWrite(" Client connected!", ConsoleColor.White);

                Clients client = new Clients();
                client.startClientThread(clientSocket);

                clientList.Add(client);
                MainFunctions.ConsoleWrite(String.Format(" New Client Count: {0}", clientList.Count));
            }
        }
    }
}
