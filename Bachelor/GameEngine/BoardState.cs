using Bachelor;
using System;

namespace GameEngine
{
    public class BoardState
    {
        public Result statisticResult;
        PlayerBoardState p1;
        PlayerBoardState p2;
        public bool isFinished;
        PlayerBoardState winner;
        private PlayerBoardState loser;

        public BoardState(PlayerSetup givenP1,Deck p1Deck1, PlayerSetup givenP2, Deck p2Deck)
        {
            p1 = new PlayerBoardState(givenP1, true,p1Deck1,this);
            p2 = new PlayerBoardState(givenP2, false,p2Deck,this);
            p1.SetOpponent(p2);
            p2.SetOpponent(p1);
            statisticResult = new Result();
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
    }
}

public enum playerNr { Player1, Player2 }