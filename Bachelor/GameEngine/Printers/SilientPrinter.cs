using System;

namespace GameEngine.Printers
{
    internal class SilientPrinter : IPrinter
    {
        public void AddEmptySpaces(int v)
        {
        }

        public void AttackCard(ICard actionCard, ITarget target)
        {
        }

        public void CardAttackTrade(PlayerSetup playerSetup, CardTemplate cardTemplate, int hp, int dmg)
        {
        }

        public void GameOver()
        {

        }

        public void HeroDamaged(PlayerSetup playerSetup, int hp, int dmg, string damageReason)
        {
        }

        public void PlayCard(PlayerSetup playerSetup, ICard actionCard, int currentMana, int cost)
        {
        }

        public void PlayerTurn(string name)
        {
        }

        public void StartCards(PlayerSetup playerSetup, int startCards, bool isGoingFirst)
        {
        }
    }
}