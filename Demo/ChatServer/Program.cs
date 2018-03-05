using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CytarMultiPlayer;
using CytarMultiPlayer.Server;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            CytarMPServer server = new CytarMPServer();
            server.Use<Cytar.Unity.Network.UnityServer>("loclahost", 36152);
            server.UseAuthenticate((uid) =>
            {
                if (uid.ToLower().Contains("dark"))
                    return false;
                return true;
            });
            ChattingRoom chattingRoom = new ChattingRoom();
            server.RootRoom = chattingRoom;
            server.Start();
            server.WaitSession((session) =>
            {
                chattingRoom.Say("Server", "A user [" + session.User.Name + "] entered the room.");
                session.Error += Session_Error;
            });
            
        }

        private static void Session_Error(Cytar.Session arg1, string arg2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(arg2);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
