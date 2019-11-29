using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{

    public class BandsAccess
    {
        BandItEntities db = new BandItEntities();

        public List<Band> Get(string search)
        {
            var bands = db.Bands
                .Where(band => band.Name.Contains(search)
                || (band.Genres.Where(genre => genre.Name.Contains(search)).Count()>0)
                || search == null)
                .Where(band => band.Deleted == null).OrderByDescending(band => band.ID);

            return bands.ToList();
        }
    }
}
