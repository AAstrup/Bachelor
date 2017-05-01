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
        public int GamesEachDeckMustPlay;

        public static SetupData GetDefault()
        {
            var toReturn = new SetupData()
            {
                DeckFactory = DeckFactoryType.Unique,
                Cardpool = new List<ICard>() {
                    //new Card_Wisp(),
                    new Card_Shadow_Rager(),
                    new Card_Dr_Boom()
                    //new Card_User_Defined(1,1,0,"Loser",true),
                    //new Card_User_Defined(7,7,0,"Winner",true)
                },
                printer = PrinterType.ResultPrint,
                DeckSize = 2,
                MaxDuplicates = 2
            };
            toReturn.GamesEachDeckMustPlay = toReturn.Cardpool.Count;
            return toReturn;
        }
    }
    public enum PrinterType { NOTSET, NoPrint, ResultPrint, AllPrint }
    public enum DeckFactoryType { NOTSET, Random, Unique };
}