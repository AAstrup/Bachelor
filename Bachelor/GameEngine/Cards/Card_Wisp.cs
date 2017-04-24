using Bachelor;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Card_Wisp : CardTracker, ICard, ITarget
    {
        public Card_Wisp() : base() { }
        public Card_Wisp(Deck deck, ICard template, ITrackable templateTrack, bool track ) : base(deck,template, templateTrack,track) { }


        public override int GetDamage()
        {
            return 1;
        }

        public override string GetNameType()
        {
            return "Wisp";
        }

        public override ICard InstantiateModel(Deck deck,BoardState board, PlayerBoardState player, bool track = true)
        {
            var toReturn = new Card_Wisp(deck,this,this,track);
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}