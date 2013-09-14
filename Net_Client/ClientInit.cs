using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Net_Client
{
    class ClientInit
    {
        private string userName;
        public IPAddress hostAddr;

        public bool newClientInit()
        {
            Console.Write("Nickname: ");
            userName = Console.ReadLine();

            Console.Write("Connect to server: ");
            if (IPAddress.TryParse(Console.ReadLine(), out hostAddr) == false) return false;

            return true;
        }

        public int serverHandshake(NetworkStream ns, Encoding enc)
        {
            byte[] buffer = enc.GetBytes(userName);
            ns.Write(buffer, 0, buffer.Length);
            ns.Flush();

            byte[] uID = new byte[4];
            int byteCout = ns.Read(uID, 0, uID.Length);

            return BitConverter.ToInt32(uID, 0);
        }
    }
}
