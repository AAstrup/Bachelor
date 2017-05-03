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

        public CardExpediton()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var con = e.Parameter as ContainerClass;
            cont = con;
            if (!(con.getCard() == null))
            {
                var cardStat = con.getCard() as CardStats;
                var data = cardStat.card;
                cont = con;

                thisCard = cardStat; //Have a local variable
                NameBlock.Text = data.GetNameType();
                NameBlock_Copy.Text = data.GetNameType();
                CostBox.Text = data.GetCost().ToString();
                AttackBox.Text = data.GetDamage().ToString();
                HealthBox.Text = data.GetCost().ToString();
                HealthBox.Text = data.GetMaxHp().ToString();
                if (data.GetRarity().Equals("common")) { checkBox_Common.IsChecked = true; }
                else if (data.GetRarity().Equals("rare")) { checkBox_Rare.IsChecked = true; }
                else { checkBox_Rare.IsChecked = true; }
                NameBlock_Copy1.Text = "  %";

                textBlock_Copy6.Text = cardStat.rank; //RANK
                if (cardStat.win_ratio == -1){
                    textBlock_Copy6.Text = "?"; //RANK
                    textBlock_Copy10.Text = "UNKNOWN"; textBlock_Copy10.FontSize = 25;
                    textBlock_Copy8.Text = "UNKNOWN"; textBlock_Copy8.FontSize = 25;
                }
                else{
                    textBlock_Copy10.Text = cardStat.win_ratio + " %"; //WIN-RATIO
                    textBlock_Copy8.Text = "" + cardStat.domminance; //DOMMINANCE
                }

                cardStat.card = data;
                comboBoxWithCards.ItemsSource = gennereateCollection(cardStat);
            }
            else
            {
                CostBox_Copy.Visibility = Visibility.Visible;
                NameBlock.Visibility = Visibility.Collapsed;
                thisCard = new CardStats(new Card_User_Defined());
                textBlock_Copy10.Text = "UNKNOWN"; textBlock_Copy10.FontSize = 25;
                textBlock_Copy8.Text = "UNKNOWN"; textBlock_Copy8.FontSize = 25;
                textBlock_Copy6.Text = "?"; //RANK
                thisCard.card.setName("NNNNNNNNNNNNAAAAAMMMMMMMME");
                comboBoxWithCards.ItemsSource = gennereateCollection(thisCard);
            }
            

        }

        private ObservableCollection<string> gennereateCollection(CardStats card) {
            var cards = cont.getModel().getCardsToDisplay();
            ObservableCollection<string> list = new ObservableCollection<string>();
            string name;
            foreach (CardStats c in cards){
                name = c.card.GetNameType();
                if (name.Equals(card.card.GetNameType())) { }
                else { list.Add(name); }
            }

            return list;
        }

        private void checkBox_Checked_common(object sender, RoutedEventArgs e){
            checkBox_Common.IsChecked = true;
            checkBox_Rare.IsChecked = false;
            checkBox_Epic.IsChecked = false;
        }

        private void checkBox_Checked_rare(object sender, RoutedEventArgs e){
            checkBox_Rare.IsChecked = true;
            checkBox_Common.IsChecked = false;
            checkBox_Epic.IsChecked = false;
        }

        private void checkBox_Checked_epic(object sender, RoutedEventArgs e){
            checkBox_Epic.IsChecked = true;
            checkBox_Common.IsChecked = false;
            checkBox_Rare.IsChecked = false;
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            if ((checkBox_Common.IsChecked ?? false) || (checkBox_Rare.IsChecked ?? false) || (checkBox_Epic.IsChecked ?? false) 
                && (!CostBox_Copy.Text.Equals("NAME") || CostBox_Copy.Visibility.Equals(Visibility.Collapsed))
                && (!CostBox.Text.Equals("COST"))
                && (!AttackBox.Text.Equals("ATTACK")) 
                && (!HealthBox.Text.Equals("HEALTH")))
            {
                thisCard.card.setCost(Int32.Parse(CostBox.Text));
                thisCard.card.setAttack(Int32.Parse(AttackBox.Text));
                thisCard.card.setHealth(Int32.Parse(HealthBox.Text));

                if (checkBox_Common.IsChecked ?? false) { thisCard.card.setRarity("common"); }
                else if (checkBox_Rare.IsChecked ?? false) { thisCard.card.setRarity("rare"); }
                else { thisCard.card.setRarity("epic"); }

                thisCard.changed = true;
                cont.setcard(thisCard);
                if (!CostBox_Copy.Visibility.Equals(Visibility.Collapsed)) {
                    thisCard.card.setName(CostBox_Copy.Text);
                    cont.getModel().cardsToDisplay.Add(thisCard);
                }
                

                this.Frame.Navigate((typeof(MainPage)), cont);
            }
            else{

            }
        }

        private void reset_Click(object sender, RoutedEventArgs e){
            NameBlock.Text = thisCard.card.GetNameType();
            CostBox.Text = thisCard.card.GetCost().ToString();
            AttackBox.Text = thisCard.card.GetDamage().ToString();
            HealthBox.Text = thisCard.card.GetMaxHp().ToString();

            if (thisCard.card.GetRarity().Equals("common")) { checkBox_Checked_common(null, null); }
            else if (thisCard.card.GetRarity().Equals("rare")) { checkBox_Checked_rare(null, null); }
            else { checkBox_Checked_epic(null, null); }
            
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e){
            NameBlock_Copy1.Text = "63% Win-ratio"+ Environment.NewLine + "Based on 250 fights";
        }

    }
}
