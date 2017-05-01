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

            }
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

        }

        private void checkBox_Checked(object sender, RoutedEventArgs e){
            textBlock.Foreground = new SolidColorBrush(Colors.DarkGray);
            textBlock_Copy.Foreground = new SolidColorBrush(Colors.DarkGray);

            textBox.IsEnabled = false;
        }

        private void checkBox_UnChecked(object sender, RoutedEventArgs e)
        {
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            textBlock_Copy.Foreground = new SolidColorBrush(Colors.Black);
            textBox.IsEnabled = true;
        }

    }
}
