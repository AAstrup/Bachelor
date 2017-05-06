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

        /// <summary>
        /// Plays games and adds results to the decks
        /// </summary>
        /// <param name="gamesPlayedPrDeckMultiplier">If matchupStrategyType is set to MatchupStrategy_AllMatchups, this is used to determine the matches each deck must play</param>
        /// <param name="SpecifiedAmount_gamesToPlay">if matchupStrategyType is set to MatchupStrategy_SpecifiedAmount, this is used to determine the total amount matches is to be played</param>
        /// <param name="matchupStrategyType"></param>
        /// <param name="decks"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="startCards"></param>
        public void PlayGames(int gamesPlayedPrDeckMultiplier, int SpecifiedAmount_gamesToPlay, MatchupStrategyType matchupStrategyType, List<Deck> decks, PlayerSetup p1, PlayerSetup p2,int startCards)
        {
            IMatchupStrategy matchupStrategy = GetMatchupStrategy(matchupStrategyType);// 
            matchupStrategy.ExecuteStrategy(gamesPlayedPrDeckMultiplier, SpecifiedAmount_gamesToPlay,decks,p1,p2,startCards, players);
            Console.WriteLine("Matches " +  SpecifiedAmount_gamesToPlay);
        }

        private IMatchupStrategy GetMatchupStrategy(MatchupStrategyType matchupStrategy)
        {
            if (matchupStrategy == MatchupStrategyType.All)
                return new MatchupStrategy_AllMatchups();
            else if (matchupStrategy == MatchupStrategyType.SpecifiedAmount)
                return new MatchupStrategy_SpecifiedAmount();
            else
                throw new Exception("MatchupStrategyType not set!");
        }

        private bool IsEven(int number)
        {
            return number % 2 == 0;
        }
    }
}
