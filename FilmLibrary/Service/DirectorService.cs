using FilmLibrary.Business;
using FilmLibrary.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FilmLibrary.Service
{
    #region Interface

    public interface IDirectorService
    {
        List<Director> GetDirectors();
        Director GetDirector(Guid id);
        bool SaveDirector(Director director);
        bool UpdateDirector(Director director);
    }

    #endregion 

    public class DirectorService : IDirectorService
    {
        private List<Director> _directors;

        #region Méthodes publics

        /// <summary>
        /// Constructeur
        /// </summary>
        public DirectorService()
        {
            Init();
        }

        /// <summary>
        /// Méthode permettant d'obtenir la liste des réalisateurs
        /// </summary>
        /// <returns>Liste des réalisateurs</returns>
        public List<Director> GetDirectors()
        {
            return _directors;
        }

        /// <summary>
        /// Méthode permettant  d'obtenir un réalisateur en fonction de son identifiant
        /// </summary>
        /// <param name="id">Identifiant du réalisateur</param>
        /// <returns>Réalisateur demandé</returns>
        public Director GetDirector(Guid id)
        {
            return _directors.FirstOrDefault(d => d.DirectorId == id);
        }

        /// <summary>
        /// Méthode permettant d'enregistrer un nouveau réalisateur
        /// </summary>
        /// <param name="director">Nouveau réalisateur</param>
        /// <returns>Retourne un booléen qui indique si la sauvegarde s'est bien passée</returns>
        public bool SaveDirector(Director director)
        {
            bool saved = false;
            try
            {
                _directors.Add(director);
                Save();
                saved = true;
            }
            catch (Exception)
            {
                throw;
            }
            return saved;
        }

        /// <summary>
        /// Méthode de mise à jour d'un réalisateur
        /// </summary>
        /// <param name="director">Réalisateur à mettre à jour</param>
        /// <returns>Retourne un booléen qui indique si la mise à jour s'est bien passée</returns>
        public bool UpdateDirector(Director director)
        {
            bool updated = false;
            try
            {
                var old = GetDirector(director.DirectorId);
                old = director;
                // vérif si c'est bon sur la liste
                // sinon affecter
                Save();
                updated = true;
            }
            catch (Exception)
            {
                throw;
            }
            return updated;
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Initialisation de la liste des réalisateurs
        /// </summary>
        private void Init()
        {
            _directors = new List<Director>();
            Load();
        }

        /// <summary>
        /// Chargement de la liste des réalisateur
        /// (Passe par une lecture du fichier Json)
        /// </summary>
        private void Load()
        {
            if (!File.Exists(DataFilePosition.Director))
            {
                var file = File.Create(DataFilePosition.Director);
                file.Close();
            }

            string json = File.ReadAllText(DataFilePosition.Director);
            _directors = JsonConvert.DeserializeObject<List<Director>>(json);
        }

        /// <summary>
        /// Sauvegarde de la liste des réalisateurs
        /// (Passe pas l'écriture d'un fichier json)
        /// </summary>
        private void Save()
        {
            string json = JsonConvert.SerializeObject(_directors, Formatting.Indented);
            File.WriteAllText(DataFilePosition.Director, json);
        }

        #endregion
    }
}
