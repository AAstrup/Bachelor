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
using Tool;
using AI;
using Bachelor;
using GameEngine;
using static ToolUI.ContainerClass;

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

            EqualShareBox.IsEnabled = false;

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

            if(con.rankCriteria == null)
            {
                AND_S.IsChecked = true;
                AND_S_Copy.IsChecked = true;
                AND_S_Copy1.IsChecked = true;
                AND_S_Copy2.IsChecked = true;
                AND_S_Copy3.IsChecked = true;

                Win_box_S.IsChecked = true;
                Win_box_S_Copy.IsChecked = true;
                Win_box_S_Copy1.IsChecked = true;
                Win_box_S_Copy2.IsChecked = true;
                Win_box_S_Copy3.IsChecked = true;

                Dom_box_S.IsChecked = true;
                Dom_box_S_Copy.IsChecked = true;
                Dom_box_S_Copy1.IsChecked = true;
                Dom_box_S_Copy2.IsChecked = true;
                Dom_box_S_Copy3.IsChecked = true;
            }
            else
            {
                var rankCriteria = con.rankCriteria;
                AND_S.IsChecked = rankCriteria.ands[0];
                AND_S_Copy.IsChecked = rankCriteria.ands[1];
                AND_S_Copy1.IsChecked = rankCriteria.ands[2];
                AND_S_Copy2.IsChecked = rankCriteria.ands[3];
                AND_S_Copy3.IsChecked = rankCriteria.ands[4];

                OR_S.IsChecked = rankCriteria.ors[0];
                OR_S_Copy.IsChecked = rankCriteria.ors[1];
                OR_S_Copy1.IsChecked = rankCriteria.ors[2];
                OR_S_Copy2.IsChecked = rankCriteria.ors[3];
                OR_S_Copy3.IsChecked = rankCriteria.ors[4];

                Win_box_S.IsChecked = rankCriteria.winRatio_t[0];
                Win_box_S_Copy.IsChecked = rankCriteria.winRatio_t[1];
                Win_box_S_Copy1.IsChecked = rankCriteria.winRatio_t[2];
                Win_box_S_Copy2.IsChecked = rankCriteria.winRatio_t[3];
                Win_box_S_Copy3.IsChecked = rankCriteria.winRatio_t[4];

                Dom_box_S.IsChecked = rankCriteria.domminance_t[0];
                Dom_box_S_Copy.IsChecked = rankCriteria.domminance_t[1];
                Dom_box_S_Copy1.IsChecked = rankCriteria.domminance_t[2];
                Dom_box_S_Copy2.IsChecked = rankCriteria.domminance_t[3];
                Dom_box_S_Copy3.IsChecked = rankCriteria.domminance_t[4];
            }

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

        private void Run_button_Click(object sender, RoutedEventArgs e) {
            var cards = con.getModel().cardsToDisplay;
            List<ICard> ICards = new List<ICard>();
            List<ICard> ICardsNOT = new List<ICard>();

            foreach (var card in cardsToPresent) {
                card.card.simulated = false;
                if (card.SelectedOrNot) {
                    ICards.Add(card.card.card);
                } else {
                    ICardsNOT.Add(card.card.card);
                }
            }

            //Run new simulation with criteria

            SetupData setup = SetupData.GetDefault();

            setup.MaxDuplicates = 1;
            setup.DeckSize = 3;
            setup.Cardpool = ICards;
            setup.StartCards = 5;
            setup.GamesEachDeckMustPlayMultiplier = 2;
            setup.DeckFactory = DeckFactoryType.Unique;


            //setup.DeckFactory = DeckFactoryType.Random;
            //setup.AmountOfDecksToGenerate = 100;

            var Res = Simulator.RunSimulation(setup);

            //Get results back

            List<CardStats> ListWithCards = new List<CardStats>();

            Random r = new Random();

            //var winRatios = con.model.calculateWinRatio(ICards, Res.Decks);

            foreach (var card in Res.Cardpool) {
                var cardStats = new CardStats(card);

                //var wins = ((winRatios[card.GetNameType()])[0] * 1.0);
                //var fights = ((winRatios[card.GetNameType()])[1] * 1.0);

                var win_ratio = (card as ITrackable).GetWinLossRate(); //(wins / fights) * 100;//(card as ITrackable).GetWinLossRate();

                var win_ratio_int = Convert.ToInt32(win_ratio);

                cardStats.win_ratio = win_ratio_int;

                //cardStats.win_ratio = r.Next(0,100); //Random
                var dom = (card as ITrackable).getDominance(card);

                cardStats.domminance = dom; //Random
                cardStats.simulated = true;
                cardStats.changed = false;
                ListWithCards.Add(cardStats);
            }
            foreach (var card in ICardsNOT) {
                var cardStats = new CardStats(card);

                cardStats.win_ratio = -1;
                cardStats.domminance = -1;
                cardStats.simulated = false;
                cardStats.changed = false;
                ListWithCards.Add(cardStats);
            }

            con.getModel().setCardsToDisplay(ListWithCards);
            con.simulated = true;

            var rankCriteria = new RankCriteria();

            rankCriteria.ands[0] = AND_S.IsChecked ?? false;
            rankCriteria.ands[1] = AND_S_Copy.IsChecked ?? false;
            rankCriteria.ands[2] = AND_S_Copy1.IsChecked ?? false;
            rankCriteria.ands[3] = AND_S_Copy2.IsChecked ?? false;
            rankCriteria.ands[4] = AND_S_Copy3.IsChecked ?? false;

            rankCriteria.ors[0] = OR_S.IsChecked ?? false;
            rankCriteria.ors[1] = OR_S_Copy.IsChecked ?? false;
            rankCriteria.ors[2] = OR_S_Copy1.IsChecked ?? false;
            rankCriteria.ors[3] = OR_S_Copy2.IsChecked ?? false;
            rankCriteria.ors[4] = OR_S_Copy3.IsChecked ?? false;

            rankCriteria.winRatio_t[0] = Win_box_S.IsChecked ?? false;
            rankCriteria.winRatio_t[1] = Win_box_S_Copy.IsChecked ?? false;
            rankCriteria.winRatio_t[2] = Win_box_S_Copy1.IsChecked ?? false;
            rankCriteria.winRatio_t[3] = Win_box_S_Copy2.IsChecked ?? false;
            rankCriteria.winRatio_t[4] = Win_box_S_Copy3.IsChecked ?? false;

            rankCriteria.domminance_t[0] = Dom_box_S.IsChecked ?? false;
            rankCriteria.domminance_t[1] = Dom_box_S_Copy.IsChecked ?? false;
            rankCriteria.domminance_t[2] = Dom_box_S_Copy1.IsChecked ?? false;
            rankCriteria.domminance_t[3] = Dom_box_S_Copy2.IsChecked ?? false;
            rankCriteria.domminance_t[4] = Dom_box_S_Copy3.IsChecked ?? false;


            Win_S.Text = Win_S.Text.Replace('%',' ');
            Win_S_Copy.Text = Win_S_Copy.Text.Replace('%', ' ');
            Win_S_Copy1.Text = Win_S_Copy1.Text.Replace('%', ' ');
            Win_S_Copy2.Text = Win_S_Copy2.Text.Replace('%', ' ');
            Win_S_Copy3.Text = Win_S_Copy3.Text.Replace('%', ' ');

            rankCriteria.winRatio[0] = Int32.Parse(Win_S.Text);
            rankCriteria.winRatio[1] = Int32.Parse(Win_S_Copy.Text);
            rankCriteria.winRatio[2] = Int32.Parse(Win_S_Copy1.Text);
            rankCriteria.winRatio[3] = Int32.Parse(Win_S_Copy2.Text);
            rankCriteria.winRatio[4] = Int32.Parse(Win_S_Copy3.Text);

            rankCriteria.domminance[0] = Int32.Parse(Dom_S.Text);
            rankCriteria.domminance[1] = Int32.Parse(Dom_S_Copy.Text);
            rankCriteria.domminance[2] = Int32.Parse(Dom_S_Copy1.Text);
            rankCriteria.domminance[3] = Int32.Parse(Dom_S_Copy2.Text);
            rankCriteria.domminance[4] = Int32.Parse(Dom_S_Copy3.Text);

            con.rankCriteria = rankCriteria;


            this.Frame.Navigate((typeof(MainPage)), con);
        }
    }
}
