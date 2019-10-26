using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasino
{
    class CardResult
    {
        //enum _HandType { HighCard = 1, Pair, Flush, Straight, ThreeKind, StrightFlush }

        public bool HasHand { get; set; }
        public int MatchingCard { get; set; }
        public int MatchingSuit { get; set; }
        public int[] Hand { get; set; }
        public int HandType { get; set; }

        public CardResult()
        {
            HasHand = false;
            MatchingCard = 0;
            MatchingSuit = 0;
            Hand = new int[] { };
            HandType = (int)Card.HandType.HighCard;
        }

    }
}
