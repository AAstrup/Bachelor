using System;
using System.Collections.Generic;
using Bachelor;

namespace GameEngine
{
    public class DeckFactory : IDeckFactory
    {
        Random random = new Random();
        int deckSize;
        private int amountOfDecksToGenerate;

        public DeckFactory(int amountOfDecksToGenerate)
        {
            this.amountOfDecksToGenerate = amountOfDecksToGenerate;
        }
        internal List<ICard> GenerateCards(List<ICard> cardPool)
        {
            var deck = new List<ICard>();
            for (int i = 0; i < deckSize; i++)
            {
                int rand = random.Next(0, cardPool.Count);
                deck.Add(cardPool[rand]);
            }
            return deck;
        }

        public Deck GenerateDeck(List<ICard> cardPool)
        {
            return new Deck(GenerateCards(cardPool));
        }

        public List<Deck> GetAllUniqueDecks(List<ICard> cardpool)
        {
            var toReturn = new List<Deck>();
            for (int i = 0; i < amountOfDecksToGenerate; i++)
            {
                toReturn.Add(GenerateDeck(cardpool));
            }
            return toReturn;
        }

        public List<Deck> GenerateDecks(int deckSize, int maxDuplicates, List<ICard> cardpool)
        {
            this.deckSize = deckSize;
            return GetAllUniqueDecks(cardpool);
        }
    }
}