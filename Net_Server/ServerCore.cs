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

        private bool idExists(int id)
        {
            foreach (var client in clientList)
            {
                if (client.uID == id) return true;
            }
            return false;
        }

        private void mainServerThread()
        {
            while (true)
            {
                clientSocket = serverSocket.AcceptTcpClient();
                MainFunctions.ConsoleWrite(" Client connected!", ConsoleColor.White);

                byte[] handshake = new byte[512];
                int bytes = clientSocket.GetStream().Read(handshake, 0, handshake.Length);

                Clients client = new Clients();
                client.usrName = Encoding.GetEncoding("windows-1250").GetString(handshake, 0, bytes);

                for (int i = 0; i < 128; i++)
                {
                    if (idExists(i) == false)
                    {
                        client.uID = i;
                        break;
                    }
                }

                client.startClientThread(clientSocket);

                clientList.Add(client);
                MainFunctions.ConsoleWrite(String.Format(" New Client, ID: {0}, Name: {1}, Count: {2}", client.uID, client.usrName, clientList.Count));
            }
        }
    }
}
