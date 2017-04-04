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
            PlayerSetup p1Setup = new PlayerSetup("Jakob", 10);
            DeckFactory player1Factory = new DeckFactory(p1Setup, GetFullCardPool());
            PlayerSetup p2Setup = new PlayerSetup("Alexander", 10);
            DeckFactory player2Factory = new DeckFactory(p2Setup, GetFullCardPool());
            //Starting the session
            GameSession session = new GameSession(p1, p2);
            var result = session.PlayGames(10, player1Factory, player2Factory);
            //Print results
            AddEmptySpaces(2);

            Console.WriteLine("Got " + result.Length + " results");
            PrintWinRates(result);

            Console.WriteLine(Console.ReadLine());
        }

        private static List<ICard> GetFullCardPool()
        {
            return new List<ICard>() { new Card_Wisp(), new Card_Earthen_Rin_Farseer(), new Card_Yeti() };
        }

        private static void PrintWinRates(Result[] results)
        {
            Dictionary<string, int> wins = new Dictionary<string, int>();
            string playerName1 = results[0].winner;
            string playerName2 = results[0].loser;
            wins.Add(playerName1, 0);
            wins.Add(playerName2, 0);

            foreach (var result in results)
            {
                wins[result.winner] = wins[result.winner] + 1;
            }

            double totalWins = wins[playerName1] + wins[playerName2];
            foreach (KeyValuePair<string,int> playerWins in wins)
            {
                Console.WriteLine(playerWins.Key + " has the winRate " + ((wins[playerWins.Key]/totalWins)*100.0) + "%, with wins " + playerWins.Value);
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
