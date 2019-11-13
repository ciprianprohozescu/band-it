using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;
using UserDB = ModelsDB.User;
using UserLogic = Models.User;
using ProfileDB = ModelsDB.Profile;

namespace Controllers
{
    public class UserController : IUserController
    {
        public List<UserLogic> Get(string search)
        {
            var usersAccess = new UsersAccess();
            var userDB = usersAccess.Get(search);
            var usersLogic = new List<UserLogic>();

            for(int i = 0; i < userDB.Count; i++)
            {
                usersLogic.Add(DBToLogic(userDB[i], userDB[i].Profiles.FirstOrDefault()));
            }

            return usersLogic;
        }
        private UserLogic DBToLogic(UserDB userDB, ProfileDB profileDB)
        {
            var userLogic = new UserLogic();

            userLogic.ID = userDB.ID;
            userLogic.Username = userDB.Username;
            userLogic.Email = userDB.Email;
            userLogic.Password = userDB.Password;
            userLogic.Salt = userDB.Salt;
            userLogic.FirstName = profileDB.FirstName;
            userLogic.LastName = profileDB.LastName;
            userLogic.Description = profileDB.Description;
            userLogic.Longitude = profileDB.Longitude;
            userLogic.Latitude = profileDB.Latitude;
            userLogic.ProfilePicture = profileDB.ProfilePicture;


            return userLogic;
        }
    }
}
