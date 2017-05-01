using Bachelor;
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
        int GetMaxHp();
        string GetRarity();

        void setName(string newName);
        void setRarity(string newRarity);
        void setCost(int newCost);
        void setAttack(int newAttack);
        void setHealth(int newHealth);

        /// <summary>
        /// Used to create the element in the actual game. This instantiates it with a player, board and health.
        /// Each card has to implement this! Card template will crash if not done.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="board"></param>
        /// <param name="player"></param>
        /// <param name="track">Used by AI to figure out the move without putting the move into records or stats</param>
        /// <returns></returns>
        ICard InstantiateModel(Deck deck,BoardState board, PlayerBoardState player, bool track = true);

        /// <summary>
        /// Used to track stats. These stats are kept on the template holding this info.
        /// </summary>
        void Win();

        /// <summary>
        /// Used to track stats. These stats are kept on the template holding this info.
        /// </summary>
        void Loss();
        ICard Copy(Deck deck,BoardState board, PlayerBoardState player);
        bool HasTaunt();
        void SetDamage(int v);
        void SetHasTaunt(bool v);
        void SetHP(int hpLeft);
        void SetAttack(bool v);
    }
}
