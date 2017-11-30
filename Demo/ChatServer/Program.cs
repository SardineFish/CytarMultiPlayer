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
            server.UseTCP("127.0.0.1", 36152);
            server.UseAuthenticate((uid) =>
            {
                if (uid.ToLower().Contains("dark"))
                    return false;
                return true;
            });
            server.RootRoom = new ChattingRoom();
            server.Start();
            
        }
    }
}
