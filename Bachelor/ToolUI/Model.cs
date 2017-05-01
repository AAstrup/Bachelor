using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;

namespace ToolUI
{
    public class Model
    {
        public List<CardStats> cardsToDisplay = new List<CardStats>() {
            new CardStats(new GameEngine.Card_Shadow_Rager()),
            new CardStats(new GameEngine.Card_Yeti()),
            new CardStats(new GameEngine.Card_Dr_Boom()),
            new CardStats(new GameEngine.Card_Earthen_Ring_Farseer()),
            new CardStats(new GameEngine.Card_Wisp()) };

        public List<CardStats> getCardsToDisplay() { return cardsToDisplay; }

        public bool new_sim;
        public bool new_rank;

        public bool getNewSim() { return new_sim; }

        public bool getNewRank() { return new_rank; }

        public void setNew_sim(bool newBool) { new_sim = newBool; }

        public void setNew_Rank(bool newBool) { new_rank = newBool; }

        public void setCardsToDisplay(List<CardStats> newCards) { cardsToDisplay = newCards; SortCardListAlfabetically(); }

        public Model() { }

        public SolidColorBrush colorFromRarity(string rarity){
            if (rarity.Equals("rare")) { return new SolidColorBrush(Colors.Purple); }
            else if (rarity.Equals("epic")) { return new SolidColorBrush(Colors.Orange); }
            else { return new SolidColorBrush(Colors.Black); }
        }

        public FontWeight boldOrNot(bool value){
            return (value) ? FontWeights.Bold : FontWeights.Normal;
        }

        public SolidColorBrush simulatedOrNot(bool value){
            return (value) ? new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(Colors.Gray);
        }

        public void SortCardListAlfabetically() {
                //NOT IMPLIMENTED JET
        }

    }
}
