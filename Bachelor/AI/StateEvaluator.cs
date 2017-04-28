using System;
using GameEngine;

namespace Bachelor
{
    public class StateEvaluator
    {
        public double Evaluate(BoardState state, playerNr playerNr)
        {
            double toReturn = 1000.0;//This should never go negative !!!!
            var me = state.GetPlayer(playerNr);
            if (me.opponent.Hero.GetHP() <= 0)
                return Double.MaxValue;
            toReturn += (me.Hero.GetHP() - me.opponent.Hero.GetHP())/5.0; 

            foreach (var unit in me.GetWholeBoard())
            {
                toReturn += EvaluateUnit(unit);
            }

            foreach (var unit in me.opponent.GetWholeBoard())
            {
                toReturn -= EvaluateUnit(unit);
            }

            if (toReturn < 0)
                throw new Exception("Error evalute value got below 0, this will mess up the sorting algorithm of the value! Increase it to something higher as default to fix");
            
            return toReturn;
        }

        private double EvaluateUnit(ICard unit)
        {
            return unit.GetCost();
        }
    }
}