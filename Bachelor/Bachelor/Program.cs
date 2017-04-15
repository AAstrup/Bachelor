using AI;
using GameEngine;
using GameEngine.Printers;
using System;
using System.Collections.Generic;
using System.Text;
using Tool;

namespace Bachelor
{
    class Program
    {
        private static AI_Random p1;
        private static AI_Random p2;
        private static PlayerSetup p1Setup;
        private static PlayerSetup p2Setup;
        private static DeckFactory deckFactory;
        private static List<ICard> cardpool;

        static void Main(string[] args)
        {
            //Variables to tweek
            int amountOfDecksToGenerate = 1000;
            int gamesPlayedPrDeck = 10;
            cardpool = GetFullCardPool();
            Singletons.UseSilientPrinter();

            //Deck generation
            deckFactory = new DeckFactory();
            List<Deck> decks = GenerateDecks(amountOfDecksToGenerate);

            //Starting the session
            SetupGameSessionRequirements();
            GameSession session = new GameSession(p1, p2);
            session.PlayGames(gamesPlayedPrDeck, decks,p1Setup,p2Setup);
            
            //Print results
            Singletons.GetPrinter().AddEmptySpaces(2);
            Console.WriteLine("RESULTS: Mathes played: " + GetTotalMatches(decks) + ", decks " + decks.Count);

            Console.WriteLine(Console.ReadLine());
        }

        private static string GetTotalMatches(List<Deck> decks)
        {
            int sum = 0;
            for (int i = 0; i < decks.Count; i++)
            {
                if (decks[i].GetResults() != null)
                    sum += decks[i].GetResults().Count;
            }
            return (sum/2).ToString();
        }

        private static List<Deck> GenerateDecks(int amountOfDecksToGenerate)
        {
            var toReturn = new List<Deck>();
            var cardPool = GetFullCardPool();
            for (int i = 0; i < amountOfDecksToGenerate; i++)
            {
                toReturn.Add(deckFactory.GenerateDeck(cardpool));
            }
            return toReturn;
        }

        private static void SetupGameSessionRequirements()
        {
            p1 = new AI_Random();
            p2 = new AI_Random();
            p1Setup = new PlayerSetup("P1");
            p2Setup = new PlayerSetup("P2");
        }

        private static List<ICard> GetFullCardPool()
        {
            return new List<ICard>() { new Card_Wisp(), new Card_Earthen_Ring_Farseer(), new Card_Yeti() };
        }
    }
}
