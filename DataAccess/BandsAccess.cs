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
        BandItEntities db;

        public BandsAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public List<Band> Get(string search)
        {
            var bands = db.Bands
                .Where(band => band.Name.Contains(search)
                || (band.Genres.Where(genre => genre.Name.Contains(search)).Count()>0)
                || search == null)
                .Where(band => band.Deleted == null).OrderByDescending(band => band.ID);

            return bands.ToList();
        }

        public Band FindByID(int id)
        {
            var band = db.Bands
                .Where(x => x.ID == id)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<Band>();
            return band;
        }

        public void Update(Band band)
        {
            var bandDB = FindByID(band.ID);

            if (!bandDB.RowVersion.SequenceEqual(band.RowVersion))
            {
                return;
            }

            bandDB.Name = band.Name;
            bandDB.Description = band.Description;
            bandDB.InviteMessage = band.InviteMessage;

            db.SaveChanges();
        }
    }
}
