﻿using System;
using System.Collections.Generic;

namespace GameEngine
{
    internal class BaseCard : CardTemplate, ICard, ITarget
    {
        public BaseCard(PlayerBoardState player, BoardState board) : base(player, board) { }

        public override int GetDamage()
        {
            return 1;
        }
    }
}