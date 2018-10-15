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
    public partial class FilmList : UserControl
    {
        FilmViewModel ViewModel = new FilmViewModel();

        public FilmList()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.CheckCanDeleteFilm();

        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteFilm();
        }

        private void _calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            _calendar.DisplayMode = CalendarMode.Year;
        }

        private void _calendar_Loaded(object sender, RoutedEventArgs e)
        {
            _calendar.DisplayMode = CalendarMode.Year;
        }
    }
}
