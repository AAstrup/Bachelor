using GameEngine;
using System.Collections.Generic;
using System;

namespace Bachelor
{
    public class Deck
    {
        public Deck(List<ICard> deck)
        {
            this.deck = deck;
            results = new List<Result>();
        }
        List<Result> results;
        private List<ICard> deck;

        public void AddResult(Result res)
        {
            results.Add(res);
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
                toReturn.Add(item.InstantiateModel(boardState,playerState));
            }
            return toReturn;
        }

        public List<Result> GetResults()
        {
            return results;
        }
    }
}