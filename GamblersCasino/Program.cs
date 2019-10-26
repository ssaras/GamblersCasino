using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GamblersCasino
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Grate Guy's Casino!!!");
            Console.WriteLine("How many players do you have?");

            int playerCount = Convert.ToInt16(Console.ReadLine());

            List<CardResult> playerHands = new List<CardResult>();

            Console.WriteLine("");
            Console.WriteLine("Enter hands");

            for (int i = 0; i < playerCount; i++)
            {
                CardResult playerHand = FindHand(Console.ReadLine());
                playerHand.PlayerId = i;
                playerHands.Add(playerHand);
            }

            List<CardResult> winner = FindBestHand(playerHands);

            string output = String.Empty;

            foreach (CardResult card in winner)
            {
                output += card.PlayerId + " ";
            }

            Console.WriteLine("");
            Console.WriteLine(output);
            Console.ReadLine();
        }

        /// <summary>
        ///     Finds the best hand out of all the players hands
        /// </summary>
        /// <param name="cards">
        ///     The players' hands
        /// </param> 
        /// <returns>
        ///     A list with the highest hand. If there are more than one in the list, there is a tie
        /// </returns>
        static List<CardResult> FindBestHand(List<CardResult> playerHands)
        {
            playerHands.Sort((x, y) => x.HandType.CompareTo(y.HandType));

            List<CardResult> straightFlushes = new List<CardResult>();
            List<CardResult> threeKinds = new List<CardResult>();
            List<CardResult> straights = new List<CardResult>();
            List<CardResult> flushes = new List<CardResult>();
            List<CardResult> pairs = new List<CardResult>();
            List<CardResult> highCards = new List<CardResult>();
            List<CardResult> bestHands = new List<CardResult>();

            foreach(CardResult hand in playerHands)
            {
                switch (hand.HandType)
                {
                    case 1:
                        highCards.Add(hand);
                        break;
                    case 2:
                        pairs.Add(hand);
                        break;
                    case 3:
                        flushes.Add(hand);
                        break;
                    case 4:
                        straights.Add(hand);
                        break;
                    case 5:
                        threeKinds.Add(hand);
                        break;
                    case 6:
                        straightFlushes.Add(hand);
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
                return GetHighCard(straightFlushes);
            }

            // Check Three Kinds
            if (threeKinds.Count() == 1)
            {
                bestHands.Add(threeKinds[0]);
                return bestHands;
            }
            else if (threeKinds.Count() > 1)
            {
                return GetHighCard(threeKinds);
            }

            // Check Straights
            if (straights.Count() == 1)
            {
                bestHands.Add(straights[0]);
                return bestHands;
            }
            else if (straights.Count() > 1)
            {
                return GetHighCard(straights);
            }

            // Check flushes
            if (flushes.Count() == 1)
            {
                bestHands.Add(flushes[0]);
                return bestHands;
            }
            else if (flushes.Count() > 1)
            {
                return GetHighCard(flushes);
            }

            // Check pairs
            if (pairs.Count() == 1)
            {
                bestHands.Add(pairs[0]);
                return bestHands;
            }
            else if (pairs.Count() > 1)
            {
                return GetHighCard(pairs);
            }

            // Check high cards
            if (highCards.Count() == 1)
            {
                bestHands.Add(highCards[0]);
                return bestHands;
            }
            
            return GetHighCard(highCards);
            
        }

        /// <summary>
        ///     Recursive function that finds the hand with the highest card in a set of hands.
        /// </summary>
        /// <param name="cards">
        ///     The hands
        /// </param> 
        /// <param name="highCardIndex">
        ///     The index of the highest card to check against
        /// </param>
        /// <returns>
        ///     A list with the highest hand. If there are more than one in the list, there is a tie
        /// </returns>
        static List<CardResult> GetHighCard(List<CardResult> cards, int highCardIndex = 2)
        {
            cards.Sort((x, y) => x.HighCard.CompareTo(y.HighCard));

            int highestNumber = -1;
            List<CardResult> highCards = new List<CardResult>();

            foreach (CardResult card in cards)
            {
                // If the current card is higher than the previous card
                // clear the high card list and set the highest card
                if (card.Hand[highCardIndex] > highestNumber)
                {
                    highCards = new List<CardResult>();
                    highestNumber = card.Hand[highCardIndex];
                    highCards.Add(card);
                }
                // If the current card is equal to the highest card 
                // add it to high card list (we'll check this list again)
                else if(card.Hand[highCardIndex] == highestNumber)
                {
                    highCards.Add(card);
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

        /// <summary>
        /// Finds the best hand from stdin
        /// </summary>
        /// <param name="input">stdin</param>
        /// <returns>CardResult</returns>
        static CardResult FindHand(string input)
        {

            List<int> cards = ConvertToCard(input);
            List<int> suits = ConvertToSuit(input);

            CardResult playerHand = IsStrightFlush(cards, suits);
            if (playerHand.HasHand)
            {
                playerHand.Hand = SortHand(cards);
                playerHand.HighCard = playerHand.Hand[2];
                return playerHand;
            }

            playerHand = IsThreeKind(cards);
            if (playerHand.HasHand)
            {
                playerHand.Hand = SortHand(cards);
                playerHand.HighCard = playerHand.Hand[2];
                return playerHand;
            }

            playerHand = IsStraight(cards);
            if (playerHand.HasHand)
            {
                playerHand.Hand = SortHand(cards);
                playerHand.HighCard = playerHand.Hand[2];
                return playerHand;
            }

            playerHand = IsFlush(suits);
            if (playerHand.HasHand)
            {
                playerHand.Hand = SortHand(cards);
                playerHand.HighCard = playerHand.Hand[2];
                return playerHand;
            }

            playerHand = IsPair(cards);
            if (playerHand.HasHand)
            {
                playerHand.Hand = SortHand(cards);
                playerHand.HighCard = playerHand.Hand[2];
                return playerHand;
            }

            playerHand = GetHighCard(cards);
            playerHand.Hand = SortHand(cards);
            playerHand.HighCard = playerHand.Hand[2];

            return playerHand;
        }


        /// <summary>
        /// Finds a stright in a hand. Also used to find straight flush.
        /// </summary>
        /// <param name="cards">the hand</param>
        /// <returns>CardResult</returns>
        static CardResult IsStraight(List<int> cards)
        {
            int[] cardsSorted = SortHand(cards);
            bool isStraight = false;

            // Check Ace, Two, Three run
            if (cardsSorted[0] == 2 && cardsSorted[1] == 3 && cardsSorted[2] == 14)
            {
                isStraight = true;
            }
            // Check King Ace, Two run
            else if (cardsSorted[0] == 2 && cardsSorted[1] == 13 && cardsSorted[2] == 14)
            {
                isStraight = false;
            }
            // Check every other hand
            else
            {
                int? currentCard = null;
                int? nextCard = null;

                // If the current card + 1 matches the value of the next card
                // for every itteration, we have a straight.
                for (int i = 0; i<cardsSorted.Length -1; i++)
                {
                    currentCard = cardsSorted[i];
                    nextCard = cardsSorted[i + 1];
                    if (currentCard + 1 == nextCard)
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

            CardResult cardResult = new CardResult {
                HasHand = isStraight,
                HandType = isStraight ? (int)Card.HandType.Straight : (int)Card.HandType.HighCard
            };

            return cardResult;
        }

        /// <summary>
        /// Finds a stright flush in a hand.
        /// </summary>
        /// <param name="cards">the hand</param>
        /// <returns>CardResult</returns>
        static CardResult IsStrightFlush(List<int> cards, List<int> suits)
        {
            // Check for Straight
            CardResult isStraight = IsStraight(cards);            

            // Check for flush
            int[] suitMatches = suits.GroupBy(i => i).Where(g => g.Count() == 3).Select(g => g.Key).ToArray();

            // If suits match and theres a straight, we have a straight flush
            bool hasHand = false;
            int matchingSuit = 0;
            if (isStraight.HasHand && suitMatches.Length > 0)
            {
                hasHand = true;
                matchingSuit = suitMatches[0];
            }

            CardResult cardResult = new CardResult
            {
                HasHand = hasHand,
                MatchingSuit = matchingSuit,
                HandType = hasHand ? (int)Card.HandType.StraightFlush : (int)Card.HandType.HighCard
            };

            return cardResult;
        }

        /// <summary>
        /// Find a matching set of 3 values in a hand. Used for finding flushes and three of a kinds
        /// </summary>
        /// <param name="cards">the hand</param>
        /// <returns>CardResult</returns>
        static CardResult HasThreeMatchingCards(List<int> cards)
        {
            CardResult cardResult = new CardResult();

            int[] matches = cards.GroupBy(i => i).Where(g => g.Count() == 3).Select(g => g.Key).ToArray();

            if (matches.Length > 0)
            {
                cardResult.HasHand = true;
            }

            return cardResult;
        }

        /// <summary>
        /// Find a flush in a hand
        /// </summary>
        /// <param name="cards">the hand</param>
        /// <returns>CardResult</returns>
        static CardResult IsFlush(List<int> cards)
        {
            CardResult cardResult = HasThreeMatchingCards(cards);

            if (cardResult.HasHand)
            {
                cardResult.HandType = (int)Card.HandType.Flush;
                cardResult.MatchingSuit = cards[0];
            }

            return cardResult;
        }

        /// <summary>
        /// Find a three of a kind in a hand
        /// </summary>
        /// <param name="cards">the hand</param>
        /// <returns>CardResult</returns>
        static CardResult IsThreeKind(List<int> cards)
        {
            CardResult cardResult = HasThreeMatchingCards(cards);

            if (cardResult.HasHand)
            {
                cardResult.HandType = (int)Card.HandType.ThreeKind;
                cardResult.MatchingCard = cards[0];
            }

            return cardResult;
        }

        /// <summary>
        /// Finds a pair in a hands
        /// </summary>
        /// <param name="cards">the set of hands</param>
        /// <returns>CardResult</returns>
        static CardResult IsPair(List<int> cards)
        {
            CardResult cardResult = new CardResult();

            int[] matches = cards.GroupBy(i => i).Where(g => g.Count() == 2).Select(g => g.Key).ToArray();

            if (matches.Length > 0)
            {
                cardResult.HasHand = true;
                cardResult.MatchingCard = matches[0];
                cardResult.HandType = (int)Card.HandType.Pair;
            }

            return cardResult;
        }

        /// <summary>
        /// Sorts a hand from low to high
        /// </summary>
        /// <param name="cards">the hand to be sorted</param>
        /// <returns>
        /// a sorted array
        /// </returns>
        static int[] SortHand(List<int> cards)
        {
            return cards.OrderBy(i => i).ToArray();
        }

        /// <summary>
        /// Reurns the highest card in a hand
        /// </summary>
        /// <param name="cards">the set of hands</param>
        /// <returns>
        /// the highest hand
        /// </returns>
        static CardResult GetHighCard(List<int> cards)
        {
            int[] cardsSorted = SortHand(cards);

            CardResult cardResult = new CardResult
            {
                HasHand = true,
                Hand = cardsSorted,
                HandType = (int)Card.HandType.HighCard,
                MatchingCard = cardsSorted[2]
            };

            return cardResult;
        }
        
        /// <summary>
        /// Generates a random number between specified values
        /// </summary>
        /// <param name="low">the lowest number possible</param>
        /// <param name="high">The highest number possible</param>
        /// <returns>
        /// An int
        /// </returns>
        static int GetRandomNumber(int low, int high)
        {
            Random random = new Random();
            return random.Next(low, high);
        }

        /// <summary>
        /// Converts suits to ints
        /// </summary>
        /// <param name="input">stdin</param>
        /// <returns>
        /// A list of ints.
        /// </returns>
        static List<int> ConvertToSuit(string input)
        {
            List<int> cards = new List<int>();
            string[] strings = input.Split(new char[0]);
            string firstChar = String.Empty;
            int cardValue = -1;

            for (int i = 0; i < strings.Length; i++)
            {
                firstChar = StringInfo.GetNextTextElement(strings[i], 1);

                try
                {
                    cardValue = Int16.Parse(firstChar);
                }
                catch (Exception e)
                {
                    switch (firstChar.ToLower())
                    {
                        case "h":
                            cardValue = 1;
                            break;
                        case "d":
                            cardValue = 2;
                            break;
                        case "s":
                            cardValue = 3;
                            break;
                        case "c":
                            cardValue = 4;
                            break;
                        default:
                            break;
                    }
                }

                cards.Add(cardValue);
            }

            return cards;
        }

        /// <summary>
        /// Convert face cards to ints.
        /// </summary>
        /// <param name="input">stdin</param>
        /// <returns>
        /// a list of ints
        /// </returns>
        static List<int> ConvertToCard(string input)
        {
            List<int> cards = new List<int>();
            string[] strings = input.Split(new char[0]);
            string firstChar = String.Empty;
            int cardValue = -1;

            for (int i = 0; i < strings.Length; i++)
            {
                firstChar = StringInfo.GetNextTextElement(strings[i], 0);

                try
                {
                    cardValue = Int16.Parse(firstChar);
                }
                catch (Exception e)
                {
                    switch (firstChar.ToUpper())
                    {
                        case "T":
                            cardValue = 10;
                            break;
                        case "J":
                            cardValue = 11;
                            break;
                        case "Q":
                            cardValue = 12;
                            break;
                        case "K":
                            cardValue = 13;
                            break;
                        case "A":
                            cardValue = 14;
                            break;
                        default:
                            break;
                    }
                }

                cards.Add(cardValue);
            }

            return cards;
        }
    }
}

