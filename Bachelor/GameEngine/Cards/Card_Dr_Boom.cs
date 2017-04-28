using Bachelor;

namespace GameEngine
{
    public class Card_Dr_Boom : CardTracker, ICard, ITrackable, ITarget
    {
        public Card_Dr_Boom() : base() { }
        public Card_Dr_Boom(Deck deck,ICard template,ITrackable templateTrack ) : base(deck,template, templateTrack) { }


        public override string GetNameType()
        {
            return "Card_Dr_Boom";
        }

        public override int GetDamage()
        {
            return 7;
        }

        protected override int GetMaxHp()
        {
            return 8;
        }

        public override int GetCost()
        {
            return 1;
        }

        public override ICard InstantiateModel(Deck deck,BoardState board, PlayerBoardState player)
        {
            var toReturn = new Card_Dr_Boom(deck,this, this);
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}