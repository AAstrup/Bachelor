using GameEngine;
using System;

namespace Bachelor
{
    public class AI_Default
    {
        protected PlayerBoardState originalPlayerState;

        public PlayerBoardState GetPlayer()
        {
            return originalPlayerState;
        }

        public void SetPlayer(PlayerBoardState playerState)
        {
            this.originalPlayerState = playerState;
        }
    }
}