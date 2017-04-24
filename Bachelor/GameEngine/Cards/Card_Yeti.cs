using Bachelor;

namespace GameEngine
{
    public class Card_Yeti : CardTracker, ICard, ITarget
    {
        public Card_Yeti() : base() { }
        public Card_Yeti(Deck deck, ICard template, ITrackable templateTrack, bool track) : base(deck,template, templateTrack,track) { }


        public override string GetNameType()
        {
            return "Yeti";
        }

        public override int GetDamage()
        {
            return 4;
        }

        protected override int GetMaxHp()
        {
            return 5;
        }

        public override int GetCost()
        {
            return 4;
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