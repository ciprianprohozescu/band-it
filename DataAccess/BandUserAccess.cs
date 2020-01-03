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
        BandItContext db;

        public BandUserAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public void Add(BandUsers bandUser)
        {
            db.BandUsers.Add(bandUser);
            db.SaveChanges();
        }

        public BandUsers Get(int id)
        {
            var bandUser = db.BandUsers
                .Where(b => b.Id == id)
                .FirstOrDefault<BandUsers>();
            return bandUser;
        }
    }
}
