using System;
using System.Collections.Generic;

namespace GameEngine.Printers
{
    public class DetailPrinter : IPrinter
    {
        public void AddEmptySpaces(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                //Console.WriteLine("");
            }
        }

        public void AI_AttackCard(ICard actionCard, ITarget target)
        {
            ////Console.WriteLine("--- AI Attack DECISION ---");
            //return actionCard.GetNameType() + " -> " + target.GetNameType();
            //Console.WriteLine("ATTTACK " +actionCard.GetNameType() + " -> " + target.GetNameType());
        }

        public void AI_PlayCard(PlayerSetup playerSetup, ICard actionCard, int hp, int dmg)
        {
            ////Console.WriteLine("--- AI Play DECISION ---");
            //return cardTemplate.GetNameType() + "[" + playerSetup.name + "](" + hp + " -> " + (hp - dmg) + ")";
            //Console.WriteLine("PLAY " +actionCard.GetNameType() + "[" + playerSetup.name + "](" + hp + " -> " + (hp - dmg) + ")");
        }

        public void AttackCard(ICard actionCard, ITarget target)
        {
            //Console.WriteLine(actionCard.GetNameType() + " -> " + target.GetNameType());
        }

        public void CardAttackTrade(PlayerSetup playerSetup, CardTemplate cardTemplate, int hp, int dmg)
        {
            //Console.WriteLine(cardTemplate.GetNameType() + "[" + playerSetup.name + "](" + hp + " -> " + (hp - dmg) + ")");
        }

        public void GameOver()
        {
            //Console.WriteLine("GAME OVER");
        }

        public void HeroDamaged(PlayerSetup playerSetup, int hp, int dmg, string damageReason)
        {
            //Console.WriteLine("Hero [" + playerSetup.name + "] hp (" + hp + " -> " + (hp - dmg)+")" );
        }

        public void PlayCard(PlayerSetup playerSetup, ICard actionCard, int currentMana, int cost)
        {
            //Console.WriteLine("Plays " + actionCard.GetNameType() + " mana (" + currentMana + " -> " + (currentMana-cost) + ")");
        }

        public void PlayerTurn(string name)
        {
            //Console.WriteLine("");
            //Console.WriteLine("["+name+"] turn");
        }

        public void StartCards(PlayerSetup playerSetup, int startCards, bool isGoingFirst, List<ICard> hand)
        {
            string cmd = playerSetup.name + " drawing  " + startCards + " cards [";
            for (int i = 0; i < hand.Count; i++)
            {
                if (i > 0)
                    cmd += ", ";
                cmd += hand[i].GetNameType();
            }
            cmd += "´]";
            //Console.WriteLine(cmd);
        }
    }
}