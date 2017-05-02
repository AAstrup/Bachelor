﻿using GameEngine;
using System.Collections.Generic;

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
                    new Card_User_Defined(7,7,0),
                    new Card_User_Defined(1,1,0)
                },
                printer = PrinterType.ResultPrint,
                DeckSize = 3,
                MaxDuplicates = 2,
                StartCards = 2
            };
            toReturn.GamesEachDeckMustPlayMultiplier = toReturn.Cardpool.Count * 10;
            return toReturn;
        }
    }
    public enum PrinterType { NOTSET, NoPrint, ResultPrint, AllPrint }
    public enum DeckFactoryType { NOTSET, Random, Unique };
}