using FilmLibrary.Business;
using FilmLibrary.Core;
using FilmLibrary.Core.Helpers;
using FilmLibrary.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace FilmLibrary.ViewModel
{
    public class DirectorViewModel : MvvmPropertyChanged
    {
        private ObservableCollection<Director> _directors;
        private DirectorService _directorService;
        private Director _currentDirector;
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
        public Director CurrentDirector
        {
            get { return _currentDirector; }
            set
            {
                if (value != _currentDirector)
                {
                    this._currentDirector = value;
                    RaisePropertyChanged();
                }
            }
        }
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

        public ICommand ValidCommand { get; set; }
        public ICommand AddDirectorCommand { get; set; }
        public ICommand DeleteDirectorCommand { get; set; }

        #region Méthodes publics

        public DirectorViewModel()
        {
            _directorService = new DirectorService();
            _filmService = new FilmService();
            InitList();
            CanDeleteDirector = false;
            CurrentDirector = new Director();
            CurrentDirector.RegisterPropertyChanged(Director_PropertyChanged);
            ValidCommand = new SimpleCommand(() => ValidDirector(), CanExecute);
            AddDirectorCommand = new SimpleCommand(() => AddDirector());
            DeleteDirectorCommand = new SimpleCommand(() => DeleteDirector());
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

        #endregion

        #region Méthodes privées

        private void ValidDirector()
        {
            _directorService.SaveOrUpdateDirector(CurrentDirector);
            InitList();
        }

        private void DeleteDirector()
        {
            if (CanDeleteDirector)
            {
                _directorService.DeleteDirector(CurrentDirector);
                InitList();
                CurrentDirector = new Director();
            }
        }

        private void AddDirector()
        {
            CurrentDirector = new Director();
            CurrentDirector.DirectorId = Guid.NewGuid();
        }        

        private bool CanExecute()
        {
            return CurrentDirector != null
                && ValidateHelper.ValidateObject(CurrentDirector);
        }

        private void Director_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CheckCanDeleteDirector();
            ValidCommand.CanExecute()
        }        

        private void InitList()
        {
            Directors = new ObservableCollection<Director>(_directorService.GetDirectors());
        }

        #endregion
    }
}
