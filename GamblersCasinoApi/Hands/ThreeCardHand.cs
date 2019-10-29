using GamblersCasinoApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamblersCasinoApi.Hands
{
    public class ThreeCardHand : BaseHand
    {

        public ThreeCardHand(string rawInput)
        {
            Cards = ConvertToCards(rawInput);

            SortHand();

            HighCard = GetHighCard();

            HandType = (HandType)FindHand();
        }

        protected override void SortHand()
        {
            base.SortHand();

            if (Cards[0].Rank == Rank.Two && Cards[1].Rank == Rank.Three && Cards[2].Rank == Rank.A)
            {
                Cards[2].Rank = Rank.AceLow;
                base.SortHand();
            }
        }

        protected override int FindHand()
        {

            if (IsStraightFlush())
            {
                return 6;
            }
            else if (IsThreeKind())
            {
                return 5;
            }
            else if (IsStraight())
            {
                return 4;
            }
            else if (IsFlush())
            {
                return 3;
            }
            else if (IsPair())
            {
                return 2;
            }
            else
            {
                return 1;
            }
            
        }

        private bool IsStraightFlush()
        {
            return IsStraight() && IsFlush();
        }

        private bool IsThreeKind()
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

        private bool IsStraight()
        {
            bool isStraight = false;

            // Check King Ace, Two run
            if (Cards[0].Rank == Rank.Two && Cards[1].Rank == Rank.K && Cards[2].Rank == Rank.A)
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

        private bool IsFlush()
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

        private bool IsPair()
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
