using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace FilmLibrary.Core.Helpers
{
    public static class ValidateHelper
    {
        /// <summary>
        /// Valider les ValidationAttribute de toutes les propriétés de obj
        /// </summary>
        /// <param name="obj">Instance de l'objet à évaluer</param>
        /// <returns>Vrai si toutes les propriétés sont valides.</returns>
        public static bool ValidateObject(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Validator.TryValidateObject(obj, new ValidationContext(obj), null, true);
        }
    }
}
