using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CytarMultiPlayer;
using CytarMultiPlayer.Client;

namespace ChatClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                LoginAgin:

                Console.Write("Your Name: ");
                var name = Console.ReadLine();

                CytarMPClient client = new CytarMPClient();
                var session = client.Connect<Cytar.Network.TCPSession>("localhost", 36152);

                var loginSucceed = await session.CallRemoteAPIAsync<bool>("AUTH", name,new byte[0]);

                if (!loginSucceed)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Login faild");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    goto LoginAgin;
                }

                session.Join(new ChattingRoom());

                while (true)
                {
                    var text = Console.ReadLine();
                    session.CallRemoteAPI("Say", text);
                }
            }).Wait();
        }
    }
}
