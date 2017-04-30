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
            if (me.opponent.Hero.GetHPLeft() <= 0)
                return Double.MaxValue;
            toReturn += (me.Hero.GetHPLeft() - me.opponent.Hero.GetHPLeft())/5.0; 

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
            return (unit.GetDamage() + unit.GetMaxHp())/2.0;
            //return unit.GetCost();
        }

        internal double CardPlayOnBoard(ICard actionCard, PlayerBoardState playerState, BoardState boardState)
        {
            return EvaluateUnit(actionCard);
        }

        internal double TradeOnBoard(ICard actionCard, ITarget target, PlayerBoardState playerState, BoardState boardState)
        {
            double val = 0.0;
            if (actionCard.GetDamage() >= target.GetHPLeft())
                val += ((ICard)target).GetCost();
            if (target.GetDamage() >= actionCard.GetHPLeft())
                val -= actionCard.GetCost();
            return val;
        }

        internal double FaceAttackOnBoard(ICard actionCard, Hero enemyHero, PlayerBoardState playerState, BoardState boardState)
        {
            if (actionCard.GetDamage() >= enemyHero.GetHPLeft())
                return Double.MaxValue;
            else
            {
                if(playerState.Hero.GetHPLeft() < enemyHero.GetHPLeft())
                    return actionCard.GetDamage() / 4.0;
                else
                    return actionCard.GetDamage();
            }
        }
    }
}