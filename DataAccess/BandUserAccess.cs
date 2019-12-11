using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BandUserAccess
    {
        BandItEntities db;

        public BandUserAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public void Add(BandUser bandUser)
        {
            db.BandUsers.Add(bandUser);
            db.SaveChanges();
        }

        public BandUser Get(int id)
        {
            var bandUser = db.BandUsers
                .Where(b => b.ID == id)
                .FirstOrDefault<BandUser>();
            return bandUser;
        }
    }
}
