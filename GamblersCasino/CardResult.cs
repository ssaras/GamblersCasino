using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasino
{
    class CardResult
    {     

        // The ID of the Player
        public int PlayerId { get; set; }

        // Whether the Player has a hand
        public bool HasHand { get; set; }

        // The matching card for pairs and three of a kinds
        public int MatchingCard { get; set; }

        // The matching suit for flushes and straight flushes
        public int MatchingSuit { get; set; }

        // The hand the player got dealt, sorted from lowest to highest
        public int[] Hand { get; set; }

        // The type of hand the player has
        public int HandType { get; set; }

        // The highest card in the players hand
        public int HighCard { get; set; }

        public CardResult()
        {
            PlayerId = -1;
            HasHand = false;
            MatchingCard = 0;
            MatchingSuit = 0;
            Hand = new int[] { };
            HandType = (int)Card.HandType.HighCard;
            HighCard = 0;
        }

    }
}
