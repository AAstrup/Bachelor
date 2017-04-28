using Bachelor;
using GameEngine;
using GameEngine.Printers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AI
{
    public class AI_Random : AI_Default, IAI
    {
        private Random random;
        public AI_Random()
        {
            random = new Random();
        }

        public void TakeTurn(BoardState board,playerNr playerNr)
        {
            this.playerNr = playerNr;
            GetOriginalPlayer(board).NewTurn(board);
            while(GetOriginalPlayer(board).GetValidBoardOptions().Count > 0 || GetOriginalPlayer(board).GetValidHandOptions().Count > 0)
            {
                MakeDecision(board);
                if (board.isFinished)
                {
                    return;
                }
            }
        }

        private void MakeDecision(BoardState state)
        {
            if(GetOriginalPlayer(state).GetValidBoardOptions().Count > 0 && GetOriginalPlayer(state).GetValidHandOptions().Count > 0)
            {
                int randIndex = random.Next(0, 2);
                if (randIndex == 0)
                    MakeDecision_CardPool_Board(state);
                else
                    MakeDecision_CardPool_Hand(state);
            }
            else if(GetOriginalPlayer(state).GetValidBoardOptions().Count > 0)
                MakeDecision_CardPool_Board(state);
            else if(GetOriginalPlayer(state).GetValidHandOptions().Count > 0)
                MakeDecision_CardPool_Hand(state);
        }

        private void MakeDecision_CardPool_Hand(BoardState state)
        {
            List<ICard> options = GetOriginalPlayer(state).GetValidHandOptions();
            ICard actionCard = options[random.Next(0, options.Count)];

            if (actionCard.BattlecryRequiresTarget())
            {
                List<ICard> possibleTargets = actionCard.GetBattlecryTargets();
                actionCard.SetBattlecryTarget(possibleTargets[random.Next(0,possibleTargets.Count)]);
            }

            Singletons.GetPrinter().PlayCard(GetOriginalPlayer(state).playerSetup, actionCard, GetOriginalPlayer(state).GetManaLeft(), actionCard.GetCost());
            GetOriginalPlayer(state).SpendMana(actionCard);

            actionCard.PlayCard();
        }
        
        private void MakeDecision_CardPool_Board(BoardState state)
        {
            List<ICard> options = GetOriginalPlayer(state).GetValidBoardOptions();
            ICard actionCard = options[random.Next(0, options.Count)];

            List<ITarget> targetOptions = actionCard.GetAttackTargetOptions(state);
            ITarget target = targetOptions[random.Next(0, targetOptions.Count)];

            Singletons.GetPrinter().AttackCard(actionCard,target);


            if (target.GetOwner() == actionCard.GetOwner())
                throw new Exception("ATTACKING MY OWN UNITS!?");

            actionCard.Attack(target);
        }
    }
}
