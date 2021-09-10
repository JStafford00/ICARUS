using System;
using System.Collections.Generic;
using System.Text;

namespace Icarus.Data
{
    public class Aisle
    {
        private static int aisleID = 0;

        public Aisle()
        {
            aisleID++;
        }

        public int AisleID
        {
            get
            {
                return aisleID;
            }
        }

        public string AisleName
        {
            get;
            set;
        }

        public override string ToString()
        {
            return AisleName;
        }
    }
}
