using AI;
using GameEngine;
using GameEngine.Printers;
using System;
using System.Collections.Generic;

namespace Bachelor
{
    public class AI_Dijkstra : AI_Default, IAI
    {
        StateEvaluator evalutator = new StateEvaluator();
        List<AI_DFS_Decision> possibleDecisions;
        List<double> decisionsValues;
        public void TakeTurn(BoardState board, playerNr playerNr)
        {
            possibleDecisions = new List<AI_DFS_Decision>();
            decisionsValues = new List<double>();
            this.playerNr = playerNr;
            board.GetPlayer(playerNr).NewTurn(board);
            if (board.isFinished)
                return;
            BoardState newBoard = MakeDecisionOnNewBoard(board, GetPlayer(board));
            board.Update(newBoard);
        }

        private BoardState MakeDecisionOnNewBoard(BoardState board, PlayerBoardState playerState)
        {
            possibleDecisions.Add( new AI_DFS_Decision(board, GetBoardStateAsValue(board)));
            decisionsValues.Add(possibleDecisions[possibleDecisions.Count - 1].GetValue());
            ComputeDecisions();
            return possibleDecisions[possibleDecisions.Count -1].GetBoard();
        }

        private void ComputeDecisions()
        {
            var decision = possibleDecisions[possibleDecisions.Count - 1];
            PlayerBoardState playerBoardState = possibleDecisions[0].GetBoard().GetPlayer(playerNr);
            while (playerBoardState.GetValidBoardOptions().Count > 0 || playerBoardState.GetValidHandOptions().Count > 0)
            {
                if (playerBoardState.GetValidHandOptions().Count > 0)
                {
                    ComputeDecision_Using_Hand(decision);
                }

                if (playerBoardState.GetValidBoardOptions().Count > 0)
                {
                    ComputeDecision_Using_Board(decision);
                }

                decision = possibleDecisions[0];
                playerBoardState = possibleDecisions[0].GetBoard().GetPlayer(playerNr);
                possibleDecisions.RemoveAt(0);
                decisionsValues.RemoveAt(0);
            }
        }

        private double GetBoardStateAsValue(BoardState state)
        {
            return evalutator.Evaluate(state, playerNr);
        }

        private void ComputeDecision_Using_Hand(AI_DFS_Decision previousState)
        {
            for (int i = 0; i < previousState.GetBoard().GetPlayer(playerNr).GetValidHandOptions().Count; i++)
            {
                BoardState newBoard = previousState.GetBoard().Copy();
                PlayerBoardState newPlayerState = newBoard.GetPlayer(playerNr);
                List<ICard> options = newPlayerState.GetValidHandOptions();

                ICard actionCard = options[i];

                Singletons.GetPrinter().AI_PlayCard(newPlayerState.playerSetup, actionCard, newPlayerState.GetManaLeft(), actionCard.GetCost());
                newPlayerState.SpendMana(actionCard);

                actionCard.PlayCard();
                Insert(new AI_DFS_Decision(newBoard, GetBoardStateAsValue(newBoard)));
            }

            //BATTLECRY NOT IMPLEMENTED YET!
            //if (actionCard.BattlecryRequiresTarget())
            //{
            //    List<ICard> possibleTargets = actionCard.GetBattlecryTargets();
            //    actionCard.SetBattlecryTarget(possibleTargets[random.Next(0, possibleTargets.Count)]);
            //}

        }

        private void Insert(AI_DFS_Decision decision)
        {
            int index = decisionsValues.BinarySearch(decision.GetValue());
            if(index < 0)
                index = ~index;
            possibleDecisions.Insert(index, decision);
            decisionsValues.Insert(index, decision.GetValue());
        }

        private void ComputeDecision_Using_Board(AI_DFS_Decision previousState)
        {
            AI_DFS_Decision currentDecision = previousState;
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
                    Insert(new AI_DFS_Decision(newBoard, GetBoardStateAsValue(newBoard)));
                }
            }

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