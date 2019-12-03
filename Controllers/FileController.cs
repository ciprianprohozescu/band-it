using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class FileController : IFileController
    {
        FilesAccess filesAccess;

        public FileController()
        {
            filesAccess = new FilesAccess();
        }

        public void SaveFile(string ownerType, int id, File file)
        {
            filesAccess.SaveFile(ownerType, id, file.Name);
        }

        public void DeleteFile(string ownerType, int id)
        {
            filesAccess.DeleteFile(ownerType, id);
        }
    }
}
