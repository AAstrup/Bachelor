using System.Collections.Generic;
using GameEngine;
using System.Linq;

namespace Bachelor
{
    public class SimulationResults
    {
        public List<ICard> Cardpool;
        public List<ITrackable> CardpoolAsTrackable;
        public List<Deck> Decks;
        public long ElapsedMilliseconds;

        public SimulationResults(List<Deck> decks, List<ICard> cardpool, long elapsedMilliseconds)
        {
            this.Decks = decks;
            this.Cardpool = cardpool;
            CardpoolAsTrackable = CastToTrackable(cardpool);
            this.ElapsedMilliseconds = elapsedMilliseconds;
        }

        private static List<ITrackable> CastToTrackable(List<ICard> cardpool)
        {
            var toReturn = new List<ITrackable>();
            foreach (var item in cardpool)
            {
                toReturn.Add((ITrackable)item);
            }
            return toReturn;
        }
    }
}