using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.UI.Text;
using GameEngine;
using Bachelor;

namespace ToolUI
{
    public class Model
    {
        public List<CardStats> cardsToDisplay { get; set; }

        public bool new_sim;
        public bool new_rank;

        public bool getNewSim() { return new_sim; }

        public bool getNewRank() { return new_rank; }

        public void setNew_sim(bool newBool) { new_sim = newBool; }

        public void setNew_Rank(bool newBool) { new_rank = newBool; }

        public Model() {
            cardsToDisplay = new List<CardStats>();
            GameEngine.ICard card = new GameEngine.Card_User_Defined();

            card.setName("DR. Boom");
            card.setRarity("epic");
            card.setAttack(7);
            card.setHealth(6);
            card.setCost(2);
            cardsToDisplay.Add(new CardStats(card));

            card = new GameEngine.Card_User_Defined();
            card.setRarity("common");
            card.setName("Wisp");
            card.setAttack(1);
            card.setHealth(1);
            card.setCost(1);
            cardsToDisplay.Add(new CardStats(card));

            card = new GameEngine.Card_User_Defined();
            card.setRarity("common");
            card.setName("IronClaw Bear");
            card.setAttack(1);
            card.setHealth(3);
            card.setCost(3);
            //card.SetHasTaunt(true);

            cardsToDisplay.Add(new CardStats(card));

            card = new GameEngine.Card_User_Defined();
            card.setRarity("common");
            card.setName("Ivory Knight");
            card.setAttack(2);
            card.setHealth(4);
            card.setCost(2);

            cardsToDisplay.Add(new CardStats(card));

            card = new GameEngine.Card_User_Defined();
            card.setRarity("common");
            card.setName("Iron Golem");
            card.setAttack(3);
            card.setHealth(2);
            card.setCost(1);

            Random r = new Random();
            for(int i = 0; i< 3; i++)
            {
                cardsToDisplay.Add(new CardStats(makeCard("Murloc"+i, "common", r.Next(1, 9), r.Next(1, 9), r.Next(1, 9))));
            }
            /*
            cardsToDisplay.Add(new CardStats(makeCard("Murloc","common",1,2,1)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc1", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc2", "common", 3, 4, 1)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc3", "common", 3, 4, 1)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc4", "common", 2, 2, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc5", "common", 1, 2, 5)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc6", "common", 4, 3, 5)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc7", "common", 3, 2, 1)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc8", "common", 2, 1, 4)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc9", "common", 4, 2, 4)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc10", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc11", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc12", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc13", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc14", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc15", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc16", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc17", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc18", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc19", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc20", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc21", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc22", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc23", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc24", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc25", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc26", "common", 1, 1, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc30", "common", 1, 1, 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc31", "common", 1, 1, 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc33", "common", 1, 1, 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc32", "common", 1, 1, 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc34", "common", 1, 1, 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc35", "common", 1, 1, 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc36", "common", 1, 1, 3)));
            */
            /*
            cardsToDisplay.Add(new CardStats(makeCard("Murloc", "common", r.Next(1, 9), r.Next(1, 9), r.Next(1, 9))));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc1", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc2", "common", 3, 4, r.Next(1, 9))));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc3", "common", 3, 4, r.Next(1, 9))));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc4", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc5", "common", r.Next(1, 9), r.Next(1, 9), 5)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc6", "common", 4, 3, 5)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc7", "common", 3, r.Next(1, 9), r.Next(1, 9))));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc8", "common", r.Next(1, 9), r.Next(1, 9), 4)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc9", "common", 4, r.Next(1, 9), 4)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc10", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc11", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc12", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc13", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc14", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc15", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc16", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc17", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc18", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc19", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc20", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc21", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc22", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc23", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc24", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc25", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc26", "common", r.Next(1, 9), r.Next(1, 9), 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc30", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc31", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc33", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc32", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc34", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc35", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc36", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc44", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc55", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc66", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc77", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc88", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc98", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            cardsToDisplay.Add(new CardStats(makeCard("Murloc99", "common", r.Next(1, 9), r.Next(1, 9), 3)));
            */
            cardsToDisplay.Add(new CardStats(card));
        }

        private GameEngine.Card_User_Defined makeCard(string name, string rarity, int attack, int health,int cost)
        {
            var card = new GameEngine.Card_User_Defined();
            card.setRarity(rarity);
            card.setName(name);
            card.setAttack(attack);
            card.setHealth(health);
            card.setCost(cost);
            return card;
        }

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

        //IF one appears more than once, don't take it's value
        public Dictionary<string, int[]> calculateWinRatio(List<ICard> cards, List<Deck> res){

            Dictionary<string, int[]> results = new Dictionary<string, int[]>();
            foreach(var card in cards){
                results.Add(card.GetNameType(), new int[] {0,0});
            }
            HashSet<string> alreadyIncluded = new HashSet<string>();

            foreach(var deck in res){
                foreach (var result in deck.results){
                    foreach(var c in result.winnerDeck.cards){
                        if (!alreadyIncluded.Contains(c.GetNameType()))
                        {
                            var numbers = results[c.GetNameType()] as int[];
                            results[c.GetNameType()] = new int[] { numbers[0] + 1, numbers[1] + 1 };
                            alreadyIncluded.Add(c.GetNameType());
                        }
                    }

                    alreadyIncluded.Clear();

                    foreach (var c in result.losingDeck.cards)
                    {
                        if (!alreadyIncluded.Contains(c.GetNameType()))
                        {
                            var numbers = results[c.GetNameType()] as int[];
                            results[c.GetNameType()] = new int[] { numbers[0], numbers[1] + 1 };
                            alreadyIncluded.Add(c.GetNameType());
                        }
                    }

                    alreadyIncluded.Clear();

                }
            }
            return results;
        }


    }
}
