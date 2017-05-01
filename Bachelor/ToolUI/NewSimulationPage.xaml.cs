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
                l.Content = card.GetNameType();
                l.Foreground = new SolidColorBrush(Colors.Black);
                l.IsEnabled = true;
                l.SelectedOrNot = true;
                cardsToPresent.Add(l);
            }

            cardsBox.ItemsSource = cardsToPresent;

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
                if (sellected) { item.Foreground = new SolidColorBrush(Colors.DarkGray); item.SelectedOrNot = false; }
                else { item.Foreground = new SolidColorBrush(Colors.Black); item.SelectedOrNot = true; }
            }
        }


    }
}
