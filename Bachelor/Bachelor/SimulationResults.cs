using System.Collections.Generic;
using GameEngine;

namespace Bachelor
{
    internal class SimulationResults
    {
        public List<ICard> Cardpool;
        public List<Deck> Decks;
        public long ElapsedMilliseconds;

        public SimulationResults(List<Deck> decks, List<ICard> cardpool, long elapsedMilliseconds)
        {
            this.Decks = decks;
            this.Cardpool = cardpool;
            this.ElapsedMilliseconds = elapsedMilliseconds;
        }
    }
}