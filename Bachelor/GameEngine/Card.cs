using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public interface ICard
    {
        bool CanAttack();
        void Attack(ITarget target);
        bool CanBePlayed(BoardState board,PlayerBoardState player);
        void Damage(int dmg,string logDescription);
        int GetDamage();
        List<ITarget> GetAttackTargetOptions(BoardState board);
        int GetCost();
        void PlayCard();
        bool BattlecryRequiresTarget();
        List<ICard> GetBattlecryTargets();
        void SetBattlecryTarget(ICard card);
        void NewRound();
        PlayerBoardState GetOwner();
        string GetNameType();

    }
}
