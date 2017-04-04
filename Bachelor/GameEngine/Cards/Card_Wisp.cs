using System;
using System.Collections.Generic;

namespace GameEngine
{
    internal class Card_Wisp : CardTemplate, ICard, ITarget
    {
        public Card_Wisp(PlayerBoardState player, BoardState board) : base(player, board) { }

        public override int GetDamage()
        {
            return 1;
        }
    }
}