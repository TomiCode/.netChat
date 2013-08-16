using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Net_Server
{
    class Clients
    {
        private TcpClient clientSocket;
        int bytes = 0;

        public void startClientThread(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;

            Thread cThread = new Thread(clientThread);
            cThread.Start();
        }

        private void clientThread()
        {
            byte[] buffer = new byte[1024];
            NetworkStream stream;

            string clientMessage;

            do
            {
                stream = clientSocket.GetStream();

                if ((bytes = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    clientMessage = Encoding.UTF8.GetString(buffer, 0, bytes);

                    Console.WriteLine(" Client Message: {0}", clientMessage);

                    stream.Write(buffer, 0, bytes);
                    stream.Flush();

                    Console.WriteLine(" Data sended to client!\n  Bytes {0} ", bytes);
                }
            } while (bytes != 0);
            
            Console.WriteLine(" Client disconected!");
            Program.updateClientList();

            clientSocket.Close();
        }

        public bool isConnected()
        {
            if (clientSocket.Client.Poll(0, SelectMode.SelectRead))
            {
                byte[] buff = new byte[1];
                if (clientSocket.Client.Receive(buff, SocketFlags.Peek) == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
