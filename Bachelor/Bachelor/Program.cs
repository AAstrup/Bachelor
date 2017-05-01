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
        private static List<ITrackable> cardpoolAsTrackable;


        static void Main(string[] args)
        {
            SimulationResults results = RunSimulation(SetupData.GetDefault());
            //Print results
            Singletons.GetPrinter().AddEmptySpaces(2);
            PrintResults(results);
        }

        /// <summary>
        /// Runs the simulation with a given setup data set.
        /// </summary>
        /// <param name="setup">Data set to be used</param>
        /// <returns></returns>
        public static SimulationResults RunSimulation(SetupData setup)
        {
            //Private configurations
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            cardpoolAsTrackable = CastToTrackable(setup.Cardpool);
            SetupGameSessionRequirements();
            
            //Injected configurations
            SetupPrinter(setup);
            IDeckFactory deckFactory = GetFactory(setup);
            List<Deck> decks = deckFactory.GenerateDecks(setup.DeckSize, setup.MaxDuplicates, setup.Cardpool);
            foreach (var item in decks)
            {
                Console.WriteLine();
            }
            int gamesPlayedPrDeck = decks.Count - 1;

            //Running game sessions
            GameSession session = new GameSession(p1, p2);
            session.PlayGames(gamesPlayedPrDeck, decks, p1Setup, p2Setup);
            
            //Assembling results 
            stopWatch.Stop();
            return new SimulationResults(decks, setup.Cardpool, stopWatch.ElapsedMilliseconds);
        }

        private static void SetupPrinter(SetupData setup)
        {
            if (setup.printer == PrinterType.NoPrint)
                Singletons.UseNoPrinter();
            else if (setup.printer == PrinterType.ResultPrint)
                Singletons.UseResultsPrinter();
            else if (setup.printer == PrinterType.AllPrint)
                Singletons.UseDetailPrinter();
            else
                throw new NullReferenceException("Printer not specified in value setup.printer");
        }

        private static IDeckFactory GetFactory(SetupData setup)
        {
            if(setup.DeckFactory == DeckFactoryType.Unique)
                return new UniqueDeckFactory();          //Used to generate unique decks
            else if(setup.DeckFactory == DeckFactoryType.Random)
                return new RandomDeckFactory(setup.AmountOfDecksToGenerate);          //Used to generate random decks
            throw new NullReferenceException("Deckfactory is null");
        }

        private static void PrintResults(SimulationResults results)
        {
            Console.WriteLine("RESULTS: Mathes played: " + GetTotalMatches(results.Decks) + ", decks " + results.Decks.Count);
            Console.WriteLine("Run time: " + (results.ElapsedMilliseconds / 1000).ToString() + " seconds");
            PrintCardsWinRates(results);
            PrintCardsAmountOfDecksWithin(results);
            PrintCardsBestDeckWinRate(results);
            PrintCardsBestDeckCardsIncluded(results);
            Console.WriteLine(Console.ReadLine());
        }

        private static void PrintCardsBestDeckCardsIncluded(SimulationResults results)
        {
            Console.WriteLine("");
            Console.WriteLine("-- Best Deck cards --");
            for (int i = 0; i < results.Cardpool.Count; i++)
            {
                string toPrint = results.Cardpool[i].GetNameType();
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

        private static void PrintCardsBestDeckWinRate(SimulationResults results)
        {
            Console.WriteLine("");
            Console.WriteLine("-- Win/loss rate of a cards best decks --");
            for (int i = 0; i < results.Cardpool.Count; i++)
            {
                string toPrint = null;
                if (cardpoolAsTrackable[i].GetDecksWithThis().Count > 0)
                    toPrint = results.Cardpool[i].GetNameType() + " " + cardpoolAsTrackable[i].GetBestDeck().GetWinLossRate() + 
                        " ( Wins " + cardpoolAsTrackable[i].GetBestDeck().GetWins() + ", losses " + cardpoolAsTrackable[i].GetBestDeck().GetLosses() + ")";
                else
                    toPrint = results.Cardpool[i].GetNameType() + " not played";
                Console.WriteLine(toPrint);
            }
        }

        private static void PrintCardsAmountOfDecksWithin(SimulationResults results)
        {
            Console.WriteLine("");
            Console.WriteLine("-- Amount of decks a card is in --");
            for (int i = 0; i < results.Cardpool.Count; i++)
            {
                string toPrint = results.Cardpool[i].GetNameType() + " " + cardpoolAsTrackable[i].GetDecksWithThis().Count;
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

        private static void PrintCardsWinRates(SimulationResults results)
        {
            Console.WriteLine("");
            Console.WriteLine("-- Win/loss rate of the cards --");
            for (int i = 0; i < results.Cardpool.Count; i++)
            {
                string toPrint = results.Cardpool[i].GetNameType() + " " + cardpoolAsTrackable[i].GetWinLossRate() + " ( Wins " + cardpoolAsTrackable[i].GetWins() + ", Losses " + cardpoolAsTrackable[i].GetLosses()+ " )"; 
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
            p1 = new AI_Guess();
            p2 = new AI_Guess();
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
