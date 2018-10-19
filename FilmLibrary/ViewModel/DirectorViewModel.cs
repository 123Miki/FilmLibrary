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
    public class DirectorViewModel : MvvmPropertyChanged
    {
        private ObservableCollection<Director> _directors;
        private DirectorService _directorService;
        private Director _currentDirector;
        private Director _directorEdit;
        private FilmService _filmService;
        private bool _canDeleteDirector;

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
        public Director CurrentDirector
        {
            get { return _currentDirector; }
            set
            {
                if (value != _currentDirector)
                {
                    this._currentDirector = value;
                    RaisePropertyChanged("CurrentDirector");
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
        public bool CanDeleteDirector
        {
            get { return _canDeleteDirector; }
            set
            {
                if (value != _canDeleteDirector)
                {
                    this._canDeleteDirector = value;
                    RaisePropertyChanged("CanDeleteDirector");
                }
            }
        }

        public SimpleCommand ValidCommand { get; set; }
        public SimpleCommand AddDirectorCommand { get; set; }
        public SimpleCommand DeleteDirectorCommand { get; set; }

        #region Méthodes publics

        public DirectorViewModel()
        {
            _directorService = new DirectorService();
            _filmService = new FilmService();
            InitList();
            CanDeleteDirector = false;
            InitCurrentDirector();
            InitDirectorEdit();
            this.RegisterPropertyChanged(DirectorViewModel_PropertyChanged);
            CreateCommands();
        }

        private void InitCurrentDirector()
        {
            CurrentDirector = new Director();
            CurrentDirector.RegisterPropertyChanged(Director_PropertyChanged);
        }

        private void CreateCommands()
        {
            ValidCommand = new SimpleCommand(() => ValidDirector(), CanValid);
            AddDirectorCommand = new SimpleCommand(() => InitDirectorEdit());
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
            if (_directorService.SaveOrUpdateDirector(DirectorEdit))
            {
                InitList();
            }
        }

        private void DeleteDirector()
        {
            if (CanDeleteDirector)
            {
                if (ConfirmationHelper.ConfirmationYesNo("Voulez-vous vraiment supprimer ce élément ?", "Confirmation"))
                {
                    var directorToDelete = Directors.Where(x => x.DirectorId == DirectorEdit.DirectorId).FirstOrDefault();
                    if (directorToDelete != null)
                    {
                        if (_directorService.DeleteDirector(directorToDelete))
                        {
                            InitList();
                            directorToDelete.PropertyChanged -= Director_PropertyChanged;
                            InitCurrentDirector();
                        }
                    }
                }
            }
        }

        private void InitDirectorEdit()
        {
            DirectorEdit = new Director();
            DirectorEdit.RegisterPropertyChanged(Director_PropertyChanged);
            DirectorEdit.DirectorId = Guid.NewGuid();
        }

        private bool CanValid()
        {
            return DirectorEdit != null
                && ValidateHelper.ValidateObject(DirectorEdit);
        }

        private void DirectorViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.GetPropertyName(() => CurrentDirector))
            {
                if (CurrentDirector != null)
                {
                    DirectorEdit.DirectorId = CurrentDirector.DirectorId;
                    DirectorEdit.Name = CurrentDirector.Name;
                    DirectorEdit.Firstname = CurrentDirector.Firstname;
                }
            }
            CheckCanDeleteDirector();
        }

        private void Director_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ValidCommand.RaiseCanExecuteChanged();
        }

        private void InitList()
        {
            Directors = new ObservableCollection<Director>(_directorService.GetDirectors().OrderBy(s => s.TextValue));
            if (Directors != null)
            {
                foreach (Director director in Directors)
                {
                    director.RegisterPropertyChanged(Director_PropertyChanged);
                }
            }
        }

        public void InitCurrentItem()
        {
            if (CurrentDirector != null)
            {
                CurrentDirector = new Director();
                CurrentDirector.RegisterPropertyChanged(Director_PropertyChanged);
                this.RaisePropertyChanged("CurrentDirector");
            }
        }

        #endregion
    }
}
