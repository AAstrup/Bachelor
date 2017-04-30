using Bachelor;
using GameEngine.Printers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class PlayerBoardState
    {
        public PlayerBoardState opponent;
        public PlayerSetup playerSetup;
        internal int manaMax = 10;
        internal int manaTotal = 0;
        internal int manaThisTurn;

        internal Deck templateDeck;
        internal List<ICard> myDeck;
        internal List<ICard> myHand;
        internal List<ICard> myBoard;
        internal List<ICard> myBoardWithTaunt;//Duplicates from myBoard, but to make it more easily to traverse

        internal int fatigueDamage = 0;
        internal int startCards = 4;
        internal BoardState board;
        internal int maxBoardSize = 7;

        public Hero Hero;
        internal int deckSize;
        private playerNr playerNr;

        /// <summary>
        /// Used as ctor for copying
        /// </summary>
        public PlayerBoardState(PlayerBoardState original) { }

        /// <summary>
        /// Used for creating the original player
        /// </summary>
        /// <param name="playerSetup"></param>
        /// <param name="isGoingFirst"></param>
        /// <param name="deck"></param>
        /// <param name="board"></param>
        /// <param name="playerNr"></param>
        public PlayerBoardState(PlayerSetup playerSetup,bool isGoingFirst,Deck deck,BoardState board,playerNr playerNr)
        {
            this.playerNr = playerNr;
            Hero = new Hero(board,this);
            this.board = board;
            this.playerSetup = playerSetup;
            myDeck = deck.GetCardList(board,this);
            templateDeck = deck;
            myHand = new List<ICard>();
            myBoard = new List<ICard>();
            myBoardWithTaunt = new List<ICard>();
            if (!isGoingFirst)
                startCards++;
            for (int i = 0; i < startCards; i++)
            {
                DrawCard();
            }
            Singletons.GetPrinter().StartCards(playerSetup, startCards,isGoingFirst, myHand);
        }

        internal List<ITarget> GetValidAttackVictims()
        {
            if (myBoardWithTaunt.Count > 0)
                return myBoardWithTaunt.Cast<ITarget>().ToList();
            else
            {
                var toReturn = myBoard.Cast<ITarget>().ToList();
                toReturn.Add(Hero);
                return toReturn;
            }
        }

        public List<ICard> GetWholeHand()
        {
            return myHand;
        }

        public List<ICard> GetTauntBoard()
        {
            return myBoardWithTaunt;
        }

        /// <summary>
        /// Returns all cards which can be afforded
        /// </summary>
        /// <returns></returns>
        public List<ICard> GetValidHandOptions()
        {
            return myHand.FindAll(x => x.GetCost() <= manaThisTurn);
        }

        public playerNr GetPlayerNr()
        {
            return playerNr;
        }

        public void NewTurn(BoardState state)
        {
            board = state;
            if(manaMax > manaTotal)
                manaTotal++;
            for (int i = 0; i < myBoard.Count; i++)
            {
                myBoard[i].NewRound();
            }
            manaThisTurn = manaTotal;
            DrawCard();
        }

        internal PlayerBoardState Copy(BoardState boardState)
        {
            var original = this;
            var toReturn = new PlayerBoardState(original)
            {
                playerSetup = original.playerSetup,
                manaMax = original.manaMax,
                manaTotal = original.manaTotal,
                manaThisTurn = original.manaThisTurn,

                templateDeck = original.templateDeck,

                fatigueDamage = original.fatigueDamage,
                startCards = original.startCards,
                board = boardState,
                maxBoardSize = original.maxBoardSize,

                deckSize = original.deckSize,
                playerNr = original.playerNr
            };

            toReturn.myDeck = CopyAList(templateDeck,original.myDeck,boardState,toReturn);
            toReturn.myHand = CopyAList(templateDeck, original.myHand, boardState, toReturn);
            toReturn.myBoard = CopyAList(templateDeck, original.myBoard, boardState, toReturn);
            toReturn.myBoardWithTaunt = GetTaunts(myBoard);
            toReturn.Hero = original.Hero.Copy(board, toReturn);
            this.board = boardState;

            return toReturn;
        }

        private List<ICard> GetTaunts(List<ICard> myBoard)
        {
            var tauntList = new List<ICard>();
            foreach (var item in myBoard)
            {
                if (item.HasTaunt())
                    tauntList.Add(item);
            }
            return tauntList;
        }

        List<ICard> CopyAList(Deck deck,List<ICard> oldList,BoardState board,PlayerBoardState player)
        {
            List<ICard> newList = new List<ICard>(oldList.Count);

            oldList.ForEach((item) =>
            {
                newList.Add(item.Copy(deck,board,player));
            });
            return newList;
        }

        public int GetManaLeft()
        {
            return manaThisTurn;
        }

        private void DrawCard()
        {
            if (myDeck.Count > 0)
            {
                ICard toAdd = myDeck[myDeck.Count - 1];
                myDeck.RemoveAt(myDeck.Count - 1);
                myHand.Add(toAdd);
            }
            else
            {
                fatigueDamage++;
                Hero.Damage(fatigueDamage,"fatigue");
                Hero.CheckForDeath();
            }
        }

        /// <summary>
        /// Returns the cards on the board which has yet to attack.
        /// </summary>
        /// <returns></returns>
        public List<ICard> GetValidBoardOptions()
        {
            return myBoard.FindAll(x => x.CanAttack());
        }

        public List<ICard> GetWholeBoard()
        {
            return myBoard;
        }

        public bool BoardIsFull()
        {
            return myBoard.Count >= maxBoardSize;
        }

        public void SetOpponent(PlayerBoardState opponent)
        {
            this.opponent = opponent;
        }
        public void SpendMana(ICard card)
        {
            manaThisTurn -= card.GetCost();
        }

        public Deck GetDeck()
        {
            return templateDeck;
        }
    }
}