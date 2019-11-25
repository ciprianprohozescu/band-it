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
        BandItEntities db = new BandItEntities();

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
    }
}
