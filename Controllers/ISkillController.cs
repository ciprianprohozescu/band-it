using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using SkillDB = ModelsDB.Skill;
namespace Controllers
{
    public interface ISkillController
    {
        List<Skill> Get();
        Skill GetById(int id);
        Skill GetByName(string name);
        Skill DBToLogic(SkillDB skillDB);
        SkillDB LogicToDB(Skill skill);
        void Add(Skill skill);
        void Delete(int id);
    }
}
