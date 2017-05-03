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
        private int matches;

        public GameSession(IAI player1, IAI player2)
        {
            players = new List<IAI>();
            players.Add(player1);
            players.Add(player2);
        }

        public void PlayGames(int gamesPlayedPrDeckMultiplier, List<Deck> decks, PlayerSetup p1, PlayerSetup p2,int startCards)
        {
            for (int deckNr = 0; deckNr < decks.Count; deckNr++)//For each deck
            {
                for (int oppoNr = (deckNr + 1); oppoNr < decks.Count; oppoNr++)//For each opponent
                {
                    for (int gameNr = 0; gameNr < gamesPlayedPrDeckMultiplier; gameNr++)//For each multiplier, play a game
                    {
                        var res = PlayGame(p1, decks[deckNr], p2, decks[oppoNr], startCards);
                        decks[deckNr].AddResult(res);
                        decks[oppoNr].AddResult(res);
                        matches++;
                    }
                }
            }
            //Console.WriteLine("Matches " + matches);
        }

        private bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public MatchResult PlayGame(PlayerSetup p1,Deck deck1, PlayerSetup p2,Deck deck2,int startCards)
        {
            BoardState board = new BoardState(p1,deck1,p2,deck2, startCards);
            currentPlayer = board.GetPlayerNumberGoingFirst();
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

        public void PlayGames(int gamesEachDeckMustPlay, List<Deck> decks, PlayerSetup p1Setup, PlayerSetup p2Setup, object startCards)
        {
            throw new NotImplementedException();
        }
    }
}
