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

        public void Add(UserLogic userLogic)
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
        public UserLogic GetByEmail(string email)
        {
            var userAccess = new UsersAccess();
            var userDB = userAccess.FindByEmail(email);
            return DBToLogic(userDB);
        private UserLogic DBToLogic(UserDB userDB, ProfileDB profileDB)
        }
        public User GetByUsername(string username)
        {
            var userDB = usersAccess.FindByUsername(username);
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
    }
}