using FilmLibrary.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace FilmLibrary
{
    /// <summary>
    /// Interaction logic for Accueil.xaml
    /// </summary>
    public partial class Accueil : Window
    {
        AccueilViewModel ViewModel = new AccueilViewModel();

        public Accueil()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ListViewMenu.SelectedItem = ListViewMenu.Items[0];
            ViewModel.CurrentMenuSelection = FilmItem.Name;
        }
        
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            //ButtonCloseMenu.Visibility = Visibility.Visible;
            //ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            //ButtonCloseMenu.Visibility = Visibility.Collapsed;
            //ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ButtonPowerOff_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.CurrentMenuSelection = (ListViewMenu.SelectedItem as ListViewItem).Name;
        }
    }
}
