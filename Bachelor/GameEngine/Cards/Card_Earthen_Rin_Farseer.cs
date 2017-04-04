namespace GameEngine
{
    public class Card_Earthen_Rin_Farseer: CardTemplate, ICard, ITarget
    {
        public Card_Earthen_Rin_Farseer() : base() { }

        
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

        public override ICard Copy(BoardState board, PlayerBoardState player)
        {
            var toReturn = new Card_Earthen_Rin_Farseer();
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}