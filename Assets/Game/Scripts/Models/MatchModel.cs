using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.Networking
{
    class MatchModel
    {
        public readonly ushort id;
        public static MatchModel currentMatch;
        public MatchModel(ushort _id)
        {
            id = _id;
        }       
    }
}
