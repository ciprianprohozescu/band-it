using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserDB = ModelsDB.User;
using Models;

namespace Controllers
{
    public interface IUserController
    {
        void Add(User userLogic);
        User GetByUsername(string username);
        User GetByEmail(string email);
        List<User> Get(string search, double distance = -1, double markerLat = 0, double markerLng = 0);
        void Delete(int id);
        User GetByID(int id);
    }
}