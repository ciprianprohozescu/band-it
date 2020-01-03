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
        BandItContext db;
        UsersAccess usersAccess;

        public SkillsAccess()
        {
            db = ContextProvider.Instance.DB;
            usersAccess = new UsersAccess();
        }

        public List<Skills> Get()
        {
            var skills = from skill in db.Skills
                         orderby skill.Id descending
                         select skill;
            return skills.ToList<Skills>();
        }
        public Skills GetByName(string name)
        {
            var skill = db.Skills
                .Where(s => s.Name == name)
                .Where(s => s.Deleted == null)
                .FirstOrDefault<Skills>();
            return skill;
        }
        public Skills GetByID(int id)
        {
            var skill = db.Skills
                .Where(s => s.Id == id)
                .Where(s => s.Deleted == null)
                .FirstOrDefault<Skills>();
            return skill;
        }
        public void Add(Users user)
        {
            Users userDB = usersAccess.FindByID(user.Id);

            //TODO: Check that this many-to-many still works as expected
            UserSkills userSkill = new UserSkills();
            userSkill.UserId = user.Id;
            userSkill.SkillId = user.UserSkills.FirstOrDefault().SkillId;

            userDB.UserSkills.Add(userSkill);

            db.SaveChanges();
        }
        public void Delete(Users user)
        {
            Users userDB = usersAccess.FindByID(user.Id);

            //TODO: Check that this many-to-many still works as expected
            UserSkills userSkill = new UserSkills();
            userSkill.SkillId = user.UserSkills.FirstOrDefault().SkillId;
            userSkill.UserId = user.Id;

            userDB.UserSkills.Remove(userSkill);

            db.SaveChanges();
        }
    }
}
