using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FilmLibrary.Business
{
    [DataContract]
    public class Film : MvvmPropertyChanged
    {
        [DataMember]
        public Guid FilmId { get; set; }

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

        private DateTime? _releaseDate;
        [DataMember]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate
        {
            get { return _releaseDate; }
            set
            {
                if (value != _releaseDate)
                {
                    this._releaseDate = value;
                    RaisePropertyChanged("ReleaseDate");
                }
            }
        }

        private int? _evaluation;
        [DataMember]
        public int? Evaluation
        {
            get { return _evaluation; }
            set
            {
                if (value != _evaluation)
                {
                    this._evaluation = value;
                    RaisePropertyChanged("Evaluation");
                }
            }
        }

        private Director _director;
        [JsonIgnore]
        [Required(ErrorMessage = "Champ obligatoire")]
        public Director Director
        {
            get { return _director; }
            set
            {
                if (value != _director)
                {
                    this._director = value;
                    RaisePropertyChanged("Director");
                }
            }
        }

        [DataMember]
        public Guid DirectorId { get; set; }
    }
}
