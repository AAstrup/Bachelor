using GameEngine;
using System.Collections.Generic;
using System;

namespace Bachelor
{
    public class Deck
    {
        public Guid guid;
        public Deck(List<ICard> deck)
        {
            guid = new Guid();
            this.deck = deck;
            results = new List<Result>();
        }
        List<Result> results;
        private List<ICard> deck;
        int wins;
        int losses;

        public void AddResult(Result res)
        {
            results.Add(res);
            bool isWinner = res.winnerDeck == this;
            if (isWinner)
            {
                wins++;
                foreach (var card in deck)
                {
                    ((ITrackable)card).IncreaseTemplatesWins();
                }
            }
            else
            {
                losses++;
                foreach (var card in deck)
                {
                    ((ITrackable)card).IncreaseTemplatesLoss();
                }
            }
        }

        /// <summary>
        /// Copies the cards, such that they can be manipulated ingame.
        /// </summary>
        /// <returns></returns>
        internal List<ICard> GetCardList(BoardState boardState,PlayerBoardState playerState)
        {
            List<ICard> toReturn = new List<ICard>();
            foreach (var item in deck)
            {
                toReturn.Add(item.InstantiateModel(this,boardState,playerState));
            }
            return toReturn;
        }

        public List<Result> GetResults()
        {
            return results;
        }

        public double GetWinLossRate()
        {
            if (wins + losses == 0)
                return -1;
            double toreturn = ((Double)wins) / ((Double)(wins + losses));
            return toreturn * 100.0;
        }
    }
}