using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamblersCasino
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Grate Guy's Casino!!!");
            Console.WriteLine("How many players do you have?\n");

            int playerCount = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("");

            for (int i = 0; i < playerCount; i++)
            {
                Console.WriteLine($"Player {i} has joined the table!");
            }

            //int[] cards = new int[] { 1, 2, 3 };

            //if (HasDuplicates(cards))
            //{

            //}

            Console.WriteLine("\nThanks for playing!");

            Console.WriteLine("\nStats for nerds:");
            Console.WriteLine("------------------");
            Console.WriteLine($"\nPlayers: {playerCount}");

            Console.ReadLine();
        }

        static bool HasDuplicates(int[] cards)
        {
            return true;
        }
    }
}

