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

        public static SetupData GetDefault()
        {
            var toReturn = new SetupData()
            {
                DeckFactory = DeckFactoryType.Unique,
                Cardpool = new List<ICard>() {
                    new Card_User_Defined(7,7,0,"Winner",false),
                    new Card_User_Defined(1,1,0,"Loser",false)
                },
                printer = PrinterType.ResultPrint,
                DeckSize = 3,
                MaxDuplicates = 2,
                StartCards = 2,
                GamesEachDeckMustPlayMultiplier = 1
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
                new Card_User_Defined(7, 7, 0, "Winner"),
                new Card_User_Defined(0, 1, 100, "Loser1"),
            };
            toReturn.GamesEachDeckMustPlayMultiplier = 2;
            toReturn.MaxDuplicates = 10;
            toReturn.DeckSize = 3;
            toReturn.StartCards = 3;

            toReturn.printer = PrinterType.ResultPrint;
            return toReturn;
        }
    }
    public enum PrinterType { NOTSET, NoPrint, ResultPrint, AllPrint }
    public enum DeckFactoryType { NOTSET, Random, Unique };
}