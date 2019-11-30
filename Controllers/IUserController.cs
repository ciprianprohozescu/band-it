using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserDB = ModelsDB.User;
using Models;
using System.Web;

namespace Controllers
{
    public interface IUserController
    {
        void Add(User userLogic);
        User GetByUsername(string username);
        User GetByEmail(string email);
        User GetById(int id);
        List<User> Get();
        List<User> Get(string search, double distance = -1, double markerLat = 0, double markerLng = 0);
        void Delete(int id);
        User LogIn(string username, string password);
        void UpdateProfilePicture(int id, string fileName);
        void SaveFile(int id, File file);
    }
}