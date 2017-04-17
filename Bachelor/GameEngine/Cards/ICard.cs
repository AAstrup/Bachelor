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

        /// <summary>
        /// Used to create the element in the actual game. This instantiates it with a player, board and health.
        /// Each card has to implement this! Card template will crash if not done.
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="board"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        ICard InstantiateModel(Deck deck,BoardState board, PlayerBoardState player);

        /// <summary>
        /// Used to track stats. These stats are kept on the template holding this info.
        /// </summary>
        void Win();

        /// <summary>
        /// Used to track stats. These stats are kept on the template holding this info.
        /// </summary>
        void Loss();
    }
}
