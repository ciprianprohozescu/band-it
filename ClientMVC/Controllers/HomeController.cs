using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using ClientMVC.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ClientMVC.Controllers
{
    public class HomeController : Controller
    {
        private Nullable<int> _loggedInUserId;
        private ISession _session;

        public HomeController(IHttpContextAccessor httpContextAccessor) {
            _loggedInUserId = httpContextAccessor.HttpContext.Session.GetInt32("ID");
            _session = httpContextAccessor.HttpContext.Session;
        }

        public ActionResult Index()
        {
            if (_loggedInUserId == null)
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
                _session.SetInt32("ID", user.ID);
                return RedirectToAction("Index", "User");
            }

            model.LogInFailed = true;

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            _loggedInUserId = null;

            return View("Index", new HomeIndex());
        }
    }
}