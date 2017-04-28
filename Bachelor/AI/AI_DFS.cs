using System;
using AI;
using GameEngine;
using System.Collections.Generic;
using GameEngine.Printers;

namespace Bachelor
{
    public class AI_DFS : AI_Default, IAI
    {
        StateEvaluator evalutator = new StateEvaluator();
        public void TakeTurn(BoardState board,playerNr playerNr)
        {
            this.playerNr = playerNr;
            GetOriginalPlayer(board).NewTurn(board);
            BoardState newBoard = MakeDecisionOnNewBoard(board, GetOriginalPlayer(board));
            board.Update(newBoard);
        }
        
        private BoardState MakeDecisionOnNewBoard(BoardState state, PlayerBoardState playerState)
        {
            AI_DFS_Decision decision = new AI_DFS_Decision(state, GetBoardStateAsValue(state));
            decision = MakeDecision(decision);
            return decision.GetBoard();
        }

        private AI_DFS_Decision MakeDecision(AI_DFS_Decision decision)
        {
            PlayerBoardState playerBoardState = decision.GetBoard().GetPlayer(playerNr);
            if (playerBoardState.GetValidBoardOptions().Count > 0 && playerBoardState.GetValidHandOptions().Count > 0)
                return decision;

            if (playerBoardState.GetValidHandOptions().Count > 0) {
                var newDecision = MakeDecision_Using_Hand(decision);
                if (newDecision.GetValue() > decision.GetValue())
                    decision = newDecision;
            }

            if (playerBoardState.GetValidBoardOptions().Count > 0)
            {
                var newDecision = MakeDecision_Using_Board(decision);
                if (newDecision.GetValue() > decision.GetValue())
                    decision = newDecision;
            }
            return decision;
        }

        private double GetBoardStateAsValue(BoardState state)
        {
            return evalutator.Evaluate(state, playerNr);
        }

        private AI_DFS_Decision MakeDecision_Using_Hand(AI_DFS_Decision previousState)
        {
            AI_DFS_Decision bestDecisiion = previousState;
            for (int i = 0; i < previousState.GetBoard().GetPlayer(playerNr).GetValidHandOptions().Count; i++)
            {
                BoardState newBoard = previousState.GetBoard().Copy();
                PlayerBoardState newPlayerState = newBoard.GetPlayer(playerNr);
                List<ICard> options = newPlayerState.GetValidHandOptions();

                ICard actionCard = options[i];

                Singletons.GetPrinter().AI_PlayCard(newPlayerState.playerSetup, actionCard, newPlayerState.GetManaLeft(), actionCard.GetCost());
                newPlayerState.SpendMana(actionCard);

                actionCard.PlayCard();
                AI_DFS_Decision exhaustedDecision = MakeDecision(new AI_DFS_Decision(newBoard, GetBoardStateAsValue(newBoard)));

                if (bestDecisiion.GetValue() < exhaustedDecision.GetValue())
                    bestDecisiion = exhaustedDecision;
            }

            return bestDecisiion;
                //BATTLECRY NOT IMPLEMENTED YET!
            //if (actionCard.BattlecryRequiresTarget())
            //{
            //    List<ICard> possibleTargets = actionCard.GetBattlecryTargets();
            //    actionCard.SetBattlecryTarget(possibleTargets[random.Next(0, possibleTargets.Count)]);
            //}

        }

        private AI_DFS_Decision MakeDecision_Using_Board(AI_DFS_Decision previousState)
        {
            AI_DFS_Decision bestDecision = previousState;
            for (int i = 0; i < previousState.GetPlayerState(playerNr).GetValidBoardOptions().Count; i++)
            {
                //No need for duplicating the board yet, as we are only getting possible targets
                List<ICard> originalOptions = previousState.GetPlayerState(playerNr).GetValidBoardOptions();
                List<ITarget> originalTargetOptions = originalOptions[i].GetAttackTargetOptions(previousState.GetBoard());
                for (int y = 0; y < originalTargetOptions.Count; y++)
                {
                    BoardState newBoard = previousState.GetBoard().Copy();
                    PlayerBoardState newPlayerState = newBoard.GetPlayer(playerNr);
                    ICard actionCard = newPlayerState.GetValidBoardOptions()[i];
                    ITarget target = actionCard.GetAttackTargetOptions(newBoard)[y];

                    Singletons.GetPrinter().AI_AttackCard(actionCard, target);

                    if (target.GetOwner() == actionCard.GetOwner())
                        throw new Exception("ATTACKING MY OWN UNITS!?");

                    actionCard.Attack(target);
                    AI_DFS_Decision exhaustedDecision = MakeDecision(new AI_DFS_Decision(newBoard, GetBoardStateAsValue(newBoard)));

                    if (bestDecision.GetValue() < exhaustedDecision.GetValue())
                        bestDecision = exhaustedDecision;
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
    }
}