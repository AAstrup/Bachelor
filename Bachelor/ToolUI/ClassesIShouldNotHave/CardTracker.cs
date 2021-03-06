﻿using Bachelor;
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
        HashSet<Deck> decksWithin;
        Dictionary<ICard, int> DominanceDegree;//This is a dictionary as each card will increment

        /// <summary>
        /// Constructor used by the template, which will be copied later on.
        /// </summary>
        public CardTracker() : base()
        {
            decksWithin = new HashSet<Deck>();
            DominanceDegree = new Dictionary<ICard, int>();
        }

        /// <summary>
        /// Constructor for non-templates, thus cards used in simulations
        /// </summary>
        /// <param name="templateAsCard"></param>
        /// <param name="templateAsTrackable"></param>
        public CardTracker(Deck deck, ICard templateAsCard, ITrackable templateAsTrackable)
        {
            this.templateAsCard = templateAsCard;
            this.templateAsTrackable = templateAsTrackable;
            decksWithin = ((ITrackable)templateAsCard).GetDecksWithThis();
            templateAsTrackable.RegisterCopy(this);
        }

        public double GetDominance(ICard card)
        {
            var cardName = card.GetNameType();
            int number = 0;
            int times = 0;
            int value = 0;
            foreach (var name in DominanceDegree.Keys)
            {
                if (name.GetNameType().Equals(cardName))
                {
                    //card = name;
                    value = DominanceDegree[name];
                    if (value != 0)
                    {
                        number += DominanceDegree[name];
                        times++;
                    }

                }
            }

            if (times == 0)
            {
                number = 1;
                times = 1;
            }

            return ((number * 1.0) / (times * 1.0));
        }


        public void IncreaseTemplatesWins(Deck deck)
        {
            if (!decksWithin.Contains(deck))
            {
                AddDeck(deck);
            }
            wins++;
        }

        public void IncreaseTemplatesLoss(Deck deck)
        {
            if (!decksWithin.Contains(deck))
            {
                AddDeck(deck);
            }
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
            var currentMaxWinRate = 0.0;//decksWithin[0].GetWinLossRate();
            Deck deckToReturn = null;
            foreach (var deck in decksWithin)
            {
                var rate = deck.GetWinLossRate();
                if (rate >= currentMaxWinRate)
                {
                    currentMaxWinRate = rate;
                    deckToReturn = deck;
                }
            }
            return deckToReturn;
        }

        public HashSet<Deck> GetDecksWithThis()
        {
            return decksWithin;
        }

        public void AddDeck(Deck deck)
        {
            if (!decksWithin.Contains(deck))
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

        public ITrackable GetTemplateAsTrackable()
        {
            return (ITrackable)templateAsCard;
        }

        public void DecreaseTemplateDominance(ICard copy)
        {
            DominanceDegree[copy] = DominanceDegree[copy] + 1;
        }

        public void RegisterCopy(ICard copy)
        {
            DominanceDegree.Add(copy, 0);
        }

        public void Reset()
        {
            wins = 0;
            losses = 0;
            decksWithin = new HashSet<Deck>();
            DominanceDegree = new Dictionary<ICard, int>();
        }
    }
}