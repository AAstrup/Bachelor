using AI;
using GameEngine;
using GameEngine.Printers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Tool;

namespace Bachelor
{
    class Program
    {
        private static IAI p1;
        private static IAI p2;
        private static PlayerSetup p1Setup;
        private static PlayerSetup p2Setup;
        private static List<ICard> cardpool;
        private static List<ITrackable> cardpoolAsTrackable;

        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            //Variables to tweek

            int amountOfDecksToGenerate = 199;
            int deckSize = 3;   //This is heavy as of now, keep this value low or experience long wait time
            int maxDuplicates = 2;
            cardpool = GetFullCardPool();

            cardpoolAsTrackable = CastToTrackable(cardpool);
            Singletons.UseSilientPrinter();

            IDeckFactory deckFactory = null;
            //deckFactory = new UniqueDeckFactory();          //Used to generate unique decks
            deckFactory = new DeckFactory(amountOfDecksToGenerate);          //Used to generate random decks
            List<Deck> decks = deckFactory.GenerateDecks(deckSize, maxDuplicates, cardpool);
            int gamesPlayedPrDeck = decks.Count - 1;

            //Running game sessions
            SetupGameSessionRequirements();
            GameSession session = new GameSession(p1, p2);
            session.PlayGames(gamesPlayedPrDeck, decks,p1Setup,p2Setup);

            //Print results
            stopWatch.Stop();
            Singletons.GetPrinter().AddEmptySpaces(2);
            PrintResults(stopWatch, decks);
        }

        private static void PrintResults(Stopwatch stopWatch,List<Deck> decks)
        {
            Console.WriteLine("RESULTS: Mathes played: " + GetTotalMatches(decks) + ", decks " + decks.Count);
            Console.WriteLine("Run time: " + (stopWatch.ElapsedMilliseconds / 1000).ToString() + " seconds");
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
                if (cardpoolAsTrackable[i].GetDecksWithThis().Count == 0) {
                    Console.WriteLine("    Not played");
                    continue;
                }
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
                string toPrint = null;
                if (cardpoolAsTrackable[i].GetDecksWithThis().Count > 0)
                    toPrint = cardpool[i].GetNameType() + " " + cardpoolAsTrackable[i].GetBestDeck().GetWinLossRate();
                else
                    toPrint = cardpool[i].GetNameType() + " not played";
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

        private static void SetupGameSessionRequirements()
        {
            p1 = new AI_Dijkstra();
            p2 = new AI_Dijkstra();
            p1Setup = new PlayerSetup("P1");
            p2Setup = new PlayerSetup("P2");
        }

        private static List<ICard> GetFullCardPool()
        {
            var toReturn = new List<ICard>() {
                new Card_Wisp(),
                new Card_Earthen_Ring_Farseer(),
                new Card_Yeti(),
                new Card_Shadow_Rager(),
                new Card_Dr_Boom()
            };
            for (int i = 0; i < toReturn.Count; i++)
            {
                toReturn[i].DEBUG_Tracetag(i.ToString() + " IS SET, THIS IS THE TEMPLATE");
            }
            return toReturn;
        }
    }
}
