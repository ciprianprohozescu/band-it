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
    }
}
