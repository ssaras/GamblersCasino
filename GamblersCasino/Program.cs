using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasino
{
    class Program
    {
        static readonly int[] CARDS = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Grate Guy's Casino!!!");
            Console.WriteLine("How many players do you have?\n");

            // int playerCount = Convert.ToInt16(Console.ReadLine());
            int playerCount = GetRandomNumber(2,5);

            Console.WriteLine("");

            List<CardResult> playerHands = new List<CardResult>();

            for (int i = 0; i < playerCount; i++)
            {
                List<int> cards = new List<int>()
                {
                    GetRandomNumber((int)Card.Cards.Two, (int)Card.Cards.Ace),
                    GetRandomNumber((int)Card.Cards.Two, (int)Card.Cards.Ace),
                    GetRandomNumber((int)Card.Cards.Two, (int)Card.Cards.Ace)
                };
                List<int> suits = new List<int>() {
                    GetRandomNumber((int)Card.Suits.Spades, (int)Card.Suits.Hearts),
                    GetRandomNumber((int)Card.Suits.Spades, (int)Card.Suits.Hearts),
                    GetRandomNumber((int)Card.Suits.Spades, (int)Card.Suits.Hearts)
                };

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
            return new CardResult();
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
    }
}

