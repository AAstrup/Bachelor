using Bachelor;

namespace GameEngine
{
    public class Card_Yeti : CardTracker, ICard, ITarget
    {
        public Card_Yeti() : base() { }
        public Card_Yeti(Deck deck, ICard template, ITrackable templateTrack) : base(deck,template, templateTrack) { }


        public override string GetNameType()
        {
            return "Yeti";
        }

        public override int GetDamage()
        {
            return 4;
        }

        public override int GetMaxHp()
        {
            return 5;
        }

        public override int GetCost()
        {
            return 4;
        }

        public override ICard InstantiateModel(Deck deck, BoardState board, PlayerBoardState player)
        {
            var toReturn = new Card_Yeti(deck,this,this);
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}