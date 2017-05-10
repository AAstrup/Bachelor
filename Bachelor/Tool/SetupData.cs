using GameEngine;
using System.Collections.Generic;
using System;

namespace Bachelor
{
    public class SetupData
    {
        public int AmountOfDecksToGenerate;
        public int DeckSize;
        public int MaxDuplicates;
        public DeckFactoryType DeckFactory;
        public PrinterType printer;
        public List<ICard> Cardpool;
        public int GamesEachDeckMustPlayMultiplier;
        public int StartCards;
        public MatchupStrategyType matchupStrategyType;
        public int specifiedMatchupAmount;

        public static SetupData GetDefault()
        {
            var toReturn = new SetupData()
            {
                DeckFactory = DeckFactoryType.Unique,
                Cardpool = new List<ICard>(),
                printer = PrinterType.ResultPrint,
                DeckSize = 5,
                MaxDuplicates = 1,
                StartCards = 2,
                GamesEachDeckMustPlayMultiplier = 2,
                matchupStrategyType = MatchupStrategyType.All
            };
            toReturn.Cardpool.Add(new Card_User_Defined(8, 8, 0, "[Card Correct 1]"));
            toReturn.Cardpool.Add(new Card_User_Defined(8, 8, 0, "[Card Correct 2]"));
            toReturn.Cardpool.Add(new Card_User_Defined(8, 8, 0, "[Card Correct 3]"));
            toReturn.Cardpool.Add(new Card_User_Defined(8, 8, 0, "[Card Correct 4]"));
            toReturn.Cardpool.Add(new Card_User_Defined(8, 8, 0, "[Card Correct 5]"));
            toReturn.Cardpool.Add(new Card_User_Defined(1, 1, 0, "[Card Incorrect 1]"));
            toReturn.Cardpool.Add(new Card_User_Defined(1, 1, 0, "[Card Incorrect 2]"));
            toReturn.Cardpool.Add(new Card_User_Defined(1, 1, 0, "[Card Incorrect 3]"));
            toReturn.Cardpool.Add(new Card_User_Defined(1, 1, 0, "[Card Incorrect 4]"));
            toReturn.Cardpool.Add(new Card_User_Defined(1, 1, 0, "[Card Incorrect 5]"));



            return toReturn;
        }

        public static SetupData GetRealSetup()
        {
            var toReturn = new SetupData()
            {
                DeckFactory = DeckFactoryType.Unique,
                Cardpool = new List<ICard>(),
                printer = PrinterType.ResultPrint,
                DeckSize = 5,
                MaxDuplicates = 1,
                StartCards = 2,
                GamesEachDeckMustPlayMultiplier = 10,
                matchupStrategyType = MatchupStrategyType.All,
            };
            toReturn.Cardpool = GetDefaultCards();

            return toReturn;
        }

        private static List<ICard> GetDefaultCards()
        {
            return new List<ICard>()
            {
                new Card_User_Defined(7,6,2,"Dr Boom"),
                new Card_User_Defined(1,1,1,"Wisp"),
                new Card_User_Defined(1,3,3,"IronClaw Bear"),
                new Card_User_Defined(2,6,2,"Ivory Knight"),
                new Card_User_Defined(3,2,1,"Iron Golem"),
                new Card_User_Defined(1,4,1,"Bloodfen raptor"),
                new Card_User_Defined(2,4,2,"Edwin VanCleef"),
                new Card_User_Defined(3,2,3,"Piloted shredder")
            };
        }

        internal static SetupData GetTest()
        {
            var toReturn = GetDefault();
            var Cardpool = new List<ICard>()
            {
                new Card_User_Defined(7, 7, 0, "Winner"),
                new Card_User_Defined(1, 1, 100, "Loser"),
                new Card_User_Defined(1, 1, 100, "Loser"),
                new Card_User_Defined(1, 1, 100, "Loser"),
                new Card_User_Defined(1, 1, 100, "Loser"),
                new Card_User_Defined(1, 1, 100, "Loser"),
                new Card_User_Defined(1, 1, 100, "Loser")
            };
            toReturn.GamesEachDeckMustPlayMultiplier = 1;
            toReturn.MaxDuplicates = 3;
            toReturn.DeckSize = 6;

            toReturn.printer = PrinterType.AllPrint;
            return toReturn;
        }


        internal static SetupData GetTestJakob()
        {
            var toReturn = GetDefault();
            toReturn.Cardpool = new List<ICard>()
            {
                new Card_User_Defined(7, 7, 1, "Winner"),
                new Card_User_Defined(6, 6, 1, "Loser")
            };
            toReturn.MaxDuplicates = 10;
            toReturn.DeckSize = 4;
            toReturn.StartCards = 2;
            toReturn.GamesEachDeckMustPlayMultiplier = 10;
            toReturn.AmountOfDecksToGenerate = 100;

            toReturn.DeckFactory = DeckFactoryType.Random;
            return toReturn;
        }
    }
    public enum PrinterType { NOTSET, NoPrint, ResultPrint, AllPrint }
    public enum DeckFactoryType { NOTSET, Random, Unique };
    public enum MatchupStrategyType { NOTSET, All, SpecifiedAmount }
}