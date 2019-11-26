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

        public User FindByID(int id)
        {
            var user = db.Users
                .Where(u => u.ID == id)
                .FirstOrDefault<User>();
            return user;
        }

        public void Add(User user)
        {
            using(var dbTransaction = db.Database.BeginTransaction())
            {
                
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    dbTransaction.Commit();
                }
                catch(Exception)
                {
                    dbTransaction.Rollback();
                }
            }
        }
        public void Delete(int id)
        {
            var user = db.Users.SingleOrDefault(u => u.ID == id);
            user.Deleted = DateTime.Now;
            db.SaveChanges();
        }
    }
}