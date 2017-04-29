using System;
using GameEngine;

namespace Bachelor
{
    internal class AI_Guess_Decision_Trade :  IAI_Guess_Decision
    {
        private ICard actionCard;
        private ITarget target;

        public AI_Guess_Decision_Trade(ICard actionCard, ITarget target) 
        {
            this.actionCard = actionCard;
            this.target = target;
        }

        public void Play(BoardState board, PlayerBoardState playerState)
        {
            actionCard.Attack(target);
        }
    }
}