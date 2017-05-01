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

        public void PlayGames(int gamesPlayedPrDeck, List<Deck> decks, PlayerSetup p1, PlayerSetup p2)
        {
            for (int i = 0; i < decks.Count; i++)//For each deck
            {
                int fixedGamesPlayedPrDeck = (int) Math.Floor(gamesPlayedPrDeck/2.0);
                if (!IsEven(gamesPlayedPrDeck))
                    if(IsEven(i))
                        fixedGamesPlayedPrDeck = fixedGamesPlayedPrDeck + 1; //In case of given an odd number of games to play, increase every second element
                for (int y = 0; y < fixedGamesPlayedPrDeck; y++) // for each match
                {
                    int opponent = (i + y + 1) % decks.Count; // +1 to ensure that it doesn't fight itself in first attempt
                    if (opponent == i)
                        continue;
                    var res = PlayGame(p1, decks[i], p2, decks[opponent]);
                    decks[i].AddResult(res);
                    decks[opponent].AddResult(res);
                    matches++;
                }
            }

            //int firstHalfOffset = (int) Math.Ceiling(deck.Count / 2.0);
            //int lastHalfOffset = (int)Math.Floor(deck.Count / 2.0);
            //for (int i = 0; i < firstHalfOffset; i++)
            //{
            //    for (int gameNr = 0; gameNr < gamesPlayedPrDeck; gameNr++)
            //    {
            //        int oppponent = (int) (((i + gameNr) % Math.Floor(offset)) + Math.Ceiling(offset));
            //        var res = PlayGame(p1,deck[i],p2, deck[oppponent]);
            //        deck[i].AddResult(res);
            //        deck[oppponent].AddResult(res);
            //        matches++;
            //    }
            //}
            Console.WriteLine("Matches " + matches);
        }

        private bool IsEven(int number)
        {
            return number % 2 == 0;
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
