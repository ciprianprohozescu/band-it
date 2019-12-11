using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsDB;

namespace DataAccess
{
    public class SkillsAccess
    {
        BandItEntities db;
        UsersAccess usersAccess;

        public SkillsAccess()
        {
            db = ContextProvider.Instance.DB;
            usersAccess = new UsersAccess();
        }

        public List<Skill> Get()
        {
            var skills = from skill in db.Skills
                         orderby skill.ID descending
                         select skill;
            return skills.ToList<Skill>();
        }
        public Skill GetByName(string name)
        {
            var skill = db.Skills
                .Where(s => s.Name == name)
                .Where(s => s.Deleted == null)
                .FirstOrDefault<Skill>();
            return skill;
        }
        public Skill GetByID(int id)
        {
            var skill = db.Skills
                .Where(s => s.ID == id)
                .Where(s => s.Deleted == null)
                .FirstOrDefault<Skill>();
            return skill;
        }
        public void Add(User user)
        {
            var userDB = usersAccess.FindByID(user.ID);
            var skillDB = GetByID(user.Skills.FirstOrDefault().ID);

            userDB.Skills.Add(skillDB);

            db.SaveChanges();
        }
        public void Delete(User user)
        {
            var userDB = usersAccess.FindByID(user.ID);
            var skillDB = GetByID(user.Skills.FirstOrDefault().ID);

            userDB.Skills.Remove(skillDB);

            db.SaveChanges();
        }
    }
}
