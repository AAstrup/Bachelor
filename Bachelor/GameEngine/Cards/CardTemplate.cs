using GameEngine.Printers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public abstract class CardTemplate : ICard, ITarget
    {
        bool canAttack;
        protected PlayerBoardState player;
        protected BoardState board;
        private int hpLeft;

        public CardTemplate()
        {
            hpLeft = GetMaxHp();
        }

        protected virtual int GetMaxHp()
        {
            return 1;
        }

        public List<ITarget> GetAttackTargetOptions(BoardState board)
        {
            return player.opponent.GetValidAttackVictims().Cast<ITarget>().ToList();
        }

        public void PlayCard()
        {
            player.GetWholeBoard().Add(this);
            if(HasTaunt())
                player.GetTauntBoard().Add(this);
            player.GetWholeHand().Remove(this);
        }

        protected virtual bool HasTaunt()
        {
            return false;
        }

        protected void Die()
        {
            //Console.WriteLine(player.playerSetup.name + " minion died of type " + GetNameType());
            player.GetWholeBoard().Remove(this);
        }

        public void Attack(ITarget target)
        {
            canAttack = false;
            target.Damage(GetDamage()," minion damage from " + GetNameType() + " of player " + player.playerSetup.name);
            if(target.GetDamage() > 0)
                Damage(target.GetDamage(), " minion damage from " + target.GetNameType() + " of player " + target.GetOwner().playerSetup.name);
            CheckForDeath();
            target.CheckForDeath();
        }

        public bool BattlecryRequiresTarget()
        {
            return false;
        }

        public bool CanAttack()
        {
            return canAttack;
        }

        public bool CanBePlayed(BoardState board, PlayerBoardState player)
        {
            return true;
        }

        public List<ICard> GetBattlecryTargets()
        {
            throw new NotSupportedException("Does not have a battlecry");
        }

        public virtual int GetCost()
        {
            return 1;
        }

        public void SetBattlecryTarget(ICard card)
        {
            throw new NotSupportedException("BASECard does not support battlecry target");
        }

        public void Damage(int dmg,string logDescription)
        {
            Singletons.GetPrinter().CardAttackTrade(player.playerSetup, this, GetHpLeft(), dmg);
            SetHpLeft(GetHpLeft() - dmg);
            CheckForDeath();
        }

        private void SetHpLeft(int v)
        {
            hpLeft = v;
        }

        private int GetHpLeft()
        {
            return hpLeft;
        }

        public virtual int GetDamage()
        {
            return 0;
        }

        public void NewRound()
        {
            canAttack = true;
        }

        public void CheckForDeath()
        {
            if (GetHpLeft() <= 0)
                Die();
        }

        public PlayerBoardState GetOwner()
        {
            return player;
        }

        public virtual string GetNameType()
        {
            return "Unnamed minion";
        }

        public virtual ICard InstantiateModel(BoardState board, PlayerBoardState player)
        {
            throw new NotSupportedException("Override copy if inheriting from CardTemplate, this template is not suppose to be copied");
        }

        public virtual void Win()
        {
            throw new NotSupportedException("Inherit from CardTracker indsteed of heriting directly from CardTemplate");
        }

        public virtual void Loss()
        {
            throw new NotSupportedException("Inherit from CardTracker indsteed of heriting directly from CardTemplate");
        }
    }
}