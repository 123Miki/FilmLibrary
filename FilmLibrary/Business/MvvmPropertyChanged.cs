using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FilmLibrary.Business
{
    public abstract class MvvmPropertyChanged : MvvmErrorChanged, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Teste si un délégué est déjà abonné à PropertyChanged
        /// </summary>
        /// <param name="prospectiveHandler">Délégué à vérifier</param>
        /// <returns>Vrai s'il est déjà inscrit</returns>
        public bool IsPropertyChangedRegistered(PropertyChangedEventHandler prospectiveHandler)
        {
            if (PropertyChanged != null)
            {
                foreach (Delegate existingHandler in PropertyChanged.GetInvocationList())
                {
                    if (existingHandler.Equals(prospectiveHandler))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Abonne un délégué à l'événement PropertyChanged à la condition qu'il n'y soit
        /// pas déjà abonné.
        /// </summary>
        /// <param name="prospectiveHandler">Délégué à inscrire</param>
        public void RegisterPropertyChanged(PropertyChangedEventHandler prospectiveHandler)
        {
            if (!IsPropertyChangedRegistered(prospectiveHandler))
            {
                PropertyChanged += prospectiveHandler;
            }
        }
    }
}
