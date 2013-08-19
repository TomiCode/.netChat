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
        char[] usrMsg = new char[128];

        public void createClient(IPAddress serverAddress, short portNumber)
        {
            mainClient = new TcpClient();
            messageClient = new Messages();

            mainClient.Connect(serverAddress, portNumber);
            NetworkStream nStream = mainClient.GetStream();

            messageClient.createMessageThread(nStream);

            byte[] buffer;
            char oneChar = new char();

            Console.SetCursorPosition(0, 20);
            while (true)
            {
                if ((oneChar = (char)Console.Read()) == '\n')
                {
                    if (string.Join("", usrMsg) == "!exit") { break; }
                    buffer = Encoding.UTF8.GetBytes(usrMsg);

                    nStream.Write(buffer, 0, getLenght(usrMsg));
                    nStream.Flush();

                    clearCharTable(usrMsg);

                    Console.Clear();
                    messageClient.updateConsole();
                }
                else
                {
                    for (int i = 0; i < usrMsg.Length; i++)
                    {
                        if (usrMsg[i] == '\0')
                        {
                            usrMsg[i] = oneChar;
                            break;
                        }
                    }
                }
            }

            messageClient.stopMessageThread();
            mainClient.Close();
        }

        private void clearCharTable(char[] table)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != '\0')
                {
                    table[i] = '\0';
                }
            }
        }

        private int getLenght(char[] txt)
        {
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] == '\0') return i;
            }
            return -1;
        }

        public void updateUsrMsg()
        {
           
            for(int i = 0; i < usrMsg.Length; i++)
            {
                if (usrMsg[i] != '\0')
                {
                    Console.Write(usrMsg[i]);
                    Console.SetCursorPosition(i + 1, 20);
                }
            }
            
        }
    }
}
