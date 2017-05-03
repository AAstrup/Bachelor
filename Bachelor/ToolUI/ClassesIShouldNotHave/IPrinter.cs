using System.Collections.Generic;

namespace GameEngine.Printers
{
    public interface IPrinter
    {
        void AddEmptySpaces(int v);
        void PlayerTurn(string name);
        void PlayCard(PlayerSetup playerSetup, ICard actionCard, int currentMana, int cost);
        void AttackCard(ICard actionCard, ITarget target);
        void CardAttackTrade(PlayerSetup playerSetup, CardTemplate cardTemplate, int hp, int dmg);
        void HeroDamaged(PlayerSetup playerSetup, int hp, int dmg, string damageReason);
        void GameOver();
        void StartCards(PlayerSetup playerSetup, int startCards,bool isGoingFirst, List<ICard> hand);
        void AI_PlayCard(PlayerSetup playerSetup, ICard actionCard, int v1, int v2);
        void AI_AttackCard(ICard actionCard, ITarget target);
    }
}