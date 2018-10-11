using System.Windows;

namespace FilmLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
            GoToAccueil();
        }

        private void GoToAccueil()
        {
            Accueil accueil = new Accueil();
            accueil.Show();
            this.Close();
        }
    }
}
