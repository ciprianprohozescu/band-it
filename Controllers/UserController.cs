using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;
using UserDB = ModelsDB.User;
using Models;

namespace Controllers
{
    public class UserController : IUserController
    {
        UsersAccess usersAccess;
        public UserController()
        {
            usersAccess = new UsersAccess();
        }
        public List<User> Get(string search, double distance = -1, double markerLat = 0, double markerLng = 0)
        {
            var usersDB = usersAccess.Get(search);
            var users = new List<User>();

            usersDB.ForEach(userDB => users.Add(DBToLogic(userDB)));

            if (distance == -1)
            {
                return users;
            }

            var marker = new LatLng(markerLat, markerLng);
            var filteredUsers = new List<User>();

            foreach (var user in users)
            {
                if (user.Location == null)
                {
                    continue;
                }
                var userDistance = Helpers.HaversineDistance(marker, user.Location);
                if (userDistance <= distance)
                {
                    filteredUsers.Add(user);
                }
            }
            return filteredUsers;
        }

        public void Add(User userLogic)
        {
            var userAccess = new UsersAccess();
            var userDB = userAccess.FindByUsername(userLogic.Username);
            if(userDB == null)
            {
                userDB = userAccess.FindByEmail(userLogic.Email);
                {
                if(userDB == null)
                    userLogic.Salt = StringCipher.RandomString();
                    userLogic.Password = StringCipher.Encrypt(userLogic.Password, userLogic.Salt);
                    userDB = LogicToDB(userLogic);
                }
                    userAccess.Add(userDB);
            }
        }
        public User GetByEmail(string email)
        {
            var userAccess = new UsersAccess();
            var userDB = userAccess.FindByEmail(email);
            return DBToLogic(userDB);
        }
        public User GetByUsername(string username)
        {
            var userDB = usersAccess.FindByUsername(username);
            return DBToLogic(userDB);
        }
        public User GetByID(int id)
        {
            var userDB = usersAccess.FindByID(id);
            return DBToLogic(userDB);
        }

        public void Delete(int id)
        {

            usersAccess.Delete(id);
        }


        public User Get(int id)
        {
            return DBToLogic(usersAccess.FindByID(id));
        }

        private UserDB LogicToDB(User userLogic)
        {
            var userDB = new UserDB();

            userDB.ID = userLogic.ID;
            userDB.Email = userLogic.Email;
            userDB.Username = userLogic.Username;
            userDB.Password = userLogic.Password;
            userDB.Salt = userLogic.Salt;

            return userDB;
        }
        private User DBToLogic(UserDB userDB)
        {
            var userLogic = new User();

            userLogic.ID = userDB.ID;
            userLogic.Email = userDB.Email;
            userLogic.Username = userDB.Username;
            userLogic.Password = userDB.Password;
            userLogic.Salt = userDB.Salt;

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