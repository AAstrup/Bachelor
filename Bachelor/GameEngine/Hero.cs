using GameEngine.Printers;
using System;

namespace GameEngine
{

    public class Hero : ITarget
    {
        private BoardState board;
        int hp = 30;
        private PlayerBoardState player;

        public Hero(BoardState board, PlayerBoardState player)
        {
            this.board = board;
            this.player = player;
        }
        public void CheckForDeath()
        {
            if (hp <= 0)
            {
                Singletons.GetPrinter().GameOver();
                board.FinishGame(player);
            }
        }

        public void Damage(int dmg,string damageReason)
        {
            Singletons.GetPrinter().HeroDamaged(player.playerSetup, hp,dmg,damageReason);
            hp -= dmg;
        }

        public int GetDamage()
        {
            return 0;
        }

        public string GetNameType()
        {
            return "Hero";
        }

        public PlayerBoardState GetOwner()
        {
            return player;
        }

        public Hero Copy(BoardState board,PlayerBoardState playerState)
        {
            var toReturn = new Hero(board, player);
            toReturn.hp = hp;
            return toReturn;
        }
    }
}