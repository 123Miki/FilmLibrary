using FilmLibrary.Business;
using FilmLibrary.ViewModel;
using System.Windows.Controls;

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
    }
}
