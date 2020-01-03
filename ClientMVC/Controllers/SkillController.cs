using Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ClientMVC.Controllers
{
    public class SkillController : Controller
    {
        [HttpPost]
        public ActionResult Add(int userID, int skillID)
        {
            var user = new User();
            user.ID = userID;
            user.Skills = new List<Skill>();

            var skill = new Skill();
            skill.ID = skillID;

            user.Skills.Add(skill);

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));

            var request = new RestRequest($"skill/add", Method.PUT);
            request.AddJsonBody(user);

            var response = client.Execute(request);

            return RedirectToAction("Show", "User", new { id = userID });
        }

        [HttpPost]
        public ActionResult Delete(int userID, int skillID)
        {
            var user = new User();
            user.ID = userID;
            user.Skills = new List<Skill>();

            var skill = new Skill();
            skill.ID = skillID;

            user.Skills.Add(skill);

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));

            var request = new RestRequest($"skill/delete", Method.DELETE);
            request.AddJsonBody(user);

            var response = client.Execute(request);

            return RedirectToAction("Show", "User", new { id = userID });
        }
    }
}