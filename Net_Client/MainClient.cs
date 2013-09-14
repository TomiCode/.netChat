using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Net_Client
{
    public struct userData
    {
        public int userID;
        public byte[] usrNet;
        public bool isCommand;
    };

    class MainClient
    {
        TcpClient mainClient = default(TcpClient);
        Thread mThread;

        string[] mBuffer = new string[18];
        Encoding encoding = Encoding.GetEncoding("windows-1250");

        userData usrData = new userData();

        public void createClient(short portNumber)
        {
            ClientInit init = new ClientInit();
            init.newClientInit();

            mainClient = new TcpClient();
            mainClient.Connect(init.hostAddr , portNumber);

            NetworkStream nStream = mainClient.GetStream();
            usrData.userID = init.serverHandshake(nStream, encoding);

            mThread = new Thread(messageThread);
            mThread.Start(nStream);

            byte[] buffer;
            string message;

            Console.SetCursorPosition(0, 21);
            while ((message = Console.ReadLine()) != "!exit")
            {
                buffer = encoding.GetBytes(message);

                nStream.Write(buffer, 0, message.Length);
                nStream.Flush();

                updateMessages(string.Format("me: {0}", message));
                message = "";

                clearConsoleLines(1, 21);
                Console.SetCursorPosition(0, 21);
            }

            mThread.Abort();
            mainClient.Close();
        }

        private void messageThread(object obj)
        {
            NetworkStream nStream = (NetworkStream)obj;
            int readBytes;

            byte[] messageBuffer = new byte[1024];

            while (true)
            {
                if ((readBytes = nStream.Read(messageBuffer, 0, messageBuffer.Length)) != 0)
                {
                    updateMessages(encoding.GetString(messageBuffer, 0, readBytes));
                }
            }
        }

        private void updateMessages(string message)
        {
            int space;
            if ((space = freeSpace()) != -1)
            {
                mBuffer[space] = message;
            }
            else
            {
                for (int i = 0; i < mBuffer.Length - 1; i++)
                {
                    mBuffer[i] = mBuffer[i + 1];
                }
                mBuffer[mBuffer.Length - 1] = message;
            }
            updateConsole();
        }

        private void updateConsole()
        {
            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            clearConsoleLines(21, 1);
            Console.SetCursorPosition(0, 1);

            foreach (string msg in mBuffer)
            {
                if (msg != string.Empty)
                    Console.WriteLine(msg);
            }

            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

        private void clearConsoleLines(int lines, int top)
        {
            for (int i = top; i < top + lines; i++)
            {
                Console.SetCursorPosition(0, i);
                for (int j = 0; j < Console.WindowWidth; j++)
                    Console.Write(" ");
            }
        }

        private int freeSpace()
        {
            for (int i = 0; i < mBuffer.Length; i++)
            {
                if (mBuffer[i] == "") return i;
            }
            return -1;
        }
    }
}
