using System;
using GameEngine;

namespace Bachelor
{
    internal class AI_Guess_Decision_Face : IAI_Guess_Decision
    {
        private ICard actionCard;

        public AI_Guess_Decision_Face(ICard actionCard)
        {
            this.actionCard = actionCard;
        }

        public void Play(BoardState board, PlayerBoardState playerState)
        {
            actionCard.Attack(playerState.opponent.Hero);
        }
    }
}