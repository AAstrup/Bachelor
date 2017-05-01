using Bachelor;

namespace GameEngine
{
    public class Card_Earthen_Ring_Farseer: CardTracker, ICard, ITrackable, ITarget
    {
        public Card_Earthen_Ring_Farseer() : base() { }
        public Card_Earthen_Ring_Farseer(Deck deck,ICard template,ITrackable templateTrack, bool track) : base(deck,template, templateTrack, track ) { }
        public string rarity = "common";
        public int hp = 3;
        public int cost = 3;
        public int attack = 3;

        public string name = "Card_Earthen_Ring_Farseer";

        public void setCost(int newCost) { cost = newCost; }

        public void setAttack(int newAttack) { attack = newAttack; }

        public void setHealth(int newHealth) { hp = newHealth; }

        public void setRarity(string newRarity) { rarity = newRarity; }    

        public void setName(string newName) { name = newName; }

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
            var toReturn = new Card_Earthen_Ring_Farseer(deck,this, this,track);
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}