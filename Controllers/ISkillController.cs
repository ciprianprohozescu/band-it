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
        Skill DBToLogic(SkillDB skillDB);
        SkillDB LogicToDB(Skill skill);
    }
}
