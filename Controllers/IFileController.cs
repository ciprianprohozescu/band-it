using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;

namespace Controllers
{
    public interface IFileController
    {
        void SaveFile(string ownerType, int id, File file);
        void DeleteFile(string ownerType, int id);
    }
}
