using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Models;
using ClientMVC.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;

namespace ClientMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["ID"] == null)
                return View(new HomeIndex());

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public ActionResult LogIn(string username, string password)
        {
            var model = new HomeIndex();

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user/login", Method.GET);

            request.AddParameter("username", username);
            request.AddParameter("password", password);

            var content = client.Execute(request);
            
            var user = JsonConvert.DeserializeObject<User>(content.Content);

            if (user != null)
            {
                Session["ID"] = user.ID;
                return RedirectToAction("Index", "User");
            }

            model.LogInFailed = true;

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            Session["ID"] = null;

            return View("Index", new HomeIndex());
        }
    }
}