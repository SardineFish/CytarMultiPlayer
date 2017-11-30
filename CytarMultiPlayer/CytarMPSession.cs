using System;
using System.Collections.Generic;
using System.Text;
using Cytar;
using Cytar.Network;

namespace CytarMultiPlayer
{
    public class CytarMPSession : Cytar.Session
    {

        public CytarMPSession(NetworkSession netSession) : base(netSession)
        {
        }

        public User User { get; set; }

        public new NetworkSession NetworkSession
        {
            get
            {
                return base.NetworkSession;
            }
            set
            {
                base.NetworkSession = value;
            }
        }
    }
}
