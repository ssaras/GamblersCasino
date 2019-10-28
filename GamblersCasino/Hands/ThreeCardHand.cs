using GamblersCasino.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamblersCasino.Hands
{
    class ThreeCardHand : BaseHand
    {

        public ThreeCardHand(string rawInput)
        {
            Cards = ConvertToCards(rawInput);

            SortHand();

            HighCard = GetHighCard();

            HandType = (HandType)FindHand();
        }

        public HandType GetHandType()
        {
            return HandType;
        }

        protected override int FindHand()
        {
            int handTypes;

            for (handTypes = Enum.GetValues(typeof(HandType)).Length; handTypes > 1; handTypes--)
            {
                if (IsStraightFlush())
                {
                    Console.WriteLine("straight flush");
                    break;
                }
                else if (IsThreeKind())
                {
                    Console.WriteLine("three kind");
                    break;
                }
                else if (IsStraight())
                {
                    Console.WriteLine("straight");
                    break;
                }
                else if (IsFlush())
                {
                    Console.WriteLine("flush");
                    break;
                }
                else if (IsPair())
                {
                    Console.WriteLine("pair");
                    break;
                }
                else
                {
                    Console.WriteLine("high card");
                    break;
                }
            }

            return handTypes;
        }

        public bool IsStraightFlush()
        {
            return IsStraight() && IsFlush();
        }

        public bool IsThreeKind()
        {
            List<Rank> matches = Cards.GroupBy(i => i.Rank).Where(g => g.Count() == 3).Select(g => g.Key).ToList();

            if (matches.Count > 0)
            {
                MatchingCard = matches[0];
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsStraight()
        {
            bool isStraight = false;

            // Check Ace, Two, Three run
            if (Cards[0].Rank == Rank.Two && Cards[1].Rank == Rank.Three && Cards[2].Rank == Rank.A)
            {
                isStraight = true;
            }
            // Check King Ace, Two run
            else if (Cards[0].Rank == Rank.Two && Cards[1].Rank == Rank.K && Cards[2].Rank == Rank.A)
            {
                isStraight = false;
            }
            // Check every other hand
            else
            {
                Card currentCard;
                Card nextCard;

                // If the current card + 1 matches the value of the next card
                // for every itteration, we have a straight.
                for (int i = 0; i < Cards.Count() - 1; i++)
                {
                    currentCard = Cards[i];
                    nextCard = Cards[i + 1];
                    if ((int)currentCard.Rank + 1 == (int)nextCard.Rank)
                    {
                        isStraight = true;
                        continue;
                    }
                    else
                    {
                        isStraight = false;
                        break;
                    }
                }
            }

            return isStraight;
        }

        public bool IsFlush()
        {
            List<Suit> matches = Cards.GroupBy(i => i.Suit).Where(g => g.Count() == 3).Select(g => g.Key).ToList();

            if (matches.Count > 0)
            {
                MatchingSuit = matches[0];
                return true;
            }
            else
            {
                return false;
            }            
        }

        public bool IsPair()
        {
            List<Rank> matches = Cards.GroupBy(i => i.Rank).Where(g => g.Count() == 2).Select(g => g.Key).ToList();

            if (matches.Count() > 0)
            {
                MatchingCard = matches[0];
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
