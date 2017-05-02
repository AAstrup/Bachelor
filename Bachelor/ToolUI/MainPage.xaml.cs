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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ToolUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 


    public sealed partial class MainPage : Page
    {
        public List<Button> SRank { get; set; }
        public List<Button> SRank1 { get; set; }
        public List<Button> SRank2 { get; set; }
        public List<Button> SRank3 { get; set; }

        public List<Button> ARank { get; set; }
        public List<Button> ARank1 { get; set; }
        public List<Button> ARank2 { get; set; }
        public List<Button> ARank3 { get; set; }

        public List<Button> BRank { get; set; }
        public List<Button> BRank1 { get; set; }
        public List<Button> BRank2 { get; set; }
        public List<Button> BRank3 { get; set; }

        public List<Button> CRank { get; set; }
        public List<Button> CRank1 { get; set; }
        public List<Button> CRank2 { get; set; }
        public List<Button> CRank3 { get; set; }

        public List<Button> FRank { get; set; }
        public List<Button> FRank1 { get; set; }
        public List<Button> FRank2 { get; set; }
        public List<Button> FRank3 { get; set; }

        Model model;

        //public List<CardStats> cardsToDisplay { get; set; }

        private void listClear(){
            SRank.Clear();
            SRank1.Clear();
            SRank2.Clear();
            SRank3.Clear();

            ARank.Clear();
            ARank1.Clear();
            ARank2.Clear();
            ARank3.Clear();

            BRank.Clear();
            BRank1.Clear();
            BRank2.Clear();
            BRank3.Clear();

            CRank.Clear();
            CRank1.Clear();
            CRank2.Clear();
            CRank3.Clear();

            FRank.Clear();
            FRank1.Clear();
            FRank2.Clear();
            FRank3.Clear();

        }

        private void listInitializer()
        {
            SRank = new List<Button>();
            SRank1 = new List<Button>();
            SRank2 = new List<Button>();
            SRank3 = new List<Button>();

            ARank = new List<Button>();
            ARank1 = new List<Button>();
            ARank2 = new List<Button>();
            ARank3 = new List<Button>();

            BRank = new List<Button>();
            BRank1 = new List<Button>();
            BRank2 = new List<Button>();
            BRank3 = new List<Button>();

            CRank = new List<Button>();
            CRank1 = new List<Button>();
            CRank2 = new List<Button>();
            CRank3 = new List<Button>();

            FRank = new List<Button>();
            FRank1 = new List<Button>();
            FRank2 = new List<Button>();
            FRank3 = new List<Button>();
        }

        private void addToList(List<Button> firstList, List<Button> secondList, List<Button> thridList, List<Button> fourthList, Button newButton){

            if (firstList.Count > 2){
                if (secondList.Count > 2) {
                    if (thridList.Count > 2){
                        fourthList.Add(newButton);
                    }
                    else { thridList.Add(newButton); }
                }
                else { secondList.Add(newButton); }
            }
            else
            { firstList.Add(newButton); }
        }

        public MainPage()
        {
            if(model == null){ model = new Model(); }

            this.DataContext = this;

            listInitializer();

            addCardsToCollection(model.getCardsToDisplay());

            this.InitializeComponent();
        }

        private void addCardsToCollection(List<CardStats> cards) {
            //SRank.Add(new Button() { Height = 50, Width = 150, Name = "Help3" });
            int i = 50;

            Button b;
            string cont;
            int fontS = 12;
            foreach (var car in cards)
            {
                var card = car.card;
                cont = card.GetNameType() + "(" + card.GetCost() + "," + card.GetDamage() + "," + card.GetMaxHp() + ")";
                if(cont.Length < 10) { fontS = 14; }
                else if (cont.Length < 13) { fontS = 13; }
                else { fontS = 12; }
                b = new Button() { Height = 30, Width = 150, Name = ("" + i), Content = cont,FontSize = fontS };
                b.Foreground = model.colorFromRarity(card.GetRarity());
                b.FontWeight = model.boldOrNot(car.changed);
                b.Background = model.simulatedOrNot(car.simulated);

                b.Click += button_Click;

                spreadOutCards(car, b);

                i += 50;
            }
        }

        private void spreadOutCards(CardStats card, Button b){
            if (!card.simulated){ //IF not simulated, place it in B-Rank
                addToList(BRank, BRank1, BRank2, BRank3, b);
            }
            else{
                if(card.win_ratio < 21) { addToList(FRank, FRank1, FRank2, FRank3, b); }
                else if(card.win_ratio > 20 && card.win_ratio < 39) { addToList(BRank, CRank1, CRank2, CRank3, b); }
                else if (card.win_ratio > 39 && card.win_ratio < 60) { addToList(BRank, BRank1, BRank2, BRank3, b); }
                else if (card.win_ratio > 60 && card.win_ratio < 81) { addToList(ARank, ARank1, ARank2, ARank3, b); }
                else if (card.win_ratio > 71) { addToList(SRank, SRank1, SRank2, SRank3, b); }

                
            }
            
        }

        private void button_Click(object sender, RoutedEventArgs e){

            var b = sender as Button;

            var s = b.Content.ToString();

            s = s.Substring(0, s.Length - 7);
            //b.Content.ToString().Substring(0, b.Content.ToString().Length - 6);

            b.Content = s;

            int i = 0;
            var cards = model.getCardsToDisplay();
            var card = cards[i];
            while (i < cards.Count){
                card = cards[i];
                if (card.card.GetNameType().Equals(s)) { break; }
                i++;
            }
            ContainerClass con = new ContainerClass(model, card);
            this.Frame.Navigate((typeof(CardExpediton)), con);

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(e.Parameter is ContainerClass)
            {
                var database = e.Parameter as ContainerClass;
                model = database.getModel();
                var cards = model.getCardsToDisplay();

                if (database.simulated) {
                    listClear();
                    addCardsToCollection(cards);
                    model.setCardsToDisplay(cards);
                    database.simulated = false;
                }
                else {
                    int i = 0;
                    var data = database.getCard() as CardStats;
                    string s = data.card.GetNameType();

                    var card = cards[i];
                    while (i < cards.Count)
                    {
                        card = cards[i];
                        if (card.card.GetNameType().Equals(s))
                        {
                            cards.Remove(card);
                            cards.Add(data);
                            listClear();
                            addCardsToCollection(cards);
                            model.setCardsToDisplay(cards);
                            break;
                        }
                        i++;
                    }
                } 

            }

        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate((typeof(NewSimulationPage)), new ContainerClass(model,null));
        }

        private void create_button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate((typeof(CardExpediton)), new ContainerClass(model, null));
        }

        private void button_Quetion(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate((typeof(QuestionPage)), null);
        }
    }
}

/*
            Button b = new Button() { Height = 50, Width = 500, Name = "Help2" };

            b.Click += button_Click;


            addToList(ARank, ARank1, ARank2, b);

            ARank.Add(b);

            b = new Button() { Height = 50, Width = 200, Name = "Help1" };

            b.Click += button_Click;

            addToList(ARank, ARank1, ARank2, b);
            */
