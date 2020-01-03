using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenreDB = ModelsDB.Genres;

namespace Controllers
{
    public interface IGenreController
    {
        List<Genre> Get();
        Genre DBToLogic(GenreDB genreDB);

    }
}
