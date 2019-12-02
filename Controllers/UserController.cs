    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;
using UserDB = ModelsDB.User;
using SkillDB = ModelsDB.Skill;
using Models;
using System.Configuration;
using System.Web;

namespace Controllers
{
    public class UserController : IUserController
    {
        UsersAccess usersAccess;
        ISkillController skillController;
        IApplicationController applicationController;

        public UserController()
        {
            usersAccess = new UsersAccess();
            skillController = new SkillController();
            applicationController = new ApplicationController();
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
            var userDB = usersAccess.FindByUsername(userLogic.Username);
            if(userDB == null)
            {
                userDB = usersAccess.FindByEmail(userLogic.Email);
                if(userDB == null)
                {
                    userLogic.Salt = StringCipher.RandomString();
                    //TODO: Move passphrase (and Google Maps API) to secure location
                    userLogic.Password = StringCipher.Encrypt(userLogic.Password + userLogic.Salt, "hello");
                    userDB = LogicToDB(userLogic);
                    usersAccess.Add(userDB);
                }
            }
        }
        public List<User> Get()
        {
            var userDB = usersAccess.Get("");
            var usersLogic = new List<User>();

            for (int i = 0; i < userDB.Count; i++)
            {
                usersLogic.Add(DBToLogic(userDB[i]));
            }

            return usersLogic;
        }
        public User GetByUsername(string username)
        {
            var userDB = usersAccess.FindByUsername(username);
            return DBToLogic(userDB);
        }
        public User GetByEmail(string email)
        {
            var userDB = usersAccess.FindByEmail(email);
            return DBToLogic(userDB);
        }

        public void Delete(int id)
        {
            usersAccess.Delete(id);
        }

        public User GetById(int id)
        {
            var userDB = usersAccess.FindByID(id);
            return DBToLogic(userDB);
        }
        public void Update(int id, string username, string firstName, string lastName, string description, string email, string password)
        {
           usersAccess.Update(id, username, firstName, lastName, description, email, password);
        }

        public User LogIn(string username, string password)
        {
            var user = usersAccess.FindByEmailOrUsername(username);

            if (user == null)
            {
                return null;
            }

            string saltedPassword;
            try
            {
                saltedPassword = StringCipher.Decrypt(user.Password, "hello");
            } catch (Exception)
            {
                return null;
            }

            if (saltedPassword != password + user.Salt)
            {
                return null;
            }

            return DBToLogic(user);
        }


        public void UpdateProfilePicture(int id, string fileName)
        {
            usersAccess.UpdateProfilePicture(id, fileName);
        }

        public void SaveLocation(User user)
        {
            usersAccess.SaveLocation(LogicToDB(user));
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

                if (userDB.Latitude != null && userDB.Longitude != null)
                {
                    user.Location = new LatLng((double)userDB.Latitude, (double)userDB.Longitude);
                }

                user.ProfilePicture = userDB.ProfilePicture;

                user.Skills = new List<Skill>();
                foreach (var skill in userDB.Skills)
                {
                    user.Skills.Add(skillController.DBToLogic(skill));
                }
                user.Applications = new List<Application>();
                foreach (var application in userDB.Applications)
                {
                    user.Applications.Add(applicationController.DBToLogic(application));
                }

                user.Files = new List<File>();
                foreach (var file in userDB.Files)
                {
                    if (file.Deleted != null)
                    {
                        continue;
                    }

                    var fileLogic = new File();
                    fileLogic.Name = file.Name;
                    fileLogic.ID = file.ID;

                    user.Files.Add(fileLogic);
                }

                return user;
            }
            return null;
        }
        private UserDB LogicToDB(User user)
        {
            var userDB = new UserDB();
            if (user != null)
            {
                userDB.ID = user.ID;
                userDB.Username = user.Username;
                userDB.Email = user.Email;
                userDB.Password = user.Password;
                userDB.Salt = user.Salt;
                userDB.FirstName = user.FirstName;
                userDB.LastName = user.LastName;
                userDB.Description = user.Description;
                if (user.Location != null)
                {
                    userDB.Latitude = (decimal)user.Location.Latitude;
                    userDB.Longitude = (decimal)user.Location.Longitude;
                }
                userDB.ProfilePicture = user.ProfilePicture;

                userDB.Skills = new List<SkillDB>();

                foreach (var skillDB in user.Skills)
                {
                    userDB.Skills.Add(skillController.LogicToDB(skillDB));
                }

                return userDB;
            }

            return null;
        }


    }
}