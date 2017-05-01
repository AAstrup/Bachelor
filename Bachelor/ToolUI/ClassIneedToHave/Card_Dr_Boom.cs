using Bachelor;

namespace GameEngine
{
    public class Card_Dr_Boom : CardTracker, ICard, ITrackable, ITarget
    {
        public Card_Dr_Boom() : base() { }
        public Card_Dr_Boom(Deck deck,ICard template,ITrackable templateTrack, bool track ) : base(deck,template, templateTrack, track) { }
        public string rarity = "common";
        public int hp = 7;
        public int cost = 1;
        public int attack = 8;
        public string name = "Card_Dr_Boom";

        public void setCost(int newCost) { cost = newCost; }

        public void setAttack(int newAttack) { attack = newAttack; }

        public void setHealth(int newHealth) { hp = newHealth; }

        public void setName(string newName) { name = newName; }

        public void setRarity(string newRarity) { rarity = newRarity; }

        public override string GetNameType()
        {
            return name;
        }

        public string GetRarity()
        {
            return rarity;
        }

        public override int GetDamage()
        {
            return attack;
        }

        public int GetMaxHp()
        {
            return hp;
        }

        public override int GetCost()
        {
            return cost;
        }

        public override ICard InstantiateModel(Deck deck,BoardState board, PlayerBoardState player, bool track = true)
        {
            var toReturn = new Card_Dr_Boom(deck,this, this,track);
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}