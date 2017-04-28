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
            MeasureUniqueDecks(deckSize, maxDuplicates, cardpool, decks);
            int matches = 0;
            int decksToSum = decks.Count;
            while (decksToSum > 0)
            {
                matches += decksToSum;
                decksToSum--;
            }
            Console.WriteLine("Generated all unique decks " + decks.Count + " resulting in " + matches + " matches");
            return decks;
        }

        private void MeasureUniqueDecks(int deckSize, int maxDuplicates, List<ICard> cardpool, List<Deck> decks)
        {
            for (int firstEle = 0; firstEle < cardpool.Count; firstEle++)
            {
                AddUniqueDecksGivenContext(new LinkedTreeElement(firstEle), 1, maxDuplicates, 1, deckSize, cardpool, decks);
            }
        }
        

        void AddUniqueDecksGivenContext(LinkedTreeElement lastElement, int duplicateNumber, int maxDuplicates,
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
                    AddUniqueDecksGivenContext(newElement, duplicateNumber, maxDuplicates, currentDeckSize + 1, maxDeckSize, cardpool, decks);
                }
            }
        }
    }
}