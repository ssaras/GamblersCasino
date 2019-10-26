﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasino
{
    class Program
    {
        static int[] CARDS = new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
        static int[] SUITS = new int[] { 1, 2, 3, 4 };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Grate Guy's Casino!!!");
            Console.WriteLine("How many players do you have?\n");

            int playerCount = 1;// Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("");

            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine($"Player {i} has joined the table!");

                List<int> cards = new List<int>() { 12, 14, 13 };
                List<int> suits = new List<int>() { 2, 2, 2 };

                //Dictionary<string, int> isStraightFlush = IsStrightFlush(cards, suits);
                //if (isStraightFlush["hasHand"] > 0)
                //{
                //    Console.WriteLine($"Player {i} has a straight flush of {isStraightFlush["matchingCard"]}'s {isStraightFlush["matchingSuit"]}'s!");
                //    continue;
                //}

                CardResult isStraight = IsStraight(cards);
                if (isStraight.HasHand)
                {
                    Console.WriteLine($"Player {i} has a straight!");
                    continue;
                }

                Dictionary<string, int> isThreeKind = IsThreeKind(cards);
                if (isThreeKind["hasHand"] > 0)
                {
                    Console.WriteLine($"Player {i} has a three of a kind of {isThreeKind["matchingCard"]}'s!");
                    continue;
                }

                Dictionary<string, int> isFlush = IsFlush(suits);
                if (isFlush["hasHand"] > 0)
                {
                    Console.WriteLine($"Player {i} has a flush of {isFlush["matchingCard"]}'s!");
                    continue;
                }

                Dictionary<string, int> isPair = IsPair(cards);
                if (isPair["hasHand"] > 0)
                {
                    Console.WriteLine($"Player {i} has a pair of {isPair["matchingCard"]}'s!");
                    continue;
                }

            }

            Console.WriteLine("\nThanks for playing!");

            Console.WriteLine("\nStats for nerds:");
            Console.WriteLine("------------------");
            Console.WriteLine($"\nPlayers: {playerCount}");

            Console.ReadLine();
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
                Hand = cardsSorted
            };

            return cardResult;
        }

        static Dictionary<string, int> IsStrightFlush(List<int> cards, List<int> suits)
        {
            int[] cardMatches = cards.GroupBy(i => i).Where(g => g.Count() == 3).Select(g => g.Key).ToArray();
            int[] suitMatches = suits.GroupBy(i => i).Where(g => g.Count() == 3).Select(g => g.Key).ToArray();

            return CreateResponseObject(cardMatches, suitMatches);
        }

        static Dictionary<string, int> IsFlush(List<int> cards)
        {
            return IsThreeKind(cards);
        }

        static Dictionary<string, int> IsPair(List<int> cards)
        {
            int[] matches = cards.GroupBy(i => i).Where(g => g.Count() == 2).Select(g => g.Key).ToArray();
            return CreateResponseObject(matches);
        }

        static Dictionary<string, int> IsThreeKind(List<int> cards)
        {
            int[] matches = cards.GroupBy(i => i).Where(g => g.Count() == 3).Select(g => g.Key).ToArray();
            return CreateResponseObject(matches);
        }

        static Dictionary<string, int> CreateResponseObject(int[] cardMatches, int[] suitMatches = null)
        {
            int hasHand = 0;
            int matchingCard = 0;
            int matchingSuit = 0;

            // Add suits if suits was passed and match was found
            if (suitMatches != null && suitMatches.Length > 0)
            {
                matchingSuit = suitMatches[0];
            }

            // Add cards if match was found
            if (cardMatches.Length > 0)
            {
                matchingCard = cardMatches[0];
            }

            // Pair or Three of a Kind
            if (suitMatches == null && matchingCard > 0)
            {
                hasHand = 1;
            }
            // Straigt Flush
            else if (suitMatches != null && matchingSuit > 0 && matchingCard > 0)
            {
                hasHand = 1;
            }
            // Straight
            else if (suitMatches != null && matchingSuit > 0 && matchingCard <= 0)
            {

            }

            Dictionary<string, int> returnObject = new Dictionary<string, int>();
            returnObject.Add("hasHand", hasHand);
            returnObject.Add("matchingCard", matchingCard);
            returnObject.Add("matchingSuit", matchingSuit);

            return returnObject;
        }

    }
}

