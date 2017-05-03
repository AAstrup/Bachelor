using Bachelor;
using System.Collections.Generic;

namespace GameEngine
{
    public interface IDeckFactory
    {
        List<Deck> GenerateDecks(int deckSize, int maxDuplicates, List<ICard> cardpool);
    }
}