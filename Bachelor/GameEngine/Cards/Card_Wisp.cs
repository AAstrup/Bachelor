using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Card_Wisp : CardTemplate, ICard, ITarget
    {
        public Card_Wisp() : base() { }

        public override int GetDamage()
        {
            return 1;
        }

        public override ICard Copy(BoardState board, PlayerBoardState player)
        {
            var toReturn = new Card_Wisp();
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}