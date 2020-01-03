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
        BandItContext db;

        public BandsAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public List<Bands> Get(string search)
        {
            var bands = db.Bands
                .Where(band => band.Name.Contains(search)
                || (band.BandGenres.Where(bandGenre => bandGenre.Genre.Name.Contains(search)).Count()>0)
                || search == null)
                .Where(band => band.Deleted == null).OrderByDescending(band => band.Id);

            return bands.ToList();
        }

        public Bands FindByName(string name)
        {
            var band = db.Bands
                .Where(x => x.Name == name)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<Bands>();

            return band;
        }

        public Bands FindByID(int id)
        {
            var band = db.Bands
                .Where(b => b.Id == id)
                .FirstOrDefault<Bands>();
            return band;
        }

        public void Add(Bands band)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {

                try
                {
                    db.Bands.Add(band);
                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                }
            }
        }

        public void Delete(int id)
        {
            var band = db.Bands.SingleOrDefault(b => b.Id== id);
            band.Deleted = DateTime.Now;
            db.SaveChanges();
        }

        public bool Update(Bands band)
        {
            Bands bandDB = FindByID(band.Id);

            if (!bandDB.RowVersion.SequenceEqual(band.RowVersion))
            {
                return false;
            }

            bandDB.Name = band.Name;
            bandDB.Description = band.Description;
            bandDB.InviteMessage = band.InviteMessage;

            db.SaveChanges();
            return true;
        }
    }
}
