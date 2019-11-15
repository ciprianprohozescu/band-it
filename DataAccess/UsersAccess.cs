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
        BandItEntities db = new BandItEntities();
        public List<User> Get(string search)
        {
            var users = db.Users
                .Where(user => user.Username.Contains(search)
                || user.Email.Contains(search)
                || user.Profiles.FirstOrDefault().FirstName.Contains(search)
                || user.Profiles.FirstOrDefault().LastName.Contains(search)
                || search == null)
                .Where(user => user.Deleted == null).OrderByDescending(user => user.ID);

            return users.ToList();
        }
        public User FindByUsername(string username)
        {
            var user = db.Users
                .Where(u => u.Username == username)
                .FirstOrDefault<User>();

            return user;
        }
        public User FindByEmail(string email)
        {
            var user = db.Users
                .Where(u => u.Email == email)
                .FirstOrDefault<User>();
            return user;
        }

        public List<User> FindAll()
        {
            List<User> users = db.Users.ToList();
            return users;
        }

        public void Add(User user)
        {
            using(var dbTransaction = db.Database.BeginTransaction())
            {
                
                try
                {
                    db.Users.Add(user);
                    Profile profile = new Profile();
                    profile.User = user;
                    db.Profiles.Add(new Profile());
                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch(Exception)
                {
                    dbTransaction.Rollback();
                }
            }
        }
    }
}