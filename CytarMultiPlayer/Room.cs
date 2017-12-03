using Cytar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CytarMultiPlayer
{
    public class Room<SubRoomT>: APIContext where SubRoomT: Room<SubRoomT>
    {
        public const string APIGetRoomsID = "GRID";
        public const string APIGetRooms = "GTRM";
        public const string APIEnterRoom = "ENRM";
        public const string APIExitRoom = "EXRM";
        [SerializableProperty(1)]
        public string Name { get; set; }

        [SerializableProperty(2)]
        public string Description { get; set; }

        [SerializableProperty(0)]
        public new uint ID { get =>base.ID; protected set => base.ID = value; }
        public Dictionary<uint, SubRoomT> SubRooms { get; protected set; }

        public Room():this("Room","A Room.")
        {
        }

        public Room(string name, string description)
        {
            this.ID = IDRegister.NextID;
            Name = name;
            Description = description;
            SubRooms = new Dictionary<uint, SubRoomT>();
        }

        public virtual void AddSubRoom(SubRoomT room)
        {
            this.Children.Add(room);
            Children.Add(room);
        }

        public virtual bool RemoveSubRoom(SubRoomT room)
        {
            if (!SubRooms.ContainsValue(room))
                return false;
            SubRooms.Remove(room.ID);
            Children.Remove(room);
            return true;
        }

        public virtual bool RemoveSubRoom(uint id)
        {
            var room = SubRooms[id];
            if (room == null)
                return false;
            SubRooms.Remove(id);
            Children.Remove(room);
            return true;
        }

        [CytarAPI(APIGetRoomsID)]
        public virtual uint[] GetSubRoomsID()
        {
            return SubRooms.Keys.ToArray();
        }

        [CytarAPI(APIGetRooms)]
        public SubRoomT[] GetSubRooms()
        {
            return SubRooms.Values.ToArray();
        }

        [CytarAPI(APIEnterRoom)]
        public void EnterRoom(CytarMPSession session,uint id)
        {
            if (!SubRooms.ContainsKey(id))
                throw new Exception("Room Not Found");

            session.Exit(this);
            session.Join(SubRooms[id]);            
        }
        [CytarAPI(APIExitRoom)]
        public void ExitRoom(CytarMPSession session)
        {
            session.Exit(this);
            if (this.Parent == null)
                session.Close(0);
            session.Join(this.Parent);
        }
    }
}
