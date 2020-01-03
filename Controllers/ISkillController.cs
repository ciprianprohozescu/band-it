using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using SkillDB = ModelsDB.Skills;
namespace Controllers
{
    public interface ISkillController
    {
        List<Skill> Get();
        Skill GetById(int id);
        Skill GetByName(string name);
        Skill DBToLogic(SkillDB skillDB);
        SkillDB LogicToDB(Skill skill);
        void Add(User user);
        void Delete(User user);
    }
}
