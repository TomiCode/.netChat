using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Net_Server
{
    class MainFunctions
    {
        public static void ConsoleWrite(string text, ConsoleColor color = ConsoleColor.Gray)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine(text);
            Console.ForegroundColor = previousColor;
        }
    }
}
