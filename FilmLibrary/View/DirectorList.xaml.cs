using FilmLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilmLibrary.View
{
    /// <summary>
    /// Logique d'interaction pour DirectorList.xaml
    /// </summary>
    public partial class DirectorList : UserControl
    {
        DirectorViewModel ViewModel = new DirectorViewModel();

        public DirectorList()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.CheckCanDeleteDirector();

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteDirector();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddButton.IsEnabled = false;
            ViewModel.IsReadingMode = false;
        }
    }
}
