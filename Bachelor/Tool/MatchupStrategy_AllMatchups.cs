using System;
using System.Collections.Generic;
using Bachelor;
using GameEngine;
using AI;

namespace Tool
{
    internal class MatchupStrategy_AllMatchups : MatchupStrategy_Default, IMatchupStrategy
    {
        public int ExecuteStrategy(int gamesPlayedPrDeckMultiplier, int SpecifiedAmount_gamesToPlay, List<Deck> decks, PlayerSetup p1, PlayerSetup p2, int startCards, List<IAI> players)
        {
            int matchesPlayed = 0;
            for (int deckNr = 0; deckNr < decks.Count; deckNr++)//For each deck
            {
                for (int oppoNr = (deckNr + 1); oppoNr < decks.Count; oppoNr++)//For each opponent
                {
                    for (int gameNr = 0; gameNr < gamesPlayedPrDeckMultiplier; gameNr++)//For each multiplier, play a game
                    {
                        var res = PlayGame(p1, decks[deckNr], p2, decks[oppoNr],players, startCards);
                        decks[deckNr].AddResult(res);
                        decks[oppoNr].AddResult(res);
                        matchesPlayed++;
                    }
                }
            }
            return matchesPlayed;
        }
    }
}