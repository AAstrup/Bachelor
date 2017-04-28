using Bachelor;
using System;

namespace GameEngine
{
    public class BoardState
    {
        public Result statisticResult;
        internal PlayerBoardState p1;
        internal PlayerBoardState p2;
        public bool isFinished;
        internal PlayerBoardState winner;
        internal PlayerBoardState loser;

        public BoardState(BoardState original) { }
        public BoardState(PlayerSetup givenP1,Deck p1Deck1, PlayerSetup givenP2, Deck p2Deck)
        {
            p1 = new PlayerBoardState(givenP1, true,p1Deck1,this, playerNr.Player1);
            p2 = new PlayerBoardState(givenP2, false,p2Deck,this, playerNr.Player2);
            p1.SetOpponent(p2);
            p2.SetOpponent(p1);
            statisticResult = new Result();
        }

        public void Update(BoardState newBoard)
        {
            statisticResult = newBoard.GetStatistics();
            p1 = newBoard.GetPlayer(playerNr.Player1);
            p2 = newBoard.GetPlayer(playerNr.Player2);
            isFinished = newBoard.IsFinished();
            if (isFinished) {
                winner = newBoard.GetWinner();
                loser = newBoard.GetLoser();
            }
        }

        private PlayerBoardState GetLoser()
        {
            return loser;
        }

        private bool IsFinished()
        {
            return isFinished;
        }

        private Result GetStatistics()
        {
            return statisticResult;
        }

        public PlayerBoardState GetWinner()
        {
            return winner;
        }

        public void FinishGame(PlayerBoardState loser)
        {
            if (p1 == loser)
            {
                this.loser = p1;
                winner = p2;
                statisticResult.SetWinner(p2.playerSetup.name,p2.GetDeck());
                statisticResult.SetLoser(p1.playerSetup.name, p1.GetDeck());
            }
            else
            {
                this.loser = p2;
                winner = p1;
                statisticResult.SetWinner(p1.playerSetup.name, p1.GetDeck());
                statisticResult.SetLoser(p2.playerSetup.name, p2.GetDeck());
            }
            isFinished = true;
        }

        public PlayerBoardState GetPlayer(playerNr PlayerNr)
        {
            if (PlayerNr == playerNr.Player1)
                return p1;
            else
                return p2;
        }

        /// <summary>
        /// Copies the boardstate, so that changes can be added and evaluated on it, without changing the original.
        /// This also copies any field which is not set when the game is done.
        /// </summary>
        /// <returns></returns>
        public BoardState Copy()
        {
            var original = this;
            var copyBoard = new BoardState(original);
            copyBoard.p1 = original.p1.Copy(copyBoard);
            copyBoard.p2 = original.p2.Copy(copyBoard);
            copyBoard.p1.SetOpponent(copyBoard.p2);
            copyBoard.p2.SetOpponent(copyBoard.p1);
            copyBoard.statisticResult = statisticResult;
            return copyBoard;
        }
    }
}

public enum playerNr { Player1, Player2 }