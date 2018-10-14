using FilmLibrary.Business;
using FilmLibrary.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FilmLibrary.ViewModel
{
    public class FilmViewModel : MvvmPropertyChanged
    {
        private ObservableCollection<Film> _films;
        private FilmService _filmService;
        private bool _canDeleteFilm;

        public ObservableCollection<Film> Films
        {
            get { return _films; }
            set
            {
                if (value != _films)
                {
                    this._films = value;
                    RaisePropertyChanged();
                }
            }
        }
        public Film CurrentFilm { get; set; }
        public bool CanDeleteFilm
        {
            get { return _canDeleteFilm; }
            set
            {
                if (value != _canDeleteFilm)
                {
                    this._canDeleteFilm = value;
                    RaisePropertyChanged();
                }
            }
        }

        public FilmViewModel()
        {
            _filmService = new FilmService();
            InitList();
            CanDeleteFilm = false;
            CurrentFilm = new Film();
            CurrentFilm.RegisterPropertyChanged(Film_PropertyChanged);
        }

        private void Film_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CheckCanDeleteFilm();
        }

        public void DeleteFilm()
        {
            if (CanDeleteFilm)
            {
                _filmService.DeleteFilm(CurrentFilm);
                InitList();
            }
        }

        private void InitList()
        {
            Films = new ObservableCollection<Film>(_filmService.GetFilms());
        }

        public void CheckCanDeleteFilm()
        {
            if(CurrentFilm != null && CurrentFilm.FilmId != null)
            {
                CanDeleteFilm = true;
            }
            else
            {
                CanDeleteFilm = false;
            }
        }
    }
}
