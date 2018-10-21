using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FilmLibrary.Business
{
    [DataContract]
    public class Director: MvvmPropertyChanged
    {
        [DataMember]
        public Guid DirectorId { get; set; }

        private string _name;
        [DataMember]
        [Required(ErrorMessage = "Champ obligatoire")]
        [StringLength(250)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    this._name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        private string _firstName;
        [DataMember]
        [Required(ErrorMessage = "Champ obligatoire")]
        [StringLength(250)]
        public string Firstname
        {
            get { return _firstName; }
            set
            {
                if (value != _firstName)
                {
                    this._firstName = value;
                    RaisePropertyChanged("Firstname");
                }
            }
        }

        [IgnoreDataMember]
        public string TextValue
        {
            get
            {
                return this.ToString();
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Firstname);
        }
    }
}
