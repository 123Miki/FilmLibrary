using FilmLibrary.Business;
using FilmLibrary.Core;
using FilmLibrary.Core.Helpers;
using FilmLibrary.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;


namespace FilmLibrary.ViewModel
{
    public class FilmViewModel : MvvmPropertyChanged
    {
        private ObservableCollection<Film> _films;
        private ObservableCollection<Director> _directors;
        private Film _currentFilm;
        private Film _filmEdit;
        private Director _directorEdit;
        private FilmService _filmService;
        private DirectorService _directorService;
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
        public ObservableCollection<Director> Directors
        {
            get
            {
                return _directors;
            }
            set
            {
                if (value != _directors)

                {
                    this._directors = value;
                    RaisePropertyChanged("Directors");
                }
            }
        }
        public Film CurrentFilm
        {
            get { return _currentFilm; }
            set
            {
                if (value != _currentFilm)
                {
                    this._currentFilm = value;
                    RaisePropertyChanged("CurrentFilm");
                }
            }
        }
        public Film FilmEdit
        {
            get { return _filmEdit; }
            set
            {
                if (value != _filmEdit)
                {
                    this._filmEdit = value;
                    RaisePropertyChanged("FilmEdit");
                }
            }
        }
        public Director DirectorEdit
        {
            get { return _directorEdit; }
            set
            {
                if (value != _directorEdit)
                {
                    this._directorEdit = value;
                    RaisePropertyChanged("DirectorEdit");
                }
            }
        }
        public bool CanDeleteFilm
        {
            get { return _canDeleteFilm; }
            set
            {
                if (value != _canDeleteFilm)
                {
                    this._canDeleteFilm = value;
                    RaisePropertyChanged("CanDeleteFilm");
                }
            }
        }

        public SimpleCommand ValidCommand { get; set; }
        public SimpleCommand AddFilmCommand { get; set; }
        public SimpleCommand DeleteFilmCommand { get; set; }

        #region Méthodes publics

        public FilmViewModel()
        {
            _filmService = new FilmService();
            _directorService = new DirectorService();
            InitListFilm();
            InitListDirector();
            CanDeleteFilm = false;
            InitCurrentFilm();
            InitFilmEdit();
            this.RegisterPropertyChanged(FilmViewModel_PropertyChanged);
            CreateCommands();
        }

        #endregion

        #region Méthodes privées

        private void CheckCanDeleteFilm()
        {
            if (CurrentFilm != null && CurrentFilm.FilmId != null)
            {
                var isFilmExist = _filmService.GetFilms().Any(x => x.FilmId == CurrentFilm.FilmId);
                if (isFilmExist)
                {
                    CanDeleteFilm = true;
                }
                else
                {
                    CanDeleteFilm = false;
                }
            }
            else
            {
                CanDeleteFilm = false;
            }
        }

        private void CreateCommands()
        {
            ValidCommand = new SimpleCommand(() => ValidFilm(), CanValid);
            AddFilmCommand = new SimpleCommand(() => InitFilmEdit());
            DeleteFilmCommand = new SimpleCommand(() => DeleteFilm());
        }

        private void InitCurrentFilm()
        {
            CurrentFilm = new Film();
            CurrentFilm.RegisterPropertyChanged(Film_PropertyChanged);
        }

        private void ValidFilm()
        {
            if (_filmService.SaveOrUpdateFilm(FilmEdit))
            {
                InitListFilm();
                InitFilmEdit();
            }
        }

        private void DeleteFilm()
        {
            if (CanDeleteFilm)
            {
                if (ConfirmationHelper.ConfirmationYesNo("Voulez-vous vraiment supprimer ce élément ?", "Confirmation"))
                {
                    var filmToDelete = Films.Where(x => x.FilmId == FilmEdit.FilmId).FirstOrDefault();
                    if (filmToDelete != null)
                    {
                        if (_filmService.DeleteFilm(filmToDelete))
                        {
                            InitListFilm();
                            filmToDelete.PropertyChanged -= Film_PropertyChanged;
                            InitCurrentFilm();
                            InitFilmEdit();
                        }
                    }
                }
            }
        }

        private void InitFilmEdit()
        {
            FilmEdit = new Film();
            FilmEdit.RegisterPropertyChanged(Film_PropertyChanged);
            FilmEdit.FilmId = Guid.NewGuid();
            ResetDirectorEdit();
        }

        private void ResetDirectorEdit()
        {
            DirectorEdit = null;
        }

        private bool CanValid()
        {
            return FilmEdit != null
                && ValidateHelper.ValidateObject(FilmEdit);
        }

        private void FilmViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(() => CurrentFilm))
            {
                if (CurrentFilm != null && CurrentFilm.FilmId != Guid.Empty)
                {
                    FilmEdit = new Film();
                    FilmEdit.FilmId = CurrentFilm.FilmId;
                    FilmEdit.Name = CurrentFilm.Name;
                    DirectorEdit = Directors.Where(x => x.DirectorId == CurrentFilm.DirectorId).First();
                    RaisePropertyChanged("DirectorEdit");
                    FilmEdit.ReleaseDate = CurrentFilm.ReleaseDate;
                    FilmEdit.Evaluation = CurrentFilm.Evaluation;
                    FilmEdit.RegisterPropertyChanged(Film_PropertyChanged);
                }
            }
            else if (e.PropertyName == this.GetPropertyName(() => DirectorEdit))
            {
                if(DirectorEdit != null)
                {
                    FilmEdit.Director = new Director()
                    {
                        DirectorId = DirectorEdit.DirectorId,
                        Name = DirectorEdit.Name,
                        Firstname = DirectorEdit.Firstname
                    };
                    FilmEdit.DirectorId = DirectorEdit.DirectorId;
                }
            }
                CheckCanDeleteFilm();
        }

        private void Film_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ValidCommand.RaiseCanExecuteChanged();
            CheckCanDeleteFilm();
        }

        private void InitListFilm()
        {
            Films = new ObservableCollection<Film>(_filmService.GetFilms().OrderBy(s => s.Name));
            if (Films != null)
            {
                foreach (Film film in Films)
                {
                    film.RegisterPropertyChanged(Film_PropertyChanged);
                }
            }
        }

        private void InitListDirector()
        {
            Directors = new ObservableCollection<Director>(_directorService.GetDirectors().OrderBy(x => x.TextValue));
            if (Films != null)
            {
                foreach (Film film in Films)
                {
                    film.RegisterPropertyChanged(Film_PropertyChanged);
                }
            }
        }

        #endregion
    }
}
