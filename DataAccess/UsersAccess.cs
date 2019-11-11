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
            var usersQuery = from user in db.Users
                             orderby user.ID descending
                             select user;

            var users = db.Users.Where(user =>
            user.Username.Contains(search) || user.Email.Contains(search) || user.Profiles.First().FirstName.Contains(search) || user.Profiles.First().LastName.Contains(search));

            return users.ToList();
        }
        public User FindByUserame(string username)
        {
            var user = db.Users
                .Where(x => x.Username == username)
                .FirstOrDefault<User>();

            return user;
        }
        public void AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
