using GameEngine;

namespace AI
{
    public interface IAI
    {
        void TakeTurn(BoardState board, PlayerBoardState playerState);
        PlayerBoardState GetPlayer();
        void SetPlayer(PlayerBoardState playerBoardState);
    }
}