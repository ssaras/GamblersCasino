using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GamblersCasino.Card;

namespace GamblersCasino
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Grate Guy's Casino!!!");
            Console.WriteLine("How many players do you have?\n");

            int playerCount = Convert.ToInt16(Console.ReadLine());
            //int playerCount = GetRandomNumber(5,15);

            Console.WriteLine("");

            List<CardResult> playerHands = new List<CardResult>();

            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine("Enter hand.");
                string input = Console.ReadLine();

                List<int> cards = ConvertToCard(input);
                List<int> suits = ConvertToSuit(input);
                
                CardResult playerHand = IsStrightFlush(cards, suits);
                if (playerHand.HasHand)
                {
                    Console.WriteLine($"Player {i} has a straight flush!");
                    playerHand.PlayerId = i;
                    playerHands.Add(playerHand);
                    continue;
                }

                playerHand = IsThreeKind(cards);
                if (playerHand.HasHand)
                {
                    Console.WriteLine($"Player {i} has a three of a kind!");
                    playerHand.PlayerId = i;
                    playerHands.Add(playerHand);
                    continue;
                }

                playerHand = IsStraight(cards);
                if (playerHand.HasHand)
                {
                    Console.WriteLine($"Player {i} has a straight!");
                    playerHand.PlayerId = i;
                    playerHands.Add(playerHand);
                    continue;
                }

                playerHand = IsFlush(suits);
                if (playerHand.HasHand)
                {
                    Console.WriteLine($"Player {i} has a flush!");
                    playerHand.PlayerId = i;
                    playerHands.Add(playerHand);
                    continue;
                }

                playerHand = IsPair(cards);
                if (playerHand.HasHand)
                {
                    Console.WriteLine($"Player {i} has a pair!");
                    playerHand.PlayerId = i;
                    playerHands.Add(playerHand);
                    continue;
                }

                playerHand = GetHighCard(cards);
                if (playerHand.HasHand)
                {
                    Console.WriteLine($"Player {i} has a high card!");
                    playerHand.PlayerId = i;
                    playerHands.Add(playerHand);
                    continue;
                }

            }

            FindBestHand(playerHands);

            Console.WriteLine("\nThanks for playing!");

            Console.WriteLine("\nStats for nerds:");
            Console.WriteLine("------------------");
            Console.WriteLine($"\nPlayers: {playerCount}");

            Console.ReadLine();
        }

        static CardResult FindBestHand(List<CardResult> playerHands)
        {
            playerHands.Sort((x, y) => x.HandType.CompareTo(y.HandType));

            List<CardResult> straightFlushes = new List<CardResult>();
            List<CardResult> threeKinds = new List<CardResult>();
            List<CardResult> straights = new List<CardResult>();
            List<CardResult> flushes = new List<CardResult>();
            List<CardResult> pairs = new List<CardResult>();
            List<CardResult> highCards = new List<CardResult>();

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
                return straightFlushes[0];
            }
            else if (straightFlushes.Count() > 1)
            {
                return GetHighCard(straightFlushes);
            }

            // Check Three Kinds
            if (threeKinds.Count() == 1)
            {
                return threeKinds[0];
            }
            else if (threeKinds.Count() > 1)
            {
                return GetHighCard(threeKinds);
            }

            // Check Straights
            if (straights.Count() == 1)
            {
                return straights[0];
            }
            else if (straights.Count() > 1)
            {
                return GetHighCard(straights);
            }

            // Check flushes
            if (flushes.Count() == 1)
            {
                return flushes[0];
            }
            else if (flushes.Count() > 1)
            {
                return GetHighCard(flushes);
            }

            // Check pairs
            if (pairs.Count() == 1)
            {
                return pairs[0];
            }
            else if (pairs.Count() > 1)
            {
                return GetHighCard(pairs);
            }

            // Check high cards
            if (highCards.Count() == 1)
            {
                return highCards[0];
            }
            
            return GetHighCard(highCards);
            
        }

        static CardResult GetHighCard(List<CardResult> cards)
        {
            cards.Sort((x, y) => x.MatchingCard.CompareTo(y.MatchingCard));

            return cards[0];
        }

        static CardResult IsStraight(List<int> cards)
        {
            int[] cardsSorted = cards.OrderBy(i => i).ToArray();
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
                Hand = cardsSorted,
                HandType = isStraight ? (int)Card.HandType.StrightFlush : (int)Card.HandType.HighCard
            };

            return cardResult;
        }

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
                Hand = isStraight.Hand,
                MatchingSuit = matchingSuit,
                HandType = hasHand ? (int)Card.HandType.StrightFlush : (int)Card.HandType.HighCard
            };

            return cardResult;
        }

        static CardResult HasThreeMatchingCards(List<int> cards)
        {
            CardResult cardResult = new CardResult();

            int[] matches = cards.GroupBy(i => i).Where(g => g.Count() == 3).Select(g => g.Key).ToArray();

            if (matches.Length > 0)
            {
                cardResult.HasHand = true;
                cardResult.MatchingCard = matches[0];
            }

            return cardResult;
        }

        static CardResult IsFlush(List<int> cards)
        {
            CardResult cardResult = HasThreeMatchingCards(cards);

            if (cardResult.HasHand)
            {
                cardResult.HandType = (int)Card.HandType.Flush;
            }

            return cardResult;
        }

        static CardResult IsThreeKind(List<int> cards)
        {
            CardResult cardResult = HasThreeMatchingCards(cards);

            if (cardResult.HasHand)
            {
                cardResult.HandType = (int)Card.HandType.ThreeKind;
            }

            return cardResult;
        }

        static CardResult IsPair(List<int> cards)
        {
            CardResult cardResult = new CardResult();

            int[] matches = cards.GroupBy(i => i).Where(g => g.Count() == 2).Select(g => g.Key).ToArray();

            if (matches.Length > 0)
            {
                cardResult.HasHand = true;
                cardResult.MatchingCard = matches[0];
            }

            return cardResult;
        }

        static CardResult GetHighCard(List<int> cards)
        {
            int[] cardsSorted = cards.OrderBy(i => i).ToArray();

            CardResult cardResult = new CardResult
            {
                HasHand = true,
                Hand = cardsSorted,
                HandType = (int)Card.HandType.HighCard
            };

            return cardResult;
        }
        
        static int GetRandomNumber(int low, int high)
        {
            Random random = new Random();
            return random.Next(low, high);
        }

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

