using GameEngine;
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

        public static SetupData GetDefault()
        {
            return new SetupData()
            {
                DeckFactory = DeckFactoryType.Unique,
                Cardpool = new List<ICard>() {
                    new Card_Wisp(),
                    new Card_Shadow_Rager(),
                    new Card_Dr_Boom()
                },
                printer = PrinterType.ResultPrint,
                DeckSize = 5,
                MaxDuplicates = 3
            };
        }
    }
    public enum PrinterType { NOTSET, NoPrint, ResultPrint, AllPrint }
    public enum DeckFactoryType { NOTSET, Random, Unique };
}