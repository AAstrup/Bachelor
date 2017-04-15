namespace GameEngine
{
    public class Card_Yeti : CardTemplate, ICard, ITarget
    {
        public Card_Yeti() : base() { }

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

        public override ICard InstantiateModel(BoardState board, PlayerBoardState player)
        {
            var toReturn = new Card_Yeti();
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}