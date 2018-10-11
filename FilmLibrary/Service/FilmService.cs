using FilmLibrary.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLibrary.Service
{
    class FilmService
    {
        private DirectorService directorService;

        private List<Film> _films;

        public FilmService()
        {
            directorService = new DirectorService();
        }

        //private void Init


        private void Laod()
        { // lire fichier 
          // var listeFilmDto = listeDuFichier
          // foreach filmDTo  in listeFilmDto
          //new Film (filmDTo) f
          //      f.directeur = directorService.GetDirector(filmDTo.DirecteurId);

          //  _films.Add(filmDTo)
        }
        private void Save()
        { // save fichier 
        }
    }
}
