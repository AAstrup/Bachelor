using System.Collections.Generic;
using Bachelor;
using GameEngine;
using AI;

namespace Tool
{
    public interface IMatchupStrategy
    {
        /// <summary>
        /// Returns the amount of matches played
        /// </summary>
        /// <param name="AllMatchup_gamesPlayedPrDeckMultiplier"></param>
        /// <param name="decks"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="startCards"></param>
        /// <param name="SpecifiedAmount_gamesToPlay"></param>
        /// <returns></returns>
        int ExecuteStrategy(int AllMatchup_gamesPlayedPrDeckMultiplier, int SpecifiedAmount_gamesToPlay, List<Deck> decks, PlayerSetup p1, PlayerSetup p2, int startCards, List<IAI> players);
    }
}