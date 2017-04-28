using GameEngine;

namespace AI
{
    public interface IAI
    {
        void TakeTurn(BoardState board,playerNr playerNr);
        PlayerBoardState GetOriginalPlayer(BoardState board);
        void SetPlayer(playerNr player1);
        //void SetPlayer(PlayerBoardState playerBoardState);
    }
}