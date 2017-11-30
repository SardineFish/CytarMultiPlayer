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
            foreach(var target in Sessions)
            {
                target.CallRemoteAPI("RcvMsg", session.User.Name, DateTime.Now.ToLongTimeString(), text);
            }
        }
    }
}
