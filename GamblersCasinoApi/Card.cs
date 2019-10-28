using GamblersCasinoApi.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasinoApi
{
    public class Card
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }

        public Card(string input)
        {
            ConvertToRank(input);
            ConvertToSuit(input);
        }

        /// <summary>
        /// Converts suits to ints
        /// </summary>
        /// <param name="input">stdin</param>
        /// <returns>
        /// A list of ints.
        /// </returns>
        private void ConvertToSuit(string input)
        {
            string firstChar = StringInfo.GetNextTextElement(input, 1);

            try
            {
                Suit = (Suit)Int16.Parse(firstChar);
            }
            catch (Exception e)
            {
                Suit = (Suit)Enum.Parse(typeof(Suit), firstChar.ToUpper());
            }

        }

        /// <summary>
        /// Convert face cards to ints.
        /// </summary>
        /// <param name="input">stdin</param>
        /// <returns>
        /// a list of ints
        /// </returns>
        private void ConvertToRank(string input)
        {
            string firstChar = StringInfo.GetNextTextElement(input, 0);
            try
            {
                Rank = (Rank)Int16.Parse(firstChar);
            }
            catch (Exception e)
            {
                Rank = (Rank)Enum.Parse(typeof(Rank), firstChar.ToUpper());
            }
        }

    }
    
}
