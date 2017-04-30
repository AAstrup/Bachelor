using Bachelor;
using System;
using System.Collections.Generic;

namespace GameEngine
{
    public class Result
    {
        public string winner;
        public string loser;
        public Deck winnerDeck;
        public Deck losingDeck;
        public List<ICard> p1CardPlaySequence;
        public List<ICard> p2CardPlaySequence;

        public Result()
        {
            p1CardPlaySequence = new List<ICard>();
            p2CardPlaySequence = new List<ICard>();
        }
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

        public List<ICard> GetCardPlaySequence(playerNr PlayerNr)
        {
            if (PlayerNr == playerNr.Player1)
                return p1CardPlaySequence;
            else
                return p2CardPlaySequence;
        }
    }
}