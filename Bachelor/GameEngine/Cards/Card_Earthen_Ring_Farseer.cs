namespace GameEngine
{
    public class Card_Earthen_Ring_Farseer: CardTemplate, ICard, ITarget
    {
        public Card_Earthen_Ring_Farseer() : base() { }

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

        public override ICard InstantiateModel(BoardState board, PlayerBoardState player)
        {
            var toReturn = new Card_Earthen_Ring_Farseer();
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}