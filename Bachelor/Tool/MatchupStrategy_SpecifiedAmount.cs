using AI;
using Bachelor;
using GameEngine;
using System;
using System.Collections.Generic;

namespace Tool
{
    internal class MatchupStrategy_SpecifiedAmount : MatchupStrategy_Default, IMatchupStrategy
    {
        public MatchupStrategy_SpecifiedAmount()
        {

        }
        public int ExecuteStrategy(int gamesPlayedPrDeckMultiplier, int SpecifiedAmount_gamesToPlay, List<Deck> decks, PlayerSetup p1, PlayerSetup p2, int startCards, List<IAI> players)
        {
            int matchesPlayed = 0;
            Random rand = new Random();

            for (int i = 0; i < SpecifiedAmount_gamesToPlay; i++)
            {
                int p1DeckNr = rand.Next(0, decks.Count);
                int p2DeckNr = rand.Next(0, decks.Count);
                while(p2DeckNr == p1DeckNr && decks.Count > 1)
                {
                    p2DeckNr = rand.Next(0, decks.Count);
                }
                Console.WriteLine("p1DeckNr " + p1DeckNr);
                Console.WriteLine("p2DeckNr " + p2DeckNr);

                var res = PlayGame(p1, decks[p1DeckNr], p2, decks[p2DeckNr], players, startCards);
                decks[p1DeckNr].AddResult(res);
                decks[p2DeckNr].AddResult(res);
                matchesPlayed++;
            }

            return matchesPlayed;
        }
    }
}