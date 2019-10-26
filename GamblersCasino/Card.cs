using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasino
{
    class Card
    {
        public enum HandType
        {
            HighCard = 1,
            Pair,
            Flush,
            Straight,
            ThreeKind,
            StrightFlush
        }

        public enum Suits
        {
            Spades = 1,
            Diamonds,
            Clubs,
            Hearts,
        }

    }
}
