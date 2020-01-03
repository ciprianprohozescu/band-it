using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;
using SkillDB = ModelsDB.Skills;
using UserDB = ModelsDB.Users;
using Models;
using ModelsDB;

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
        public void Add(User user)
        {
            var userDB = new UserDB();
            userDB.Id = user.ID;

            UserSkills userSkill = new UserSkills();
            userSkill.SkillId = user.Skills[0].ID;
            userDB.UserSkills.Add(userSkill);

            skillsAccess.Add(userDB);
        }
        public void Delete(User user)
        {
            var userDB = new UserDB();
            userDB.Id = user.ID;

            UserSkills userSkill = new UserSkills();
            userSkill.UserId = user.ID;
            userSkill.SkillId = user.Skills[0].ID;
            userDB.UserSkills.Add(userSkill);

            skillsAccess.Delete(userDB);
        }

        public Skill DBToLogic (SkillDB skillDB)
        {
            var skill = new Skill();
            if(skillDB != null)
            {
                skill.ID = skillDB.Id;
                skill.Name = skillDB.Name;

                return skill;
            }

            return null;
        }

        public SkillDB LogicToDB (Skill skill)
        {
            var skillDB = new SkillDB();

            skillDB.Id = skill.ID;
            skillDB.Name = skill.Name;

            return skillDB;
        }
    }
}
