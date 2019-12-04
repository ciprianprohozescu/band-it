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
        BandItEntities db;

        public GenreAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public List<Genre> Get()
        {
            var genres = from genre in db.Genres
                         orderby genre.ID descending
                         select genre;
            return genres.ToList<Genre>();
        }
    }
}
