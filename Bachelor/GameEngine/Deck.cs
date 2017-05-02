using GameEngine;
using System.Collections.Generic;
using System;

namespace Bachelor
{
    public class Deck
    {
        public Deck(List<ICard> cards)
        {
            this.cards = cards;
            results = new List<MatchResult>();
        }
        public List<MatchResult> results;
        public List<ICard> cards;
        int wins;
        int losses;

        public void AddResult(MatchResult res)
        {
            results.Add(res);
            bool isWinner = res.winnerDeck == this;
            if (isWinner)
            {
                wins++;
                foreach (var card in cards)
                {
                    ((ITrackable)card).IncreaseTemplatesWins(res.winnerDeck);
                }
            }
            else
            {
                losses++;
                foreach (var card in cards)
                {
                    ((ITrackable)card).IncreaseTemplatesLoss(res.losingDeck);
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
            foreach (var item in cards)
            {
                toReturn.Add(item.InstantiateModel(this,boardState,playerState));
            }
            Shuffle(toReturn);
            return toReturn;
        }

        private void Shuffle(List<ICard> cards)
        {
            Random rng = new Random();
            int currentIndex = cards.Count;
            while (currentIndex > 1)
            {
                currentIndex--;
                int k = rng.Next(currentIndex + 1);
                var value = cards[k];
                cards[k] = cards[currentIndex];
                cards[currentIndex] = value;
            }
        }

        public List<MatchResult> GetResults()
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

        public Dictionary<string,int> GetCardListCompressed()
        {
            var toReturn = new Dictionary<string, int>();
            foreach (var card in cards)
            {
                if (!toReturn.ContainsKey(card.GetNameType()))
                    toReturn.Add(card.GetNameType(), 1);
                else
                {
                    int val = toReturn[card.GetNameType()] + 1;
                    toReturn[card.GetNameType()] = val;
                }
            }
            return toReturn;
        }

        public int GetWins()
        {
            return wins;
        }

        public int GetLosses()
        {
            return losses;
        }
    }
}