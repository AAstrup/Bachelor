using Bachelor;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class UniqueDeckFactory : IDeckFactory
    {
        public List<Deck> GenerateDecks(int deckSize, int maxDuplicates, List<ICard> cardpool)
        {
            List<Deck> decks = new List<Deck>();
            CreateGenerateUniqueDecks(deckSize, maxDuplicates, cardpool, decks);
            return decks;
        }

        /// <summary>
        /// Called to generate unique decks
        /// </summary>
        /// <param name="deckSize">Size of the decks</param>
        /// <param name="maxDuplicates">Maximum of duplicates in a single deck</param>
        /// <param name="cardpool">Cards to build decks with</param>
        /// <param name="decks">Accumulator</param>
        private void CreateGenerateUniqueDecks(int deckSize, int maxDuplicates, List<ICard> cardpool, List<Deck> decks)
        {
            for (int firstEle = 0; firstEle < cardpool.Count; firstEle++)
            {
                FillUniqueDecks(new LinkedTreeElement(firstEle), 0, maxDuplicates, 1, deckSize, cardpool, decks);
            }
        }

        /// <summary>
        /// Generates unique decks recursively, and injects into list
        /// </summary>
        /// <param name="lastElement">When calling this function, this element will be the first in the list</param>
        /// <param name="duplicateNumber">Current accumulation of duplicates</param>
        /// <param name="maxDuplicates">Amount of duplicates allowed</param>
        /// <param name="currentDeckSize">Current accumulation of cards</param>
        /// <param name="maxDeckSize">Amount of cards allowed</param>
        /// <param name="cardpool">Card to choice between</param>
        /// <param name="decks">List to inject decks generated in</param>
        void FillUniqueDecks(LinkedTreeElement lastElement, int duplicateNumber, int maxDuplicates,
            int currentDeckSize, int maxDeckSize, List<ICard> cardpool, List<Deck> decks)
        {
            if (currentDeckSize == maxDeckSize)
            {
                List<ICard> toReturn = new List<ICard>();
                while (lastElement.parent != null)
                {
                    toReturn.Add(cardpool[lastElement.value]);
                    lastElement = lastElement.parent;
                }
                toReturn.Add(cardpool[lastElement.value]);
                decks.Add(new Deck(toReturn));
            }
            else
            {
                for (int i = lastElement.value; i < cardpool.Count; i++)
                {
                    if (i == lastElement.value)
                    {
                        if (duplicateNumber == maxDuplicates)
                            continue;
                        else
                            duplicateNumber++;
                    }
                    else
                        duplicateNumber = 1;
                    var newElement = new LinkedTreeElement(lastElement, i);
                    FillUniqueDecks(newElement, duplicateNumber, maxDuplicates, currentDeckSize + 1, maxDeckSize, cardpool, decks);
                }
            }
        }
    }
}