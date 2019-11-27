using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;
using SkillDB = ModelsDB.Skill;
using Models;

namespace Controllers
{
    public class SkillController : ISkillController
    {
        public List<Skill> Get()
        {
            var skillAccess = new SkillsAccess();
            var skillsDB = skillAccess.Get();
            var skill = new List<Skill>();

            skillsDB.ForEach(s => skill.Add(DBToLogic(s)));

            return skill;
        }

        public Skill DBToLogic (SkillDB skillDB)
        {
            var skill = new Skill();

            skill.ID = skillDB.ID;
            skill.Name = skillDB.Name;

            return skill;
        }
    }
}
