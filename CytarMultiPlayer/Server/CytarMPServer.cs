using System;
using System.Collections.Generic;
using System.Text;
using Cytar;
using Cytar.Network;

namespace CytarMultiPlayer.Server
{
    public class CytarMPServer
    {

        public CytarMPServer()
        {
            CytarServer = new Cytar.Cytar();
            CustomSessionType = typeof(CytarMPSession);
        }

        public Cytar.Cytar CytarServer { get; private set; }

        public AuthAPIContext AuthAPIContext { get; set; }
        
        public Room RootRoom { get; set; }

        internal Func<string, byte[], bool> AuthCallback;

        internal Action<CytarMPSession> WaitSessionCallback;

        internal Type CustomSessionType { get; set; }


        public void UseTCP(string host,int port)
        {
            CytarServer.UseTCP(host, port);

        }

        public void UseAuthenticate(Func<string, byte[], bool> authCallback)
        {
            AuthCallback = authCallback;
        }

        public void UseAuthenticate(Func<string, bool> authCallback)
        {
            AuthCallback = (uid, pwdData) =>
            {
                return authCallback(uid);
            };
        }

        public void WaitSession(Action<CytarMPSession> callback)
        {
            WaitSessionCallback = callback;
        }

        public void WaitSession<SessionT>(Action<SessionT> callback) where SessionT : CytarMPSession
        {
            CustomSessionType = typeof(SessionT);
            WaitSessionCallback = (session) => callback(session as SessionT);
        }

        internal void SessionReady(CytarMPSession session)
        {
            if (RootRoom != null)
            {
                session.RootContext = RootRoom;
                session.Join(RootRoom);
            }
            WaitSessionCallback?.Invoke(session);
        }

        internal void AuthPassCallback(CytarMPSession session)
        {
            SessionReady(session);
        }
        public void Start()
        {
            CytarServer.CustomSession((networkSession)=>
            {
                return Activator.CreateInstance(CustomSessionType, networkSession) as Session;
            });

            CytarServer.WaitSession<CytarMPSession>((session) =>
            {
                if (AuthAPIContext != null)
                {
                    session.Join(AuthAPIContext);
                }
                else
                {
                    SessionReady(session);
                }
            });

            CytarServer.Start();
        }


    }
}
