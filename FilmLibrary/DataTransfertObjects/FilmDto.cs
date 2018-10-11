using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FilmLibrary.DataTransfertObjects
{
    [DataContract]
    public class FilmDto : MvvmDto
    {
        [DataMember]
        public Guid FilmId { get; set; }

        [DataMember]
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [DataMember]
        public DateTime Year { get; set; }

        [DataMember]
        [Range(0, 5, ErrorMessage = "La note doit être comprise entre 0 .. 5.")]
        public int Evaluation { get;  set; }

        [DataMember]
        [Required]
        public DirectorDto Director { get; set; }
    }
}
