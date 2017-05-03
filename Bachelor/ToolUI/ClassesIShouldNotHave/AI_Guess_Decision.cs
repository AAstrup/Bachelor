using System;
using GameEngine;

namespace Bachelor
{
    internal class AI_Guess_Decision
    {
        private BoardState state;
        private double boardValue;
        private IAI_Guess_Decision decisionLogic; // If null, do nothing

        public AI_Guess_Decision(BoardState state, double boardValue)
        {
            this.state = state;
            this.boardValue = boardValue;
        }

        internal BoardState GetBoard()
        {
            return state;
        }

        internal double GetBoardValue()
        {
            return boardValue;
        }

        internal void SetBoardValue(double boardValue)
        {
            this.boardValue = boardValue;
        }

        internal void SetDecision(IAI_Guess_Decision decisionType)
        {
            this.decisionLogic = decisionType;
        }

        internal PlayerBoardState GetPlayerState(playerNr playerNr)
        {
            return state.GetPlayer(playerNr);
        }

        public void Execute(PlayerBoardState playerState)
        {
            if(decisionLogic != null)
            {
                decisionLogic.Play(state, playerState);
            }
        }
    }
}