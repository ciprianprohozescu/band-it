using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenreAccess
    {
        BandItContext db;

        public GenreAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public List<Genres> Get()
        {
            var genres = from genre in db.Genres
                         orderby genre.Id descending
                         select genre;
            return genres.ToList<Genres>();
        }
    }
}
