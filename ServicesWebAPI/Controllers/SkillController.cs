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
            return skillController.Get();
        }

        [Route("api/skill/add")]
        [HttpPut]
        public void Add(User user)
        {
            skillController.Add(user);
        }

        [Route("api/skill/delete")]
        public void Delete(User user)
        {
            skillController.Delete(user);
        }
    }
}
