using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FilesAccess
    {
        BandItEntities db;

        public FilesAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public void SaveFile(string ownerType, int id, string fileName)
        {
            if (ownerType == "user")
            {
                var file = new File();
                file.Name = fileName;
                file.Users.Add(db.Users.Find(id));

                db.Files.Add(file);

                db.SaveChanges();
            }
        }

        public void DeleteFile(string ownerType, int id)
        {
            if (ownerType == "user")
            {
                var file = db.Files.Find(id);
                file.Deleted = DateTime.Now;
                db.SaveChanges();
            }
        }
    }
}
