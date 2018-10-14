using FilmLibrary.Business;
using FilmLibrary.Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace FilmLibrary.ViewModel
{
    public class DirectorViewModel : MvvmPropertyChanged
    {
        private ObservableCollection<Director> _directors;
        private DirectorService _directorService;
        private bool _isReadingMode;
        private FilmService _filmService;
        private bool _canDeleteDirector;

        public ObservableCollection<Director> Directors
        {
            get { return _directors; }
            set
            {
                if (value != _directors)
                {
                    this._directors = value;
                    RaisePropertyChanged();
                }
            }
        }
        public Director CurrentDirector { get; set; }
        public bool CanDeleteDirector
        {
            get { return _canDeleteDirector; }
            set
            {
                if (value != _canDeleteDirector)
                {
                    this._canDeleteDirector = value;
                    RaisePropertyChanged();
                }
            }
        }
        public bool IsReadingMode
        {
            get
            {
                return _isReadingMode;
            }

            set
            {
                if (value != _isReadingMode)
                {
                    this._isReadingMode = value;
                    RaisePropertyChanged();
                }
            }
        }

        public DirectorViewModel()
        {
            _directorService = new DirectorService();
            _filmService = new FilmService();
            InitList();
            CanDeleteDirector = false;
            IsReadingMode = true;
            CurrentDirector = new Director();
            CurrentDirector.RegisterPropertyChanged(Director_PropertyChanged);
        }

        private void Director_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CheckCanDeleteDirector();
        }

        public void DeleteDirector()
        {
            if (CanDeleteDirector)
            {
                _directorService.DeleteDirector(CurrentDirector);
                InitList();
            }
        }

        private void InitList()
        {
            Directors = new ObservableCollection<Director>(_directorService.GetDirectors());
        }

        public void CheckCanDeleteDirector()
        {
            if (CurrentDirector != null && CurrentDirector.DirectorId != null)
            {
                var isDirectorUsed = _filmService.GetFilms().Any(x => x.DirectorId == CurrentDirector.DirectorId);
                if (!isDirectorUsed)
                {
                    CanDeleteDirector = true;
                }
                else
                {
                    CanDeleteDirector = false;
                }
            }
            else
            {
                CanDeleteDirector = false;
            }
        }
    }
}
