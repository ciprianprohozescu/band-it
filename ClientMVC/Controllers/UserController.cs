using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using UserLogic = Models.User;
using ClientMVC.Models;

namespace ClientMVC.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index(string search)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user", Method.GET);
            request.AddParameter("search", search);
            var content = client.Execute(request).Content;

            var users = JsonConvert.DeserializeObject<List<UserLogic>>(content);

            var model = new UserIndex();
            model.Search = search;
            model.Users = users;

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserForm user)
        {
            if (ModelState.IsValid)
            {
                var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
                var request = new RestRequest("user", Method.POST);
                request.AddJsonBody(MVCToLogic(user));
                var response = client.Execute(request);
                Console.WriteLine(request.ToString());
                return RedirectToAction("Register");
            }

            return View(user);
        }

        [HttpPost]
        public JsonResult doesUserNameExist(string Username)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user", Method.POST);
            request.AddParameter("username", Username);


            var content = client.Execute(request).Content;
            var user = JsonConvert.DeserializeObject<UserLogic>(content);
            return Json(!(user == null));

        }

        [HttpPost]
        public JsonResult doesEmailExist(string Email)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user", Method.POST);
            request.AddParameter("email", Email);

            var content = client.Execute(request).Content;
            var user = JsonConvert.DeserializeObject<UserLogic>(content);

            return Json(!(user == null));
        }

        private UserLogic MVCToLogic(UserForm userMVC)
        {
            var userLogic = new UserLogic();

            userLogic.Email = userMVC.Email;
            userLogic.Username = userMVC.Username;
            userLogic.Password = userMVC.Password;

            return userLogic;
        }
    }
}