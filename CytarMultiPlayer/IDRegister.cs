using System;
using System.Collections.Generic;
using System.Text;

namespace CytarMultiPlayer
{
    public static class IDRegister
    {
        static uint nextID = 0;
        public static uint NextID { get { return nextID++; } }
    }
}
