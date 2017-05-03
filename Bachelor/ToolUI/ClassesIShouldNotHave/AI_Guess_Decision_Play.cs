using System;
using GameEngine;
using System.Collections.Generic;
using GameEngine.Printers;

namespace Bachelor
{
    internal class AI_Guess_Decision_Play : IAI_Guess_Decision
    {
        private ICard actionCard;
        private Random random;//Used to set battlecrys

        public AI_Guess_Decision_Play(ICard actionCard)
        {
            random = new Random();
            this.actionCard = actionCard;
        }

        //IAI_Guess_DecisionType
        public void Play(BoardState board, PlayerBoardState playerState)
        {

            if (actionCard.BattlecryRequiresTarget())
            {
                List<ICard> possibleTargets = actionCard.GetBattlecryTargets();
                actionCard.SetBattlecryTarget(possibleTargets[random.Next(0, possibleTargets.Count)]);
            }

            Singletons.GetPrinter().PlayCard(playerState.playerSetup, actionCard, playerState.GetManaLeft(), actionCard.GetCost());
            playerState.SpendMana(actionCard);
            actionCard.PlayCard();

            board.statisticResult.GetCardPlaySequence(playerState.GetPlayerNr()).Add(((ITrackable)actionCard).GetTemplate());
        }
    }
}