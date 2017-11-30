using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CytarMultiPlayer;
using Cytar;

namespace ChatClient
{
    public class ChattingRoom: APIContext
    {
        [CytarAPI("RcvMsg")]
        public void RecieveMessage(string sender,string time,string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[" + sender + "]");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("[" + time + "]");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(":");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);

        }
    }
}
