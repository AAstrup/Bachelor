using AI;
using GameEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Tool;

namespace Bachelor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Players
            AI_Random p1 = new AI_Random();
            AI_Random p2 = new AI_Random();
            //Factories/deck
            PlayerSetup p1Setup = new PlayerSetup("Jakob", 0);
            DeckFactory player1Factory = new DeckFactory(p1Setup);
            PlayerSetup p2Setup = new PlayerSetup("Alexander", 15);
            DeckFactory player2Factory = new DeckFactory(p2Setup);
            //Starting the session
            GameSession session = new GameSession(p1, p2);
            var result = session.PlayGames(1, player1Factory, player2Factory);
            //Print results
            AddEmptySpaces(2);

            Console.WriteLine("Got " + result.Length + " results");
            PrintWinRates(result);

            Console.WriteLine(Console.ReadLine());
        }

        private static void PrintWinRates(Result[] result)
        {
            Dictionary<string, int> wins = new Dictionary<string, int>();
            string playerName1 = result[0].winner;
            string playerName2 = result[0].loser;
            wins.Add(playerName1, 0);
            wins.Add(playerName2, 0);

            foreach (var item in result)
            {
                wins[item.winner] = wins[item.winner] + 1;
            }

            int totalWins = wins[playerName1] + wins[playerName2];
            foreach (KeyValuePair<string,int> playerWins in wins)
            {
                Console.WriteLine(playerWins.Key + " has the winRate " + ((wins[playerWins.Key]/totalWins)*100) + "%, with wins " + playerWins.Value);
            }
        }

        private static void AddEmptySpaces(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Console.WriteLine("");
            }
        }
    }
}
