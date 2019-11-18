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
        List<User> Get(string search, double distance = -1, double markerLat = 0, double markerLng = 0);
        User Get(int id);
    }
}
