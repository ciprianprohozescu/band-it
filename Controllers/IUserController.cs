using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UserDB = ModelsDB.User;
using UserLogic = Models.User;
using ProfileDB = ModelsDB.Profile;

namespace Controllers
{
    public interface IUserController
    {
        List<UserLogic> Get(string search);
        void Add(UserLogic userLogic);
        List<UserLogic> Get();
        UserLogic GetByUsername(string username);
        UserLogic GetByEmail(string email);
    }
}