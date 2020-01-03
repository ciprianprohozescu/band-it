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
        BandItContext db;

        public FilesAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public void SaveFile(string ownerType, int id, string fileName)
        {
            if (ownerType == "user")
            {
                var file = new Files();
                file.Name = fileName;

                //TODO: Check that this many-to-many still works as expected
                var userFile = new UserFiles();
                userFile.UserId = id;
                file.UserFiles.Add(userFile);

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
