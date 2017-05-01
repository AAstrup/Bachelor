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
using GameEngine;
using System.Collections.ObjectModel;

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

        ICard thisCard;

        public CardExpediton()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var con = e.Parameter as ContainerClass;
            var data = con.getCard() as ICard;
            cont = con;
            
            thisCard = data; //Have a local variable
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

            comboBoxWithCards.ItemsSource = gennereateCollection(data);

        }

        private ObservableCollection<string> gennereateCollection(ICard card) {
            var cards = cont.getModel().getCardsToDisplay();
            ObservableCollection<string> list = new ObservableCollection<string>();
            string name;
            foreach (ICard c in cards){
                name = c.GetNameType();
                if (name.Equals(card.GetNameType())) { }
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
            if ((checkBox_Common.IsChecked ?? false) || (checkBox_Rare.IsChecked ?? false) || (checkBox_Epic.IsChecked ?? false))
            {
                thisCard.setCost(Int32.Parse(CostBox.Text));
                thisCard.setAttack(Int32.Parse(AttackBox.Text));
                thisCard.setHealth(Int32.Parse(HealthBox.Text));

                if (checkBox_Common.IsChecked ?? false) { thisCard.setRarity("common"); }
                else if (checkBox_Rare.IsChecked ?? false) { thisCard.setRarity("rare"); }
                else { thisCard.setRarity("epic"); } 
                
                cont.setcard(thisCard);

                this.Frame.Navigate((typeof(MainPage)), cont);
            }
            else{
                ALARM_BLOK.Text = "YOU CAN NOT PROCEDE WIITHOUT A RARITY";
            }
        }

        private void reset_Click(object sender, RoutedEventArgs e){
            NameBlock.Text = thisCard.GetNameType();
            CostBox.Text = thisCard.GetCost().ToString();
            AttackBox.Text = thisCard.GetDamage().ToString();
            HealthBox.Text = thisCard.GetMaxHp().ToString();

            if (thisCard.GetRarity().Equals("common")) { checkBox_Checked_common(null, null); }
            else if (thisCard.GetRarity().Equals("rare")) { checkBox_Checked_rare(null, null); }
            else { checkBox_Checked_epic(null, null); }
            
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e){
            NameBlock_Copy1.Text = "63% Win-ratio"+ Environment.NewLine + "Based on 250 fights";
        }

    }
}
