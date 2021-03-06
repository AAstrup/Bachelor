﻿using Bachelor;
using GameEngine.Printers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public abstract class CardTemplate : ICard, ITarget
    {
        protected bool hasCharge;//Used to read whether or not the card had charge when it was played. Charge minions has a default value of canAttack set to true.
        protected bool canAttack;
        protected int cost;
        protected PlayerBoardState player;
        protected BoardState board;
        protected int hpLeft;
        protected int totalHP = 1;
        protected int damage = 1;
        protected bool hasTaunt;
        private string unique;
        private string rarity;
        private string name;

        public CardTemplate()
        {
            hpLeft = GetMaxHp();
        }

        public virtual int GetMaxHp()
        {
            return totalHP;
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

        public virtual bool HasTaunt()
        {
            return hasTaunt;
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
            return cost;
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
            return damage;
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

        /// <summary>
        /// Creates a card from a cardpool into a game, without changing the template.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="board"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public virtual ICard InstantiateModel(Deck deck, BoardState board, PlayerBoardState player)
        {
            throw new NotSupportedException("Override InstantiateModel if inheriting from CardTemplate, this template is not suppose to be instantiated as Model");
        }

        public virtual ICard Copy(Deck deck,BoardState boardState,PlayerBoardState playerBoardState)
        {
            var card = InstantiateModel(deck, board, playerBoardState);
            card.SetDamage(GetDamage());
            card.SetHasTaunt(HasTaunt());
            card.SetHP(hpLeft);
            card.SetAttack(CanAttack());
            return card;
        }

        public void SetDamage(int v)
        {
            damage = v;
        }

        public void SetHasTaunt(bool v)
        {
            hasTaunt = v;
        }

        public void SetHP(int hpLeft)
        {
            this.hpLeft = hpLeft;
        }

        public void SetAttack(bool v)
        {
            canAttack = v;
        }

        public void DEBUG_Tracetag(string unique)
        {
            this.unique = unique;
        }

        public int GetHPLeft()
        {
            return hpLeft;
        }

        public bool HasCharge()
        {
            return hasCharge;
        }

        public string GetRarity()
        {
            return rarity;
        }

        public void setName(string newName)
        {
            this.name = newName; 
        }

        public void setRarity(string newRarity)
        {
            rarity = newRarity;
        }

        public void setCost(int newCost)
        {
            cost = newCost;
        }

        public void setAttack(int newAttack)
        {
            damage = newAttack;
        }

        public void setHealth(int newHealth)
        {
            totalHP = newHealth;
            hpLeft = totalHP;
        }
    }
}