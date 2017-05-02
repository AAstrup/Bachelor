using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ToolUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewSimulationPage : Page
    {
        ContainerClass con;
        int cardsToExamine;
        ObservableCollection<listBoxExtra> cardsToPresent;

        public NewSimulationPage()
        {
            
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e){
            con = e.Parameter as ContainerClass;
            var cards = con.getModel().getCardsToDisplay();

            cardsToPresent = new ObservableCollection<listBoxExtra>();

            listBoxExtra l;

            foreach (var card in cards){
                l = new listBoxExtra();
                l.Height = 45;
                l.Content = card.card.GetNameType();
                l.Foreground = new SolidColorBrush(Colors.Black);
                l.IsEnabled = true;
                l.SelectedOrNot = true;
                l.card = card;
                cardsToPresent.Add(l);
            }

            cardsBox.ItemsSource = cardsToPresent;
            cardsToExamine = cardsToPresent.Count();
            textBlock1_Copy13.Text = "Cards to examine: " +cardsToExamine;
            checkBox.IsChecked = true;
            EqualShareBox.IsChecked = true;
        }

        public class listBoxExtra : ListBoxItem
        {
           public bool SelectedOrNot { get; set; }
           public CardStats card { get; set; }
        }

        private void cardsBox_SelectionChanged(object sender, SelectionChangedEventArgs e){
            var obj = e.AddedItems;

            if(obj.Count > 0)
            {
                var selectedItem = obj[0];
                var item = selectedItem as listBoxExtra;

                var sellected = item.SelectedOrNot;
                if (sellected) { item.Foreground = new SolidColorBrush(Colors.DarkGray); item.Background = new SolidColorBrush(Colors.Gray); item.SelectedOrNot = false; cardsToExamine--; }
                else { item.Foreground = new SolidColorBrush(Colors.Black); item.Background = new SolidColorBrush(Colors.White); item.SelectedOrNot = true; cardsToExamine++; }

                textBlock1_Copy13.Text = "Cards to examine: " + cardsToExamine;
                timeEstimator();
            }
        }

        private void timeEstimator(){
            var time = 0.0;
            var fightsPerSecond = 200.0;
            if (checkBox.IsChecked ?? false)
            {
                time = ((cardsToExamine * 252)/ fightsPerSecond) * Int32.Parse(textBox_Copy3.Text);

            }
            else
            {
                var NumberOfDecks = Int32.Parse(textBox.Text);
                time = ((cardsToExamine * NumberOfDecks) / fightsPerSecond) * Int32.Parse(textBox_Copy3.Text);
                time = Math.Round(time, 2);
            }
            if(time > 60.0) { time = Math.Round((time / 60.0), 2) ; TimeEstationText.Text = "Time estimation: " + time + "minuts"; }
            else { TimeEstationText.Text = "Time estimation: " + time + "seconds"; }
            
        }

        private void EqualShareBox_UnChecked(object sender, RoutedEventArgs e)
        {
            textBox_Copy.IsEnabled = true;
            textBox_Copy1.IsEnabled = true;
            textBox_Copy2.IsEnabled = true;

            textBlock1_Copy.Foreground = new SolidColorBrush(Colors.Black);
            textBlock1_Copy3.Foreground = new SolidColorBrush(Colors.Black);
            textBlock1_Copy4.Foreground = new SolidColorBrush(Colors.Black);
            textBlock1_Copy5.Foreground = new SolidColorBrush(Colors.Black);
            textBlock1_Copy8.Foreground = new SolidColorBrush(Colors.Black);

            textBlock1_Copy1.Foreground = new SolidColorBrush(Colors.Black);
            textBlock1_Copy9.Foreground = new SolidColorBrush(Colors.Black);
            textBlock1_Copy10.Foreground = new SolidColorBrush(Colors.Black);

            textBlock1_Copy2.Foreground = new SolidColorBrush(Colors.Black);

            timeEstimator();
        }

        private void EqualShareBox_Checked(object sender, RoutedEventArgs e)
        {
            textBox_Copy.IsEnabled = false;
            textBox_Copy1.IsEnabled = false;
            textBox_Copy2.IsEnabled = false;

            textBlock1_Copy.Foreground = new SolidColorBrush(Colors.DarkGray);
            textBlock1_Copy3.Foreground = new SolidColorBrush(Colors.DarkGray);
            textBlock1_Copy4.Foreground = new SolidColorBrush(Colors.DarkGray);
            textBlock1_Copy5.Foreground = new SolidColorBrush(Colors.DarkGray);
            textBlock1_Copy8.Foreground = new SolidColorBrush(Colors.DarkGray);

            textBlock1_Copy1.Foreground = new SolidColorBrush(Colors.DarkGray);
            textBlock1_Copy9.Foreground = new SolidColorBrush(Colors.DarkGray);
            textBlock1_Copy10.Foreground = new SolidColorBrush(Colors.DarkGray);

            textBlock1_Copy2.Foreground = new SolidColorBrush(Colors.DarkGray);

            timeEstimator();
        }

        private void textBox_Copy3_TextChanged(object sender, TextChangedEventArgs e){
            timeEstimator();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e) {
            timeEstimator();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e){
            textBlock.Foreground = new SolidColorBrush(Colors.DarkGray);
            textBlock_Copy.Foreground = new SolidColorBrush(Colors.DarkGray);

            textBox.IsEnabled = false;

            timeEstimator();
        }

        private void checkBox_UnChecked(object sender, RoutedEventArgs e)
        {
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock_Copy.Foreground = new SolidColorBrush(Colors.Black);
            textBox.IsEnabled = true;

            timeEstimator();
        }

        private void AND_OR_Checked(CheckBox c1, CheckBox c2) { c1.IsChecked = true; c2.IsChecked = false; }

        private void AND_S_Checked(object sender, RoutedEventArgs e){
            AND_OR_Checked(AND_S, OR_S);
        }

        private void AND_A_Checked(object sender, RoutedEventArgs e){
            AND_OR_Checked(AND_S_Copy, OR_S_Copy);
        }

        private void AND_B_Checked(object sender, RoutedEventArgs e){
            AND_OR_Checked(AND_S_Copy1, OR_S_Copy1);
        }

        private void AND_C_Checked(object sender, RoutedEventArgs e){
            AND_OR_Checked(AND_S_Copy2, OR_S_Copy2);
        }

        private void AND_F_Checked(object sender, RoutedEventArgs e){
            AND_OR_Checked(AND_S_Copy3, OR_S_Copy3);
        }

        private void OR_S_Checked(object sender, RoutedEventArgs e)
        {
            AND_OR_Checked(OR_S, AND_S);
        }

        private void OR_A_Checked(object sender, RoutedEventArgs e)
        {
            AND_OR_Checked(OR_S_Copy, AND_S_Copy);
        }

        private void OR_B_Checked(object sender, RoutedEventArgs e)
        {
            AND_OR_Checked(OR_S_Copy1, AND_S_Copy1);
        }

        private void OR_C_Checked(object sender, RoutedEventArgs e)
        {
            AND_OR_Checked(OR_S_Copy2, AND_S_Copy2);
        }

        private void OR_F_Checked(object sender, RoutedEventArgs e)
        {
            AND_OR_Checked(OR_S_Copy3, AND_S_Copy3);
        }

        private void Run_button_Click(object sender, RoutedEventArgs e){
            var cards = con.getModel().cardsToDisplay;
            foreach(var card in cards) {
                card.simulated = false;
            }

            //Run new simulation with criteria

            //Get results back

            List<CardStats> ListWithCards = new List<CardStats>();

            Random r = new Random();

            foreach(var ca in cardsToPresent){
                var card = ca.card;
                card.win_ratio = r.Next(0,100); //Random
                card.domminance = r.Next(0, 6); //Random
                card.simulated = false;
                if (ca.SelectedOrNot){
                    card.simulated = true;
                }
                card.changed = false;
                ListWithCards.Add(card);
            }

            con.getModel().setCardsToDisplay(ListWithCards);
            con.simulated = true;



            this.Frame.Navigate((typeof(MainPage)), con);
        }
    }
}
