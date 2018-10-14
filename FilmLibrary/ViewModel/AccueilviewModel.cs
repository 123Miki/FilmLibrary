using FilmLibrary.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLibrary.ViewModel
{
    public class AccueilViewModel : MvvmPropertyChanged
    {
        private string _currentMenuSelection;
        public string CurrentMenuSelection
        {
            get
            {
                return _currentMenuSelection;
            }

            set
            {
                if (value != _currentMenuSelection)
                {
                    this._currentMenuSelection = value;
                    RaisePropertyChanged();
                }
            }
        }

        public AccueilViewModel()
        {
            InitProperties();
        }

        private void InitProperties()
        {
            CurrentMenuSelection = "";
        }
    }
}
