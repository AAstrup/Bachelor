using System;
using GameEngine;

namespace Bachelor
{
    internal class AI_DFS_Decision
    {
        private BoardState state;
        private double val;

        public AI_DFS_Decision(BoardState state, double val)
        {
            this.state = state;
            this.val = val;
        }

        public BoardState GetBoard()
        {
            return state;
        }

        public PlayerBoardState GetPlayerState(playerNr nr)
        {
            return state.GetPlayer(nr);
        }

        public double GetValue()
        {
            return val;
        }
    }
}