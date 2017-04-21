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
        private static List<ITrackable> cardpoolAsTrackable;

        static void Main(string[] args)
        {
            //Variables to tweek
            int amountOfDecksToGenerate = 100;
            int gamesPlayedPrDeck = 100;
            int deckSize = 10;
            cardpool = GetFullCardPool();
            cardpoolAsTrackable = CastToTrackable(cardpool);
            Singletons.UseSilientPrinter();

            //Deck generation
            deckFactory = new DeckFactory(deckSize);
            List<Deck> decks = GenerateDecks(amountOfDecksToGenerate);

            //Starting the session
            SetupGameSessionRequirements();
            GameSession session = new GameSession(p1, p2);
            session.PlayGames(gamesPlayedPrDeck, decks,p1Setup,p2Setup);
            
            //Print results
            Singletons.GetPrinter().AddEmptySpaces(2);
            Console.WriteLine("RESULTS: Mathes played: " + GetTotalMatches(decks) + ", decks " + decks.Count);
            PrintCardsWinRates();
            PrintCardsAmountOfDecksWithin();
            PrintCardsBestDeckWinRate();
            PrintCardsBestDeckCardsIncluded();

            Console.WriteLine(Console.ReadLine());
        }

        private static void PrintCardsBestDeckCardsIncluded()
        {
            Console.WriteLine("");
            Console.WriteLine("-- Best Deck cards --");
            for (int i = 0; i < cardpool.Count; i++)
            {
                string toPrint = cardpool[i].GetNameType();
                Console.WriteLine(toPrint);
                var deckList = cardpoolAsTrackable[i].GetBestDeck().GetCardListCompressed();

                foreach (KeyValuePair<string,int> cardAmount in deckList)
                {
                    string toPrintDetail = cardAmount.Value + " x " + cardAmount.Key;
                    Console.WriteLine("    <" + toPrintDetail + ">");
                }
            }
        }

        private static void PrintCardsBestDeckWinRate()
        {
            Console.WriteLine("");
            Console.WriteLine("-- Win/loss rate of a cards best decks --");
            for (int i = 0; i < cardpool.Count; i++)
            {
                string toPrint = cardpool[i].GetNameType() +" " + cardpoolAsTrackable[i].GetBestDeck().GetWinLossRate();
                Console.WriteLine(toPrint);
            }
        }

        private static void PrintCardsAmountOfDecksWithin()
        {
            Console.WriteLine("");
            Console.WriteLine("-- Amount of decks a card is in --");
            for (int i = 0; i < cardpool.Count; i++)
            {
                string toPrint = cardpool[i].GetNameType() + " " + cardpoolAsTrackable[i].GetDecksWithThis().Count;
                Console.WriteLine(toPrint);
            }
        }

        private static List<ITrackable> CastToTrackable(List<ICard> cardpool)
        {
            var toReturn = new List<ITrackable>();
            foreach (var item in cardpool)
            {
                toReturn.Add((ITrackable)item);
            }
            return toReturn;
        }

        private static void PrintCardsWinRates()
        {
            Console.WriteLine("");
            Console.WriteLine("-- Win/loss rate of the cards --");
            for (int i = 0; i < cardpool.Count; i++)
            {
                string toPrint = cardpool[i].GetNameType() + " " + cardpoolAsTrackable[i].GetWinLossRate();
                Console.WriteLine(toPrint);
            }
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
            return new List<ICard>() {
                new Card_Wisp(),
                new Card_Earthen_Ring_Farseer(),
                new Card_Yeti(),
                new Card_Shadow_Rager(),
                new Card_Dr_Boom()
            };
        }
    }
}
