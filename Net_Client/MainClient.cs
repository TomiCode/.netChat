using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Net_Client
{
    class MainClient
    {
        TcpClient mainClient = default(TcpClient);
        Messages messageClient = default(Messages);

        string[] messageBuffer = new string[16];

        public void createClient(IPAddress serverAddress, short portNumber)
        {
            mainClient = new TcpClient();
            messageClient = new Messages();

            mainClient.Connect(serverAddress, portNumber);
            NetworkStream nStream = mainClient.GetStream();

            messageClient.createMessageThread(nStream);

            byte[] buffer;
            string msg;

            Console.SetCursorPosition(0, 10);
            while ((msg = Console.ReadLine()) != "!exit")
            {
                buffer = Encoding.UTF8.GetBytes(msg);

                nStream.Write(buffer, 0, buffer.Length);
                nStream.Flush();

                Console.SetCursorPosition(0, 10);
            }

            messageClient.stopMessageThread();
            mainClient.Close();
        }
    }
}
