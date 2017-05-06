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
                Cardpool = new List<ICard>() {
                    new Card_User_Defined(9,9,0,"Jeff",false),
                    new Card_User_Defined(1,1,0,"Wisp",false),
                    new Card_User_Defined(1,1,0,"Wisp",false)
                },
                printer = PrinterType.ResultPrint,
                DeckSize = 6,
                MaxDuplicates = 6,
                StartCards = 4,
                GamesEachDeckMustPlayMultiplier = 1,
                specifiedMatchupAmount = 100,
                matchupStrategyType = MatchupStrategyType.SpecifiedAmount        
            };
            return toReturn;
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