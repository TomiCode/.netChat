using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Net_Client
{
    class Messages
    {
        protected Thread mThread;
        private string[] mBuffer = new string[16];

        public void createMessageThread(NetworkStream stream)
        {
            mThread = new Thread(messageThread);
            mThread.Start(stream);
        }

        public void stopMessageThread()
        {
            if (mThread != null)
            {
                mThread.Abort();
            }
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
                    updateMessages(Encoding.UTF8.GetString(messageBuffer, 0, readBytes));
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
        }

        private int freeSpace()
        {
            for (int i = 0; i < mBuffer.Length; i++)
            {
                if (mBuffer[i] == string.Empty) return i;
            }
            return -1;
        }
    }
}
