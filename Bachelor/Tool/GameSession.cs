using AI;
using Bachelor;
using GameEngine;
using GameEngine.Printers;
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

        public void PlayGames(int gamesPlayedPrDeck, List<Deck> deck, PlayerSetup p1, PlayerSetup p2)
        {
            int offset = deck.Count / 2;
            for (int i = 0; i < offset; i++)
            {
                for (int gameNr = 0; gameNr < gamesPlayedPrDeck; gameNr++)
                {
                    int oppponent = ((i + gameNr) % offset) + offset;
                    var res = PlayGame(p1,deck[i],p2, deck[oppponent]);
                    deck[i].AddResult(res);
                    deck[oppponent].AddResult(res);
                }
            }
        }

        public MatchResult PlayGame(PlayerSetup p1,Deck deck1, PlayerSetup p2,Deck deck2)
        {
            BoardState board = new BoardState(p1,deck1,p2,deck2);
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
