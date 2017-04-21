using Bachelor;
using System;

namespace GameEngine
{
    public class Result
    {
        public string winner;
        public string loser;
        public Deck winnerDeck;
        public Deck losingDeck;

        internal void SetWinner(string name,Deck winnerDeck)
        {
            winner = name;
            this.winnerDeck = winnerDeck;
        }

        internal void SetLoser(string name, Deck losingDeck)
        {
            loser = name;
            this.losingDeck = losingDeck;
        }
    }
}