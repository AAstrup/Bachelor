using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using GameEngine;
using Bachelor;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ToolUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CardExpediton : Page
    {

        ObservableCollection<string> obsList = new ObservableCollection<string>();

        ContainerClass cont;

        CardStats thisCard;

        string extraString = "";

        public CardExpediton()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var con = e.Parameter as ContainerClass;
            cont = con;
            if(cont.simulationCriteria != null) { Rerun_button.IsEnabled = true; }
            else { Rerun_button.IsEnabled = false; }
            if (!(con.getCard() == null))
            {
                cont = con;
                var cardStat = con.getCard() as CardStats;
                setCardData(cardStat);
            }
            else
            {
                CostBox_Copy.Visibility = Visibility.Visible;
                NameBlock.Visibility = Visibility.Collapsed;
                thisCard = new CardStats(new Card_User_Defined());
                textBlock_Copy10.Text = "UNKNOWN"; textBlock_Copy10.FontSize = 50;
                textBlock_Copy8.Text = "UNKNOWN"; textBlock_Copy8.FontSize = 50;
                textBlock_Copy6.Text = "?"; //RANK
                thisCard.card.setName("NNNNNNNNNNNNAAAAAMMMMMMMME");
                comboBoxWithCards.ItemsSource = gennereateCollection(thisCard);
            }


        }

        private void setCardData(CardStats cardStat)
        {
            var data = cardStat.card;

            thisCard = cardStat; //Have a local variable
            NameBlock.Text = data.GetNameType();
            NameBlock_Copy.Text = data.GetNameType();
            CostBox.Text = data.GetCost().ToString();
            AttackBox.Text = data.GetDamage().ToString();
            HealthBox.Text = data.GetCost().ToString();
            HealthBox.Text = data.GetMaxHp().ToString();
            checkBox_Common_Copy.IsChecked = data.HasTaunt();
            checkBox_Rare_Copy.IsChecked = data.HasCharge();

            if (data.GetRarity().Equals("common")) { checkBox_Common.IsChecked = true; }
            else if (data.GetRarity().Equals("rare")) { checkBox_Rare.IsChecked = true; }
            else { checkBox_Rare.IsChecked = true; }
            NameBlock_Copy1.Text = "  %";

            textBlock_Copy6.Text = cardStat.rank; //RANK
            if (cardStat.win_ratio == -1)
            {
                textBlock_Copy6.Text = "?"; //RANK
                textBlock_Copy10.Text = "UNKNOWN"; textBlock_Copy10.FontSize = 50;
                textBlock_Copy8.Text = "UNKNOWN"; textBlock_Copy8.FontSize = 50;
            }
            else
            {
                textBlock_Copy10.Text = cardStat.win_ratio + " %"; //WIN-RATIO
                textBlock_Copy8.Text = "" + cardStat.domminance; //DOMMINANCE
            }

            cardStat.card = data;
            comboBoxWithCards.ItemsSource = gennereateCollection(cardStat);
            examineAllCards();
            if(!thisCard.note.Equals("")) { textBox_Copy.Text = thisCard.note; }
        }

        private ObservableCollection<string> gennereateCollection(CardStats card)
        {
            var cards = cont.getModel().cardsToDisplay;
            ObservableCollection<string> list = new ObservableCollection<string>();
            string name;
            foreach (CardStats c in cards)
            {
                name = c.card.GetNameType();
                if (name.Equals(card.card.GetNameType())) { }
                else { list.Add(name); }
            }

            return list;
        }

        private void checkBox_Checked_common(object sender, RoutedEventArgs e)
        {
            checkBox_Common.IsChecked = true;
            checkBox_Rare.IsChecked = false;
            checkBox_Epic.IsChecked = false;
        }

        private void checkBox_Checked_rare(object sender, RoutedEventArgs e)
        {
            checkBox_Rare.IsChecked = true;
            checkBox_Common.IsChecked = false;
            checkBox_Epic.IsChecked = false;
        }

        private void checkBox_Checked_epic(object sender, RoutedEventArgs e)
        {
            checkBox_Epic.IsChecked = true;
            checkBox_Common.IsChecked = false;
            checkBox_Rare.IsChecked = false;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            thisCard.changed = false;
            cont.setcard(thisCard);
            this.Frame.Navigate((typeof(MainPage)), cont);
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            if ((checkBox_Common.IsChecked ?? false) || (checkBox_Rare.IsChecked ?? false) || (checkBox_Epic.IsChecked ?? false)
                && (!CostBox_Copy.Text.Equals("NAME") || CostBox_Copy.Visibility.Equals(Visibility.Collapsed))
                && (!CostBox.Text.Equals("COST"))
                && (!AttackBox.Text.Equals("ATTACK"))
                && (!HealthBox.Text.Equals("HEALTH")))
            {
                thisCard.note = textBox_Copy.Text;
                thisCard.card.setCost(Int32.Parse(CostBox.Text));
                thisCard.card.setAttack(Int32.Parse(AttackBox.Text));
                thisCard.card.setHealth(Int32.Parse(HealthBox.Text));
                thisCard.card.SetHasTaunt(checkBox_Common_Copy.IsChecked ?? false);
                thisCard.card.SetHasCharge(checkBox_Rare_Copy.IsChecked ?? false);

                if (checkBox_Common.IsChecked ?? false) { thisCard.card.setRarity("common"); }
                else if (checkBox_Rare.IsChecked ?? false) { thisCard.card.setRarity("rare"); }
                else { thisCard.card.setRarity("epic"); }

                thisCard.changed = true;
                cont.setcard(thisCard);
                if (!CostBox_Copy.Visibility.Equals(Visibility.Collapsed))
                {
                    thisCard.card.setName(CostBox_Copy.Text);
                    cont.getModel().cardsToDisplay.Add(thisCard);
                }
                cont.chardHaveBeenChanged = true;
                this.Frame.Navigate((typeof(MainPage)), cont);
            }
            else
            {

            }
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            NameBlock.Text = thisCard.card.GetNameType();
            CostBox.Text = thisCard.card.GetCost().ToString();
            AttackBox.Text = thisCard.card.GetDamage().ToString();
            HealthBox.Text = thisCard.card.GetMaxHp().ToString();

            if (thisCard.card.GetRarity().Equals("common")) { checkBox_Checked_common(null, null); }
            else if (thisCard.card.GetRarity().Equals("rare")) { checkBox_Checked_rare(null, null); }
            else { checkBox_Checked_epic(null, null); }

        }

        private void examineAllCards()
        {
            var highestWinRatio = -1.0;
            object[] highestValues = new object[] {-1,-1,""};

            foreach (var card in cont.model.cardsToDisplay)
            {
                if (!card.card.GetNameType().Equals(thisCard.card.GetNameType()))
                {
                    var decksA = ((thisCard.card) as ITrackable).GetDecksWithThis();
                    var decksB = (card.card as ITrackable).GetDecksWithThis();
                    HashSet<Deck> decksToExamine = new HashSet<Deck>();

                    foreach (var deck in decksA)
                    {
                        if (decksB.Contains(deck))
                        {
                            decksToExamine.Add(deck);
                        }
                    }

                    var objects = calculateWinRatio(decksToExamine, card);
                    var fights = Convert.ToInt32(objects[0]);
                    var wins = Convert.ToInt32(objects[1]);
                    var winRatioWithOtherCard = Convert.ToDouble(objects[2]);
                    if(highestWinRatio < winRatioWithOtherCard) {
                        highestValues[0] = fights;
                        highestValues[1] = wins;
                        highestValues[2] = card.card.GetNameType();
                        highestWinRatio = winRatioWithOtherCard; }
                }
            }
            if(highestWinRatio > 50.0)
            {
                if(extraString != "" && textBox_Copy.Text.Contains(extraString))
                {
                    textBox_Copy.Text = textBox_Copy.Text.Replace(extraString, "");
                }
                extraString = "Best combined with " + highestValues[2] + ". Win ratio was " + highestWinRatio + "% out of " + highestValues[0] + " fights";
                textBox_Copy.Text = (textBox_Copy.Text).Trim();
                textBox_Copy.Text += Environment.NewLine + extraString;
            }

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var obj = e.AddedItems;
            string item = "";
            if (obj.Count > 0)
            {
                var selectedItem = obj[0];
                item = selectedItem as string;
            }
            var card = thisCard;
            foreach (var ca in cont.model.cardsToDisplay)
            {
                if (ca.card.GetNameType().Equals(item))
                {
                    card = ca; break;
                }
            }

            var decksA = ((thisCard.card) as ITrackable).GetDecksWithThis();
            var decksB = (card.card as ITrackable).GetDecksWithThis();
            HashSet<Deck> decksToExamine = new HashSet<Deck>();

            foreach (var deck in decksA)
            {
                if (decksB.Contains(deck))
                {
                    decksToExamine.Add(deck);
                }
            }

            
            var objects = calculateWinRatio(decksToExamine,card);
            var fights = Convert.ToInt32(objects[0]);
            var wins = Convert.ToInt32(objects[1]);
            var winRatioWithOtherCard = Convert.ToDouble(objects[2]);

            if (wins == 0 && fights == 0)
            {
                NameBlock_Copy1.Text = "Never got to play together";
                winRatioWithOtherCard = -1.0;
            }
            else if (wins == 0)
            {
                NameBlock_Copy1.Text = "won " + 0 + " %" + Environment.NewLine + "Based on " + fights + " fights";
                winRatioWithOtherCard = 0.0;
            }
            else
            {
                winRatioWithOtherCard = Math.Round((1.0 * wins) / (1.0 * fights) * 100.0, 2);

                NameBlock_Copy1.Text = "won " + winRatioWithOtherCard + " %" + Environment.NewLine + "Based on " + fights + " fights";
            }
        }

        private object[] calculateWinRatio(HashSet<Deck> decksToExamine, CardStats card)
        {
            int fights = 0;
            int wins = 0;

            foreach (var deck in decksToExamine)
            {
                var re = deck.results;
                foreach (var res in re)
                {
                    if (res.winnerDeck == deck)
                    {
                        if (res.winner.Equals("P2"))
                        {
                            if (res.p2CardPlaySequence.Contains(card.card) && res.p2CardPlaySequence.Contains(thisCard.card))
                            {
                                wins++;
                                fights++;
                            }
                        }
                        else
                        {
                            if (res.p1CardPlaySequence.Contains(card.card) && res.p1CardPlaySequence.Contains(thisCard.card))
                            {
                                wins++;
                                fights++;
                            }
                        }
                    }
                    else
                    {
                        if (res.loser.Equals("P2"))
                        {
                            if (res.p2CardPlaySequence.Contains(card.card) && res.p2CardPlaySequence.Contains(thisCard.card))
                            {
                                //wins++;
                                fights++;
                            }
                        }
                        else
                        {
                            if (res.p1CardPlaySequence.Contains(card.card) && res.p1CardPlaySequence.Contains(thisCard.card))
                            {
                                //wins++;
                                fights++;
                            }
                        }
                    }
                }
            }

            var winRatioWithOtherCard = 1.0;

            if (wins == 0 && fights == 0)
            {
                //NameBlock_Copy1.Text = "Never got to play together";
                winRatioWithOtherCard = -1.0;
            }
            else if (wins == 0)
            {
                //NameBlock_Copy1.Text = "won " + 0 + " %" + Environment.NewLine + "Based on " + fights + " fights";
                winRatioWithOtherCard = 0.0;
            }
            else
            {
                winRatioWithOtherCard = Math.Round((1.0 * wins) / (1.0 * fights) * 100.0, 2);

                //NameBlock_Copy1.Text = "won " + winRatioWithOtherCard + " %" + Environment.NewLine + "Based on " + fights + " fights";
            }

            return new object[] { fights, wins, winRatioWithOtherCard };

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e){

            thisCard.card.setCost(Int32.Parse(CostBox.Text));
            thisCard.card.setAttack(Int32.Parse(AttackBox.Text));
            thisCard.card.setHealth(Int32.Parse(HealthBox.Text));
            thisCard.card.SetHasTaunt(checkBox_Common_Copy.IsChecked ?? false);
            thisCard.card.SetHasCharge(checkBox_Rare_Copy.IsChecked ?? false);

            if (checkBox_Common.IsChecked ?? false) { thisCard.card.setRarity("common"); }
            else if (checkBox_Rare.IsChecked ?? false) { thisCard.card.setRarity("rare"); }
            else { thisCard.card.setRarity("epic"); }

            if (!CostBox_Copy.Visibility.Equals(Visibility.Collapsed))
            {
                thisCard.card.setName(CostBox_Copy.Text);
            }

            bool found = false;
            foreach (var card in cont.simulationCriteria.setup.Cardpool)
            {
                if (card.GetNameType().Equals(thisCard.card.GetNameType())){
                    found = true;

                    var setupPool = cont.simulationCriteria.setup.Cardpool;
                    setupPool.Remove(card);
                    setupPool.Add(thisCard.card);
                    cont.simulationCriteria.setup.Cardpool = setupPool;
                    
                    break;
                }
            }

            if (!found){
                cont.simulationCriteria.setup.Cardpool.Add(thisCard.card);
            }

            var res = NewSimulationPage.runSimulation(cont,cont.simulationCriteria.setup);
            foreach(var card in cont.model.cardsToDisplay)
            {
                if(card.card == (thisCard.card))
                {
                    setCardData(card);
                    break;
                }
            }

        }
    }
}