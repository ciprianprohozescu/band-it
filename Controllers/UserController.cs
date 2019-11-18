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
        private User DBToLogic(UserDB userDB)
        {
            var user = new User();
            if (userDB != null)
            {
                user.ID = userDB.ID;
                user.Username = userDB.Username;
                user.Email = userDB.Email;
                user.Password = userDB.Password;
                user.Salt = userDB.Salt;
                user.FirstName = userDB.FirstName;
                user.LastName = userDB.LastName;
                user.Description = userDB.Description;
                user.Location = new LatLng((double)userDB.Latitude, (double)userDB.Longitude);
                user.ProfilePicture = userDB.ProfilePicture;

                return user;
            }
            return null;
        }

        private UserDB LogicToDB(User user)
        {
            var userDB = new UserDB();

            userDB.ID = user.ID;
            userDB.Username = user.Username;
            userDB.Email = user.Email;
            userDB.Password = user.Password;
            userDB.Salt = user.Salt;
            userDB.FirstName = user.FirstName;
            userDB.LastName = user.LastName;
            userDB.Description = user.Description;
            userDB.Latitude = (decimal)user.Location.Latitude;
            userDB.Longitude = (decimal)user.Location.Longitude;
            userDB.ProfilePicture = user.ProfilePicture;

            return userDB;
        }
    }
}
