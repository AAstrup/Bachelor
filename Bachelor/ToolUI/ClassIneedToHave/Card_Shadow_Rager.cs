using Bachelor;

namespace GameEngine
{
    public class Card_Shadow_Rager : CardTracker, ICard, ITrackable, ITarget
    {
        public Card_Shadow_Rager() : base() { }
        public Card_Shadow_Rager(Deck deck,ICard template,ITrackable templateTrack, bool track) : base(deck,template, templateTrack,track) { }
        public string rarity = "common";
        public int hp = 1;
        public int cost = 5;
        public int attack = 1;
        public string name = "Card_Shadow_Rager";

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
            var toReturn = new Card_Shadow_Rager(deck,this, this, track);
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}