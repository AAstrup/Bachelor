using System;
using System.Collections.Generic;
using Bachelor;

namespace GameEngine
{
    public class DeckFactory
    {
        Random random = new Random();
        int deckSize;

        public DeckFactory(int deckSize)
        {
            this.deckSize = deckSize;
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
    }
}