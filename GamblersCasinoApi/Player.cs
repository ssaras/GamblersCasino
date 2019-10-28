using GamblersCasinoApi.Hands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasinoApi
{
    public class Player
    {
        public int PlayerId { get; set; }
        public BaseHand Hand { get; set; }

    }
}
