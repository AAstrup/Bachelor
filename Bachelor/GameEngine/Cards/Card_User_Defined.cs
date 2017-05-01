using Bachelor;

namespace GameEngine
{
    public class Card_User_Defined : CardTracker, ICard, ITrackable, ITarget
    {
        private string name;

        /// <summary>
        /// This constructor is used to create a card template
        /// A template will be copied into games using the InstantiateModel
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="totalHP"></param>
        /// <param name="cost"></param>
        /// <param name="name"></param>
        /// <param name="hasCharge"></param>
        public Card_User_Defined(int damage = 1, int totalHP = 1, int cost = 1, string name = "Unnamed",bool hasCharge = false,bool hasTaunt = false) : base()
        {
            this.damage = damage;
            this.totalHP = totalHP;
            this.cost = cost;
            this.name = name;
            this.canAttack = hasCharge;
            this.hasCharge = hasCharge;
            this.hasTaunt = hasTaunt;
        }

        /// <summary>
        /// The constructor is used to create a COPY of a card template
        /// </summary>
        /// <param name="deck">Deck to copy it in</param>
        /// <param name="template">Template / original card</param>
        /// <param name="templateTrack">Template given as trackable</param>
        public Card_User_Defined(Deck deck, ICard template, ITrackable templateTrack) : base(deck,template, templateTrack) {
            this.damage = template.GetDamage();
            this.totalHP = template.GetMaxHp();
            this.cost = template.GetCost();
            this.name = template.GetNameType();
            this.canAttack = template.HasCharge();
            this.hasTaunt = template.HasTaunt();
        }



        public override string GetNameType()
        {
            return name;
        }
        
        public override ICard InstantiateModel(Deck deck, BoardState board, PlayerBoardState player)
        {
            var toReturn = new Card_User_Defined(deck, this, this);
            toReturn.player = player;
            toReturn.board = board;
            return toReturn;
        }
    }
}