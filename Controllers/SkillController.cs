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
        SkillsAccess skillsAccess;
        public SkillController()
        {
            skillsAccess = new SkillsAccess();
        }
        public List<Skill> Get()
        {
            var skillAccess = new SkillsAccess();
            var skillsDB = skillAccess.Get();
            var skill = new List<Skill>();

            skillsDB.ForEach(s => skill.Add(DBToLogic(s)));

            return skill;
        }
        public Skill GetById(int id)
        {
            var skillAccess = new SkillsAccess();
            var skillDB = skillAccess.GetByID(id);
            return DBToLogic(skillDB);
        }
        public Skill GetByName(string name)
        {
            var skillAccess = new SkillsAccess();
            var skillDB = skillAccess.GetByName(name);
            return DBToLogic(skillDB);
        }
        public void Add(Skill skill)
        {
            var skillDB = LogicToDB(skill);
            skillsAccess.Add(skillDB);
        }
        public void Delete(int id)
        {
            skillsAccess.Delete(id);
        }

        public Skill DBToLogic (SkillDB skillDB)
        {
            var skill = new Skill();
            if(skillDB != null)
            {
                skill.ID = skillDB.ID;
                skill.Name = skillDB.Name;

                return skill;
            }

            return null;
        }

        public SkillDB LogicToDB (Skill skill)
        {
            var skillDB = new SkillDB();

            skillDB.ID = skill.ID;
            skillDB.Name = skill.Name;

            return skillDB;
        }
    }
}
