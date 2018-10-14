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

        [DataMember]
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        [Range(0, 5, ErrorMessage = "La note doit être comprise entre 0 .. 5.")]
        public int Evaluation { get; set; }

        [JsonIgnore]
        [Required]
        public Director Director { get; set; }

        [DataMember]
        public Guid DirectorId { get; set; }
    }
}
