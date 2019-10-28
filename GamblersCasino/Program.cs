using GamblersCasino.Analyze;
using GamblersCasino.Hands;
using System;
using System.Collections.Generic;

namespace GamblersCasino
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;
            while (playAgain)
            {


                Console.WriteLine("Welcome to Grate Guy's Casino!!!");
                Console.WriteLine("How many players do you have?");

                int playerCount = Convert.ToInt16(Console.ReadLine());//1;// 

                List<Player> players = new List<Player>();

                Console.WriteLine("");
                Console.WriteLine("Enter hands");

                string playerInput = String.Empty;

                for (int i = 0; i < playerCount; i++)
                {
                    playerInput = Console.ReadLine();

                    players.Add(new Player
                    {
                        PlayerId = i,
                        Hand = new ThreeCardHand(playerInput)
                    });
                }

                PokerHand pokerHand = new PokerHand();
                List<Player> winners = pokerHand.FindBestHand(players, 2);
                string output = String.Empty;

                foreach (Player winner in winners)
                {
                    output += winner.PlayerId + " ";
                }

                Console.WriteLine("");
                Console.WriteLine(output);
                Console.ReadLine();
            }
                        
        }

        
    }
}

