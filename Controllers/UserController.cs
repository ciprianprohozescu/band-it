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

            {
            for(int i = 0; i < userDB.Count; i++)
                usersLogic.Add(DBToLogic(userDB[i], userDB[i].Profiles.FirstOrDefault()));
            }

            return usersLogic;
        }

        public void Add(UserLogic userLogic)
        {
            var userAccess = new UsersAccess();
            var userDB = userAccess.FindByUsername(userLogic.Username);
            if(userDB == null)
            {
                userDB = userAccess.FindByEmail(userLogic.Email);
                if(userDB == null)
                {
                    userLogic.Salt = StringCipher.RandomString();
                    userLogic.Password = StringCipher.Encrypt(userLogic.Password, userLogic.Salt);
                    userDB = LogicToDB(userLogic);
                    userAccess.Add(userDB);
                }
            }
        }
        public List<UserLogic> Get()
        {
            var usersAccess = new UsersAccess();
            var userDB = usersAccess.FindAll();
            var usersLogic = new List<UserLogic>();

            for (int i = 0; i < userDB.Count; i++)
            {
                usersLogic.Add(DBToLogic(userDB[i]));
            }

            return usersLogic;
        }
        public UserLogic GetByUsername(string username)
        {
            var userAccess = new UsersAccess();
            var userDB = userAccess.FindByUsername(username);
            return DBToLogic(userDB);
        }
        public UserLogic GetByEmail(string email)
        {
            var userAccess = new UsersAccess();
            var userDB = userAccess.FindByEmail(email);
            return DBToLogic(userDB);
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

        private UserLogic DBToLogic(UserDB userDB)
        {
            var userLogic = new UserLogic();

            userLogic.ID = userDB.ID;
            userLogic.Email = userDB.Email;
            userLogic.Username = userDB.Username;
            userLogic.Password = userDB.Password;
            userLogic.Salt = userDB.Salt;

            return userLogic;
        }

        private UserDB LogicToDB(UserLogic userLogic)
        {
            var userDB = new UserDB();

            userDB.ID = userLogic.ID;
            userDB.Email = userLogic.Email;
            userDB.Username = userLogic.Username;
            userDB.Password = userLogic.Password;
            userDB.Salt = userLogic.Salt;

            return userDB;
        }
    }
}