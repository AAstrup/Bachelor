using GameEngine;
using System;

namespace Bachelor
{
    public class AI_Default
    {
        protected playerNr playerNr;

        public PlayerBoardState GetOriginalPlayer(BoardState board)
        {
            return board.GetPlayer(playerNr);
        }

        public void SetPlayer(playerNr playerNr)
        {
            this.playerNr = playerNr;
        }
    }
}