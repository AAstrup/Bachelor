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
        int manaMax = 10;
        int manaTotal = 0;
        int manaThisTurn;

        Deck templateDeck;
        List<ICard> myDeck;
        List<ICard> myHand;
        List<ICard> myBoard;
        List<ICard> myBoardWithTaunt;//Duplicates from myBoard, but to make it more easily to traverse

        int fatigueDamage = 0;
        int startCards = 4;
        private BoardState board;
        private int maxBoardSize = 7;

        public ITarget Hero;
        internal int deckSize;

        public PlayerBoardState(PlayerSetup playerSetup,bool isGoingFirst,Deck deck,BoardState board)
        {
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
            Singletons.GetPrinter().StartCards(playerSetup, startCards,isGoingFirst);
            for (int i = 0; i < startCards; i++)
            {
                DrawCard();
            }
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

        public List<ICard> GetValidHandOptions()
        {
            return myHand.FindAll(x => x.GetCost() <= manaThisTurn);
        }

        public void NewTurn()
        {
            if(manaMax > manaTotal)
                manaTotal++;
            for (int i = 0; i < myBoard.Count; i++)
            {
                myBoard[i].NewRound();
            }
            manaThisTurn = manaTotal;
            DrawCard();
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