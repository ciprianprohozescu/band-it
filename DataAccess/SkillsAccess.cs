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

        public SkillsAccess()
        {
            db = ContextProvider.Instance.DB;
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
                .FirstOrDefault<Skill>();
            return skill;
        }
        public Skill GetByID(int id)
        {
            var skill = db.Skills
                .Where(s => s.ID == id)
                .FirstOrDefault<Skill>();
            return skill;
        }
        public void Add(Skill skill)
        {
            db.Skills.Add(skill);
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var skill = db.Skills.SingleOrDefault(s => s.ID == id);
            skill.Deleted = DateTime.Now;
            db.SaveChanges();
        }
    }
}
