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
        bool SaveOrUpdateDirector(Director director);
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
            if (_directors == null)
            {
                return new List<Director>();
            }
            return _directors;
        }

        /// <summary>
        /// Méthode permettant  d'obtenir un réalisateur en fonction de son identifiant
        /// </summary>
        /// <param name="id">Identifiant du réalisateur</param>
        /// <returns>Réalisateur demandé</returns>
        public Director GetDirector(Guid id)
        {
            return _directors.FirstOrDefault(director => director.DirectorId == id);
        }

        public bool SaveOrUpdateDirector(Director director)
        {
            if (GetDirector(director.DirectorId) != null)
            {
                return UpdateDirector(director);
            }
            else
            {
                return SaveDirector(director);
            }
        }

        /// <summary>
        /// Méthode permettant d'enregistrer un nouveau réalisateur
        /// </summary>
        /// <param name="director">Nouveau réalisateur</param>
        /// <returns>Retourne un booléen qui indique si la sauvegarde s'est bien passée</returns>
        private bool SaveDirector(Director director)
        {
            bool saved = false;
            try
            {
                _directors.Add(director);
                Save();
                saved = true;
            }
            catch (Exception e)
            {
                Logger.LogMessage(e.Message);
            }
            return saved;
        }

        /// <summary>
        /// Méthode de mise à jour d'un réalisateur
        /// </summary>
        /// <param name="director">Réalisateur à mettre à jour</param>
        /// <returns>Retourne un booléen qui indique si la mise à jour s'est bien passée</returns>
        private bool UpdateDirector(Director director)
        {
            bool updated = false;
            try
            {
                var directorToUpdate = GetDirector(director.DirectorId);
                directorToUpdate.DirectorId = director.DirectorId;
                directorToUpdate.Name = director.Name;
                directorToUpdate.Firstname = director.Firstname;
                Save();
                updated = true;
            }
            catch (Exception e)
            {
                Logger.LogMessage(e.Message);
            }
            return updated;
        }

        /// <summary>
        /// Méthode de suppression d'un réalisateur
        /// </summary>
        /// <param name="film">Réalisateur à supprimer</param>
        /// <returns>Retourne un booléen qui indique si la mise à jour s'est bien passée</returns>
        public bool DeleteDirector(Director director)
        {
            bool deleted = false;
            try
            {
                var directorToDelete = GetDirector(director.DirectorId);
                _directors.Remove(directorToDelete);
                Save();
                deleted = true;
            }
            catch (Exception e)
            {
                Logger.LogMessage(e.Message);
            }
            return deleted;
        }

        #endregion

        #region Méthodes privées

        /// <summary>
        /// Initialisation de la liste des réalisateurs
        /// </summary>
        private void Init()
        {
            _directors = new List<Director>();
            //_directors.Add(new Director() { })
            Load();
        }

        /// <summary>
        /// Chargement de la liste des réalisateur
        /// (Passe par une lecture du fichier Json)
        /// </summary>
        private void Load()
        {
            try
            {
                if (!File.Exists(DataFilePosition.Director))
                {
                    var file = File.Create(DataFilePosition.Director);
                    file.Close();
                }

                string json = File.ReadAllText(DataFilePosition.Director);
                _directors = JsonConvert.DeserializeObject<List<Director>>(json);
                if(_directors == null)
                {
                    _directors = new List<Director>();
                }
            }
            catch (Exception e)
            {
                Logger.LogMessage(e.Message);
            }
        }

        /// <summary>
        /// Sauvegarde de la liste des réalisateurs
        /// (Passe pas l'écriture d'un fichier json)
        /// </summary>
        private void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_directors, Formatting.Indented);
                File.WriteAllText(DataFilePosition.Director, json);
            }
            catch (Exception e)
            {
                Logger.LogMessage(e.Message);
            }
        }

        #endregion
    }
}
