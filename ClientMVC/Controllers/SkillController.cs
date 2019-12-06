using Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientMVC.Controllers
{
    public class SkillController : Controller
    {
        [HttpPost]
        public ActionResult Add(string name, int id)
        {
            var user = new User();
            user.ID = id;

            return null;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"skill/delete/{id}", Method.DELETE);
            client.Execute(request);
            
            return RedirectToAction($"Show/{id}");
        }
    }
}