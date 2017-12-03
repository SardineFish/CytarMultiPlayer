using Cytar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CytarMultiPlayer
{
    public class AuthAPIContext: APIContext
    {
        public CytarMPServer CytarMPServer { get; internal set; }
        public const string AuthAPIName = "AUTH";

        public AuthAPIContext(CytarMPServer cytarMPServer)
        {
            CytarMPServer = cytarMPServer ?? throw new ArgumentNullException(nameof(cytarMPServer));
        }

        [CytarAPI(AuthAPIName)]
        public bool Authenticate(CytarMPSession session, string username, byte[] pwdHash)
        {
            if (CytarMPServer.AuthCallback == null)
                return true;
            var authResult = CytarMPServer.AuthCallback(username, pwdHash);
            if (authResult)
            {
                session.User = new User(session, username);
                CytarMPServer.AuthPassCallback(session);
                return true;
            }
            else
            {
                session.Exit(this);
                return false;
            }
        }
    }
}
