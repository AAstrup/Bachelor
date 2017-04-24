using Bachelor;

namespace GameEngine
{
    public class Card_Shadow_Rager : CardTracker, ICard, ITrackable, ITarget
    {
        public Card_Shadow_Rager() : base() { }
        public Card_Shadow_Rager(Deck deck,ICard template,ITrackable templateTrack, bool track) : base(deck,template, templateTrack,track) { }


        public override string GetNameType()
        {
            return "Card_Shadow_Rager";
        }

        public override int GetDamage()
        {
            return 1;
        }

        protected override int GetMaxHp()
        {
            return 1;
        }

        public override int GetCost()
        {
            return 5;
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