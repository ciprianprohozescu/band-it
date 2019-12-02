using ModelsDB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess
{
    public class UsersAccess
    {
        BandItEntities db;

        public UsersAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public List<User> Get(string search)
        {
            var users = db.Users
                .Where(user => user.Username.Contains(search)
                || user.Email.Contains(search)
                || user.FirstName.Contains(search)
                || user.LastName.Contains(search)
                || (user.Skills.Where(skill => skill.Name.Contains(search)).Count()>0)
                || search == null)
                .Where(user => user.Deleted == null).OrderByDescending(user => user.ID);

            return users.ToList();
        }
        public User FindByUsername(string username)
        {
            var user = db.Users
                .Where(x => x.Username == username)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<User>();

            return user;
        }
        public User FindByEmail(string email)
        {
            var user = db.Users
                .Where(x => x.Email == email)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<User>();

            return user;
        }
        public User FindByEmailOrUsername(string search)
        {
            var user = db.Users
                .Where(x => x.Username == search
                || x.Email == search)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<User>();

            return user;
        }
        public User FindByID(int id)
        {
            var user = db.Users
                .Where(x => x.ID == id)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<User>();

            return user;
        }
        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void Update(int id, string username, string firstName, string lastName, string description,string email,string password)
        {
            var user = FindByID(id);
            user.Username = username;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Description = description;
            user.Email = email;
            user.Password = password;

            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var user = db.Users.SingleOrDefault(u => u.ID == id);
            user.Deleted = DateTime.Now;
            db.SaveChanges();
        }
        public void UpdateProfilePicture(int id, string fileName)
        {
            var user = FindByID(id);
            user.ProfilePicture = fileName;
        }
        public void SaveLocation(User user)
        {
            var validUser = db.Users.SingleOrDefault(u => u.ID == user.ID);
            validUser.Latitude = user.Latitude;
            validUser.Longitude = user.Longitude;
            db.SaveChanges();
        }
    }
}