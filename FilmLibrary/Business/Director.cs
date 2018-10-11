using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FilmLibrary.Business
{
    [DataContract]
    public class Director
    {
        [DataMember]
        public Guid DirectorId { get; set; }

        [DataMember]
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [DataMember]
        [Required]
        [StringLength(250)]
        public string Firstname { get; set; }

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
            return string.Format("{0} - {1}", Name, Firstname);
        }
    }
}
