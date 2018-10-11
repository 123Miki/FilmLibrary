﻿using FilmLibrary.Business;
using FilmLibrary.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FilmLibrary.Service
{
    #region Interface

    public interface IFilmService
    {
        List<Film> GetFilms();
        Film GetFilm(Guid id);
        bool SaveFilm(Film film);
        bool UpdateFilm(Film film);
    }

    #endregion 

    public class FilmService : IFilmService
    {
        private IDirectorService directorService;
        private List<Film> _films;

        #region Méthodes publics

        public FilmService()
        {
            Init();
        }

        /// <summary>
        /// Méthode permettant d'obtenir la liste des films
        /// </summary>
        /// <returns>Liste des films</returns>
        public List<Film> GetFilms()
        {
            return _films;
        }

        /// <summary>
        /// Méthode permettant  d'obtenir un film en fonction de son identifiant
        /// </summary>
        /// <param name="id">Identifiant du film</param>
        /// <returns>Film demandé</returns>
        public Film GetFilm(Guid id)
        {
            return _films.FirstOrDefault(film => film.FilmId == id);
        }

        /// <summary>
        /// Méthode permettant d'enregistrer un nouveau film
        /// </summary>
        /// <param name="film">Nouveau film</param>
        /// <returns>Retourne un booléen qui indique si la sauvegarde s'est bien passée</returns>
        public bool SaveFilm(Film film)
        {
            bool saved = false;
            try
            {
                _films.Add(film);
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
        /// Méthode de mise à jour d'un film
        /// </summary>
        /// <param name="film">Film à mettre à jour</param>
        /// <returns>Retourne un booléen qui indique si la mise à jour s'est bien passée</returns>
        public bool UpdateFilm(Film film)
        {
            bool updated = false;
            try
            {
                var filmToUpdate = GetFilm(film.FilmId);
                filmToUpdate = film;
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
        /// Initialisation de la liste des films
        /// </summary>
        private void Init()
        {
            _films = new List<Film>();
            directorService = new DirectorService();
            Load();
        }

        /// <summary>
        /// Chargement de la liste des films
        /// (Passe par une lecture du fichier Json)
        /// </summary>
        private void Load()
        {
            if (!File.Exists(DataFilePosition.Film))
            {
                var file = File.Create(DataFilePosition.Film);
                file.Close();
            }

            string json = File.ReadAllText(DataFilePosition.Film);
            var filmsFromJson = JsonConvert.DeserializeObject<List<Film>>(json);

            foreach (Film film in filmsFromJson)
            {
                film.Director = directorService.GetDirector(film.DirectorId);
            }

            _films = new List<Film>(filmsFromJson);
        }

        /// <summary>
        /// Sauvegarde de la liste des films
        /// (Passe pas l'écriture d'un fichier json)
        /// </summary>
        private void Save()
        {
            string json = JsonConvert.SerializeObject(_films, Formatting.Indented);
            File.WriteAllText(DataFilePosition.Film, json);
        }

        #endregion
    }
}
