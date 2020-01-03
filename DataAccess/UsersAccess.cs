using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UsersAccess
    {
        BandItContext db;

        public UsersAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public List<Users> Get(string search)
        {
            var users = db.Users
                .Where(user => user.Username.Contains(search)
                || user.Email.Contains(search)
                || user.FirstName.Contains(search)
                || user.LastName.Contains(search)
                || (user.UserSkills.Where(userSkill => userSkill.Skill.Name.Contains(search)).Count()>0)
                || search == null)
                .Where(user => user.Deleted == null).OrderByDescending(user => user.Id);

            return users.ToList();
        }
        public Users FindByUsername(string username)
        {
            var user = db.Users
                .Where(x => x.Username == username)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<Users>();

            return user;
        }
        public Users FindByEmail(string email)
        {
            var user = db.Users
                .Where(x => x.Email == email)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<Users>();

            return user;
        }
        public Users FindByEmailOrUsername(string search)
        {
            var user = db.Users
                .Where(x => x.Username == search
                || x.Email == search)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<Users>();

            return user;
        }
        public Users FindByID(int id)
        {
            var user = db.Users
                .Where(x => x.Id == id)
                .Where(x => x.Deleted == null)
                .FirstOrDefault<Users>();

            return user;
        }
        public void Add(Users user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public void Update(Users user)
        {
            var userDB = FindByID(user.Id);
            userDB.Username = user.Username;
            userDB.FirstName = user.FirstName;
            userDB.LastName = user.LastName;
            userDB.Description = user.Description;
            userDB.Email = user.Email;
            userDB.Password = user.Password;
            
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var user = db.Users.SingleOrDefault(u => u.Id == id);
            user.Deleted = DateTime.Now;
            db.SaveChanges();
        }
        public void UpdateProfilePicture(int id, string fileName)
        {
            var user = FindByID(id);
            user.ProfilePicture = fileName;
        }
        public void SaveLocation(Users user)
        {
            var validUser = db.Users.SingleOrDefault(u => u.Id == user.Id);
            validUser.Latitude = user.Latitude;
            validUser.Longitude = user.Longitude;
            db.SaveChanges();
        }
    }
}