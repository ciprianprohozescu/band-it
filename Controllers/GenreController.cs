using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenreDB = ModelsDB.Genre;

namespace Controllers
{
    public class GenreController : IGenreController
    {
        public List<Genre> Get()
        {
            var genreAccess = new GenreAccess();
            var genresDB = genreAccess.Get();
            var genre = new List<Genre>();

            genresDB.ForEach(g => genre.Add(DBToLogic(g)));

            return genre;
        }

        public Genre DBToLogic(GenreDB genreDB)
        {
            var genre = new Genre();

            genre.ID = genreDB.ID;
            genre.Name = genre.Name;

            return genre;
        }
    }
}
