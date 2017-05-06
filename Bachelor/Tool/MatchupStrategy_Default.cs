using AI;
using Bachelor;
using GameEngine;
using GameEngine.Printers;
using System.Collections.Generic;

namespace Tool
{
    public class MatchupStrategy_Default
    {
        public MatchResult PlayGame(PlayerSetup p1, Deck deck1, PlayerSetup p2, Deck deck2, List<IAI> players, int startCards)
        {
            BoardState board = new BoardState(p1, deck1, p2, deck2, startCards);
            var currentPlayer = board.GetPlayerNumberGoingFirst();
            players[0].SetPlayer(playerNr.Player1);
            players[1].SetPlayer(playerNr.Player2);
            while (!board.isFinished)
            {
                currentPlayer++;
                currentPlayer = currentPlayer % players.Count;
                Singletons.GetPrinter().PlayerTurn(board.GetPlayer((playerNr)currentPlayer).playerSetup.name);
                players[currentPlayer].TakeTurn(board, (playerNr)currentPlayer);
            }
            return board.statisticResult;
        }
    }
}