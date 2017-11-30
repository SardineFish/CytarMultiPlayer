using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CytarMultiPlayer;
using Cytar;

namespace ChatServer
{
    public class ChattingRoom: Room
    {
        [CytarAPI("Say")]
        public void Say(CytarMPSession session,string text)
        {
            Say(session.User.Name, text);
        }

        public void Say(string name,string text)
        {
            foreach (var target in Sessions)
            {
                target.CallRemoteAPI("RcvMsg", name, DateTime.Now.ToLongTimeString(), text);
            }
        }

        public override void OnSessionExit(Session session)
        {
            Say("Server", "The user [" + (session as CytarMPSession).User.Name + "] has left the room.");
        }
    }
}
