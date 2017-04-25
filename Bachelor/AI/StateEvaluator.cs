using System;
using GameEngine;

namespace Bachelor
{
    public class StateEvaluator
    {
        public double Evaluate(BoardState state, playerNr playerNr)
        {
            double toReturn = 0.0;
            var me = state.GetPlayer(playerNr);
            if (me.opponent.Hero.GetHP() <= 0)
                return Double.MaxValue;
            foreach (var unit in me.GetWholeBoard())
            {
                toReturn += EvaluateUnit(unit);
            }

            foreach (var unit in me.opponent.GetWholeBoard())
            {
                toReturn -= EvaluateUnit(unit);
            }
            return toReturn;
        }

        private double EvaluateUnit(ICard unit)
        {
            return unit.GetCost();
        }
    }
}