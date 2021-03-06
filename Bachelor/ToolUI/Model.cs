﻿using System;
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
            card.setHealth(6);
            card.setCost(2);

            cardsToDisplay.Add(new CardStats(card));

            card = new GameEngine.Card_User_Defined();
            card.setRarity("rare");
            card.setName("Iron Golem");
            card.setAttack(3);
            card.setHealth(2);
            card.setCost(1);

            cardsToDisplay.Add(new CardStats(card));

            cardsToDisplay.Add(new CardStats(makeCard("Bloodfen raptor", "common", 1, 4, 1)));
            cardsToDisplay.Add(new CardStats(makeCard("Edwin VanCleef", "epic", 2, 4, 2)));
            cardsToDisplay.Add(new CardStats(makeCard("Piloted shredder", "common", 3, 2, 3)));
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
