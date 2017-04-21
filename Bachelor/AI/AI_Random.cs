using GameEngine;
using GameEngine.Printers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AI
{
    public class AI_Random : IAI
    {
        PlayerBoardState playerState;
        private Random random;
        private PlayerBoardState playerBoardState;

        public AI_Random()
        {
            random = new Random();
        }

        public void TakeTurn(BoardState board,PlayerBoardState playerState)
        {
            this.playerState = playerState;
            playerState.NewTurn();
            while(playerState.GetValidBoardOptions().Count > 0 || playerState.GetValidHandOptions().Count > 0)
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
            if(playerState.GetValidBoardOptions().Count > 0 && playerState.GetValidHandOptions().Count > 0)
            {
                int randIndex = random.Next(0, 2);
                if (randIndex == 0)
                    MakeDecision_CardPool_Board(state);
                else
                    MakeDecision_CardPool_Hand(state);
            }
            else if(playerState.GetValidBoardOptions().Count > 0)
                MakeDecision_CardPool_Board(state);
            else if(playerState.GetValidHandOptions().Count > 0)
                MakeDecision_CardPool_Hand(state);
        }

        private void MakeDecision_CardPool_Hand(BoardState state)
        {
            List<ICard> options = playerState.GetValidHandOptions();
            ICard actionCard = options[random.Next(0, options.Count)];

            if (actionCard.BattlecryRequiresTarget())
            {
                List<ICard> possibleTargets = actionCard.GetBattlecryTargets();
                actionCard.SetBattlecryTarget(possibleTargets[random.Next(0,possibleTargets.Count)]);
            }

            Singletons.GetPrinter().PlayCard(playerState.playerSetup, actionCard, playerBoardState.GetManaLeft(), actionCard.GetCost());
            playerState.SpendMana(actionCard);

            actionCard.PlayCard();
        }
        
        private void MakeDecision_CardPool_Board(BoardState state)
        {
            List<ICard> options = playerState.GetValidBoardOptions();
            ICard actionCard = options[random.Next(0, options.Count)];

            List<ITarget> targetOptions = actionCard.GetAttackTargetOptions(state);
            ITarget target = targetOptions[random.Next(0, targetOptions.Count)];

            Singletons.GetPrinter().AttackCard(actionCard,target);


            if (target.GetOwner() == actionCard.GetOwner())
                throw new Exception("ATTACKING MY OWN UNITS!?");

            actionCard.Attack(target);
        }

        public PlayerBoardState GetPlayer()
        {
            return playerBoardState;
        }

        public void SetPlayer(PlayerBoardState playerBoardState)
        {
            this.playerBoardState = playerBoardState;
        }
    }
}
