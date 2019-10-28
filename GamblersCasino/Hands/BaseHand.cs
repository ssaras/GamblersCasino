using GamblersCasino.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasino.Hands
{
    abstract class BaseHand 
    {
        public List<Card> Cards { get; set; }
        public Card HighCard { get; set; }
        public Rank MatchingCard { get; set; }
        public Suit MatchingSuit { get; set; }
        public HandType HandType { get; set; }

        protected virtual void SortHand()
        {
            Cards.Sort((x, y) => x.Rank.CompareTo(y.Rank));
        }

        protected virtual List<Card> ConvertToCards(string rawInput)
        {
            string[] rawHands = rawInput.Split(new char[0]);
            List<Card> cards = new List<Card>();

            for (int i = 0; i < rawHands.Length; i++)
            {
                cards.Add(new Card(rawHands[i]));
            }

            return cards;
        }

        protected virtual Card GetHighCard()
        {
            return Cards[2];
        }

        protected abstract int FindHand();
        
    }

}
