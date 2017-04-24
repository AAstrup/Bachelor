using Bachelor;

namespace GameEngine
{
    public class Card_Earthen_Ring_Farseer: CardTracker, ICard, ITrackable, ITarget
    {
        public Card_Earthen_Ring_Farseer() : base() { }
        public Card_Earthen_Ring_Farseer(Deck deck,ICard template,ITrackable templateTrack, bool track) : base(deck,template, templateTrack, track ) { }


        public override string GetNameType()
        {
            return "Card_Earthen_Ring_Farseer";
        }

        public override int GetDamage()
        {
            return 3;
        }

        protected override int GetMaxHp()
        {
            return 3;
        }

        public override int GetCost()
        {
            return 3;
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