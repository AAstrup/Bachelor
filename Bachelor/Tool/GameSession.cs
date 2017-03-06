using AI;
using GameEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tool
{
    public class GameSession
    {
        List<IAI> players;
        int currentPlayer = 0;
        public GameSession(IAI player1, IAI player2)
        {
            players = new List<IAI>();
            players.Add(player1);
            players.Add(player2);
        }

        public Result[] PlayGames(int amount, DeckFactory factory1, DeckFactory factory2)
        {
            Result[] toReturn = new Result[amount];
            for (int i = 0; i < amount; i++)
            {
                toReturn[i] = PlayGame(factory1,factory2);
            }
            return toReturn;
        }

        public Result PlayGame(DeckFactory factory1, DeckFactory factory2)
        {
            BoardState board = new BoardState(factory1,factory2);
            players[0].SetPlayer(board.GetPlayer(playerNr.Player1));
            players[1].SetPlayer(board.GetPlayer(playerNr.Player2));
            while (!board.isFinished)
            {
                currentPlayer++;
                currentPlayer = currentPlayer % players.Count;
                Console.WriteLine(players[currentPlayer].GetPlayer().playerSetup.name + " started his turn");
                players[currentPlayer].TakeTurn(board, players[currentPlayer].GetPlayer());
            }
            return board.statisticResult;
        }

    }
}
