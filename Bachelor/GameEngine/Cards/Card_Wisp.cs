using Bachelor;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Card_Wisp : CardTracker, ICard, ITarget
    {
        public Card_Wisp() : base() { }
        public Card_Wisp(Deck deck, ICard template, ITrackable templateTrack) : base(deck,template, templateTrack) { }


        public override int GetDamage()
        {
            return 1;
        }

        public override string GetNameType()
        {
            return "Wisp";
        }

        public override ICard InstantiateModel(Deck deck,BoardState board, PlayerBoardState player)
        {
            var toReturn = new Card_Wisp(deck,this,this);
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}