using Bachelor;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class CardTracker : CardTemplate, ITrackable
    {
        private ICard templateAsCard;//If null, then this is the template
        private ITrackable templateAsTrackable;//If null, then this is the template
        int wins;
        int losses;
        List<Deck> decksWithin;

        /// <summary>
        /// Constructor used by the template, which will be copied later on.
        /// </summary>
        public CardTracker():base() {
            decksWithin = new List<Deck>();
        }

        /// <summary>
        /// Constructor for non-templates, thus cards used in simulations
        /// </summary>
        /// <param name="templateAsCard"></param>
        /// <param name="templateAsTrackable"></param>
        public CardTracker(Deck deck,ICard templateAsCard,ITrackable templateAsTrackable)
        {
            this.templateAsCard = templateAsCard;
            this.templateAsTrackable = templateAsTrackable;
            decksWithin = ((ITrackable)templateAsCard).GetDecksWithThis();
        }

        public void IncreaseTemplatesWins(Deck deck)
        {
            AddDeck(deck);
            wins++;
        }

        public void IncreaseTemplatesLoss(Deck deck)
        {
            AddDeck(deck);
            losses++;
        }

        public double GetWinLossRate()
        {
            if (wins + losses == 0)
                return -1;
            double toreturn = ((Double)wins) / ((Double)(wins + losses));
            return toreturn * 100.0;
        }

        public Deck GetBestDeck()
        {
            var currentMaxWinRate = decksWithin[0].GetWinLossRate();
            int indexToReturn = 0;
            for (int i = 1; i < decksWithin.Count; i++)
            {
                var rate = decksWithin[i].GetWinLossRate();
                if (rate > currentMaxWinRate)
                {
                    currentMaxWinRate = rate;
                    indexToReturn = i;
                }
            }
            return decksWithin[indexToReturn];
        }

        public List<Deck> GetDecksWithThis()
        {
            return decksWithin;
        }

        public void AddDeck(Deck deck)
        {
            decksWithin.Add(deck);
        }

        public int GetWins()
        {
            return wins;
        }

        public int GetLosses()
        {
            return losses;
        }

        public ICard GetTemplate()
        {
            return templateAsCard;
        }
    }
}