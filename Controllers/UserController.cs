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
    public class UserController
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
        //public void Add(UserLogic userLogic)
        //{
        //    var usersAccess = new UsersAccess();
        //    var userDB = usersAccess.FindByUserame(userLogic.Username);

        //    if(userDB == null)
        //    {
        //        userDB = LogicToDB(userLogic);
        //        usersAccess.AddUser(userDB);
        //    }
        //}
        private UserLogic DBToLogic(UserDB userDB, ProfileDB profileDB)
        {
            var userLogic = new UserLogic();

            userLogic.ID = userDB.ID;
            userLogic.Username = userDB.Username;
            userLogic.Email = userDB.Email;
            userLogic.Password = userDB.Password;
            userLogic.Salt = userDB.Salt;
            userLogic.Deleted = userDB.Deleted;
            userLogic.FirstName = profileDB.FirstName;
            userLogic.LastName = profileDB.LastName;
            userLogic.Description = profileDB.Description;
            userLogic.Longitude = profileDB.Longitude;
            userLogic.Latitude = profileDB.Latitude;
            userLogic.ProfilePicture = profileDB.ProfilePicture;


            return userLogic;
        }
        //private UserDB LogicToDB(UserLogic userLogic)
        //{
        //    var userDB = new UserDB();

        //    userDB.ID = userLogic.ID;
        //    userDB.Username = userLogic.Username;
        //    userDB.Email = userLogic.Email;
        //    userDB.Password = userLogic.Password;
        //    userDB.Salt = userLogic.Salt;
        //    userDB.Deleted = userLogic.Deleted;

        //    return userDB;
        //}
    }
}
