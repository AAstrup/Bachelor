using Bachelor;

namespace GameEngine
{
    public class Card_Yeti : CardTracker, ICard, ITarget
    {
        public Card_Yeti() : base() { }
        public Card_Yeti(Deck deck, ICard template, ITrackable templateTrack, bool track) : base(deck,template, templateTrack,track) { }
        public string rarity = "common";
        public int hp = 5;
        public int cost = 4;
        public int attack = 4;

        public string name = "Card_Yeti";

        public void setName(string newName) { name = newName; }

        public void setCost(int newCost) { cost = newCost; }

        public void setAttack(int newAttack) { attack = newAttack; }

        public void setHealth(int newHealth) { hp = newHealth; }

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

        public override ICard InstantiateModel(Deck deck, BoardState board, PlayerBoardState player, bool track = true)
        {
            var toReturn = new Card_Yeti(deck,this,this,track);
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}