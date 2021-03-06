﻿using System;
using AI;
using GameEngine;
using System.Collections.Generic;
using GameEngine.Printers;
namespace Bachelor
{
    public class AI_Guess: AI_Default, IAI
    {
        StateEvaluator evalutator = new StateEvaluator();
        public void TakeTurn(BoardState board, playerNr playerNr)
        {
            this.playerNr = playerNr;
            var playerState = GetPlayer(board);
            playerState.NewTurn(board);
            if (playerState.Hero.IsDead())
            {
                board.FinishGame(playerState);
                return;
            }
            MakeDecisionsOnBoard(board, GetPlayer(board));
            TrackDominance(board.GetPlayer(playerNr));
        }

        private void TrackDominance(PlayerBoardState state)
        {
            foreach (var card in state.GetWholeHand())
            {
                if (card.GetCost() <= state.GetMaxMana())
                    ((ITrackable)card).GetTemplateAsTrackable().DecreaseTemplateDominance(card);
            }
        }

        private void MakeDecisionsOnBoard(BoardState state, PlayerBoardState playerState)
        {
            AI_Guess_Decision decision = new AI_Guess_Decision(state, GetBoardStateAsValue(state));
            while (playerState.GetValidHandOptions().Count > 0 || playerState.GetValidBoardOptions().Count > 0)
            {
                decision = MakeDecision(decision);
                decision.Execute(playerState);
                if (playerState.opponent.Hero.IsDead())
                {
                    state.FinishGame(playerState.opponent);
                    return;
                }
            }
        }

        private AI_Guess_Decision MakeDecision(AI_Guess_Decision decision)
        {
            PlayerBoardState playerBoardState = decision.GetBoard().GetPlayer(playerNr);

            if (playerBoardState.GetValidHandOptions().Count > 0)
            {
                var newDecision = MakeDecision_Using_Hand(decision);
                if (newDecision.GetBoardValue() >= decision.GetBoardValue())
                    decision = newDecision;
            }

            if (playerBoardState.GetValidBoardOptions().Count > 0)
            {
                var newDecision = MakeDecision_Using_Board(decision);
                if (newDecision.GetBoardValue() >= decision.GetBoardValue())
                    decision = newDecision;
            }
            return decision;
        }

        private double GetBoardStateAsValue(BoardState state)
        {
            return evalutator.Evaluate(state, playerNr);
        }

        private AI_Guess_Decision MakeDecision_Using_Hand(AI_Guess_Decision previousState)
        {
            AI_Guess_Decision bestDecision = new AI_Guess_Decision(previousState.GetBoard(),previousState.GetBoardValue());
            PlayerBoardState playerState = previousState.GetBoard().GetPlayer(playerNr);
            List<ICard> options = playerState.GetValidHandOptions();

            for (int i = 0; i < options.Count; i++)
            {
                double val = evalutator.CardPlayOnBoard(options[i],playerState,previousState.GetBoard());
                if(bestDecision.GetBoardValue() < (previousState.GetBoardValue() + val))
                {
                    bestDecision.SetBoardValue(previousState.GetBoardValue() + val);
                    bestDecision.SetDecision(new AI_Guess_Decision_Play(options[i]));
                }
            }
            return bestDecision;
        }

        private AI_Guess_Decision MakeDecision_Using_Board(AI_Guess_Decision previousState)
        {
            AI_Guess_Decision bestDecision = new AI_Guess_Decision(previousState.GetBoard(),previousState.GetBoardValue());
            PlayerBoardState playerState = previousState.GetBoard().GetPlayer(playerNr);
            List<ICard> myUnitOptions = previousState.GetPlayerState(playerNr).GetValidBoardOptions();
            for (int i = 0; i < previousState.GetPlayerState(playerNr).GetValidBoardOptions().Count; i++)
            {
                ICard actionCard = playerState.GetValidBoardOptions()[i];
                if (playerState.opponent.GetTauntBoard().Count > 0 || IsDyingFirst(playerState)) //Trade
                {
                    List<ITarget> originalTargetOptions = actionCard.GetAttackTargetOptions(previousState.GetBoard());
                    for (int y = 0; y < originalTargetOptions.Count; y++)
                    {
                        ITarget target = originalTargetOptions[y];

                        if (target == playerState.opponent.Hero)
                            continue;
                        if (target.GetOwner() == actionCard.GetOwner())
                            throw new Exception("ATTACKING MY OWN UNITS!?");

                        double val = evalutator.TradeOnBoard(actionCard, target, playerState, previousState.GetBoard());
                        if (bestDecision.GetBoardValue() < (previousState.GetBoardValue() + val))
                        {
                            bestDecision.SetBoardValue(previousState.GetBoardValue() + val);
                            bestDecision.SetDecision(new AI_Guess_Decision_Trade(actionCard, target));
                        }
                    }
                }
                else //Face
                {
                    var val = evalutator.FaceAttackOnBoard(actionCard, playerState.opponent.Hero, playerState, previousState.GetBoard());
                    if (bestDecision.GetBoardValue() < (previousState.GetBoardValue() + val))
                    {
                        bestDecision.SetBoardValue(previousState.GetBoardValue() + val);
                        bestDecision.SetDecision(new AI_Guess_Decision_Face(actionCard));
                        return bestDecision;
                    }
                }
            }
            return bestDecision;

            //List<ICard> options = playerState.GetValidBoardOptions();
            //ICard actionCard = options[random.Next(0, options.Count)];

            //List<ITarget> targetOptions = actionCard.GetAttackTargetOptions(state);
            //ITarget target = targetOptions[random.Next(0, targetOptions.Count)];

            //Singletons.GetPrinter().AttackCard(actionCard, target);


            //if (target.GetOwner() == actionCard.GetOwner())
            //    throw new Exception("ATTACKING MY OWN UNITS!?");

            //actionCard.Attack(target);
        }

        private bool IsDyingFirst(PlayerBoardState playerState)
        {
            return false;
            double myDmgThisTurn = 0.0;
            double myDmg = 0.0;
            foreach (var item in playerState.GetWholeBoard())
            {
                if (item.CanAttack())
                    myDmgThisTurn += item.GetDamage();
                myDmg += item.GetDamage();
            }

            double oppDmg = 0.0;
            foreach (var item in playerState.opponent.GetWholeBoard())
            {
                oppDmg += item.GetDamage();
            }

            if (myDmgThisTurn >= playerState.opponent.Hero.GetHPLeft())
                return false;
            if (oppDmg >= playerState.Hero.GetHPLeft())
                return true;
            return playerState.Hero.GetHPLeft() / oppDmg > (playerState.opponent.Hero.GetHPLeft() - myDmgThisTurn) / myDmg;
            //      10                                 / 5 =2   >  (10                                   - 1 ) / 5= 2
        }
    }
}