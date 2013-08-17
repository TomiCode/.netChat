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

        public void createClient(IPAddress serverAddress, short portNumber)
        {
            mainClient = new TcpClient();

            mainClient.Connect(serverAddress, portNumber);
            NetworkStream nStream = mainClient.GetStream();

            Thread mThread = new Thread(messageThread);
            mThread.Start(nStream);

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

            mainClient.Close();
            mThread.Abort();
        }

        private void messageThread(object obj)
        {
            NetworkStream stream = (NetworkStream)obj;
            int readBytes;
            
            byte[] buffer = new byte[1024];

            while (true)
            {
                if ((readBytes = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    Console.SetCursorPosition(0, 2);
                    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, readBytes));

                    Console.SetCursorPosition(0, 10);
                }
            }
        }
    }
}
