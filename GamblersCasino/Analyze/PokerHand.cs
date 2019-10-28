using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasino.Analyze
{
    class PokerHand : IAnalyzeHand
    {
        public List<Player> FindBestHand(List<Player> players, int highCardIndex)
        {
            List<Player> straightFlushes = new List<Player>();
            List<Player> threeKinds = new List<Player>();
            List<Player> straights = new List<Player>();
            List<Player> flushes = new List<Player>();
            List<Player> pairs = new List<Player>();
            List<Player> highCards = new List<Player>();
            List<Player> bestHands = new List<Player>();

            foreach (Player player in players)
            {
                switch (player.Hand.HandType)
                {
                    case Enums.HandType.HighCard:
                        highCards.Add(player);
                        break;
                    case Enums.HandType.Pair:
                        pairs.Add(player);
                        break;
                    case Enums.HandType.Flush:
                        flushes.Add(player);
                        break;
                    case Enums.HandType.Straight:
                        straights.Add(player);
                        break;
                    case Enums.HandType.ThreeKind:
                        threeKinds.Add(player);
                        break;
                    case Enums.HandType.StraightFlush:
                        straightFlushes.Add(player);
                        break;
                    default:
                        // u wot m8?
                        break;
                }
            }

            // Check Straight Flushes
            if (straightFlushes.Count() == 1)
            {
                bestHands.Add(straightFlushes[0]);
                return bestHands;
            }
            else if (straightFlushes.Count() > 1)
            {
                return GetHighCard(straightFlushes, highCardIndex);
            }

            // Check Three Kinds
            if (threeKinds.Count() == 1)
            {
                bestHands.Add(threeKinds[0]);
                return bestHands;
            }
            else if (threeKinds.Count() > 1)
            {
                return GetHighCard(threeKinds, highCardIndex);
            }

            // Check Straights
            if (straights.Count() == 1)
            {
                bestHands.Add(straights[0]);
                return bestHands;
            }
            else if (straights.Count() > 1)
            {
                return GetHighCard(straights, highCardIndex);
            }

            // Check flushes
            if (flushes.Count() == 1)
            {
                bestHands.Add(flushes[0]);
                return bestHands;
            }
            else if (flushes.Count() > 1)
            {
                return GetHighCard(flushes, highCardIndex);
            }

            // Check pairs
            if (pairs.Count() == 1)
            {
                bestHands.Add(pairs[0]);
                return bestHands;
            }
            else if (pairs.Count() > 1)
            {
                return GetHighCard(pairs, highCardIndex);
            }

            // Check high cards
            if (highCards.Count() == 1)
            {
                bestHands.Add(highCards[0]);
                return bestHands;
            }

            return GetHighCard(highCards, 2);
        }

        public List<Player> GetHighCard(List<Player> players, int highCardIndex)
        {
            int highestNumber = -1;
            int currentNumber;
            List<Player> highCards = new List<Player>();

            foreach (Player player in players)
            {
                currentNumber = (int)player.Hand.Cards[highCardIndex].Rank;

                // If the current card is higher than the previous card
                // clear the high card list and set the highest card
                if (currentNumber > highestNumber)
                {
                    highCards = new List<Player>();
                    highestNumber = currentNumber;
                    highCards.Add(player);
                }
                // If the current card is equal to the highest card 
                // add it to high card list (we'll check this list again)
                else if (currentNumber == highestNumber)
                {
                    highCards.Add(player);
                }
            }

            // If the list has more than one high card, 
            // check it again but against the next highest card 
            // until all cards have been checked
            if (highCardIndex > 0 && highCards.Count() > 1)
            {
                return GetHighCard(highCards, --highCardIndex);
            }
            else
            {
                return highCards;
            }

        }
    }
}
}
