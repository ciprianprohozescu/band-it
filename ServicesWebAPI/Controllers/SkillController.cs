using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SkillControllerLogic = Controllers.SkillController;
using Models;

namespace ServicesWebAPI.Controllers
{
    public class SkillController : ApiController
    {
        ISkillController skillController;
        public SkillController()
        {
            skillController = new SkillControllerLogic();
        }

        [Route("api/skill")]
        public List<Skill> Get()
        {
            ISkillController skillController = new SkillControllerLogic();
            return skillController.Get();
        }

        [Route("api/skill/add")]
        [HttpPut]
        public void Add(Skill skill)
        {
            ISkillController skillController = new SkillControllerLogic();
            skillController.Add(skill);
        }

        [Route("api/skill/delete/{id}")]
        public Skill Delete(int id)
        {
            skillController.Delete(id);

            return skillController.GetById(id);
        }
    }
}
