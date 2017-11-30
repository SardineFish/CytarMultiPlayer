using System;
using System.Collections.Generic;
using System.Text;
using Cytar;
using Cytar.Network;

namespace CytarMultiPlayer.Client
{
    public class CytarMPClient : CytarClient
    {
        public CytarMPClient(Protocol protocol, string host, int port) : base(protocol, host, port)
        {
        }
        
    }
}
