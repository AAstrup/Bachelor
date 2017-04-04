using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public class JakobsKort : CardTemplate, ICard
    {
        public JakobsKort(PlayerBoardState player, BoardState board) : base(player,board)
        { }
    }
}
