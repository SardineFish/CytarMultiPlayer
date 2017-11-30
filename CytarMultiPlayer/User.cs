using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Cytar;
using Cytar.Network;

namespace CytarMultiPlayer
{
    public class User
    {
        public User(CytarMPSession session, string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public string Name { get; protected set; }

        public CytarMPSession Session { get; set; }

        public IPAddress IP
        {
            get
            {
                return Session.NetworkSession.RemoteIPAdress;
            }
        }

        public int Latency { get; protected set; }

        public Protocol Protocol
        {
            get
            {
                if (Session.NetworkSession is TCPSession)
                    return Protocol.TCP;
                else if (Session.NetworkSession is UDPSession)
                    return Protocol.UDP;
                else if (Session.NetworkSession is HTTPSession)
                    return Protocol.HTTP;
                else if (Session.NetworkSession is WebSocketSession)
                    return Protocol.WebSocket;
                return Protocol.Unknown;
            }
        }

    }
}
