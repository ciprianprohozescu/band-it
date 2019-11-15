﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ClientMVC.Models;
using User = Models.User;

using RestSharp;
using Newtonsoft.Json;

namespace ClientMVC.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Index(string search, double distance = -1, double markerLat = 0.0, double markerLng = 0.0)
        {
            var model = new UserIndex();

            model.Search = search;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user", Method.GET);

            if (distance > -1)
            {
                request.AddParameter("search", search);
                request.AddParameter("distance", distance);
                request.AddParameter("markerLat", markerLat);
                request.AddParameter("markerLng", markerLng);

                model.Distance = distance;
                model.DistanceEnabled = true;
            } else
            {
                model.Distance = -1;
                model.DistanceEnabled = false;
            }
            
            var content = client.Execute(request).Content;
            var users = JsonConvert.DeserializeObject<List<User>>(content);

            model.Users = new List<User>();
            foreach (var user in users)
            {
                model.Users.Add(user);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            var model = new UserShow();

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"user/{id}", Method.GET);

            var content = client.Execute(request);
            model.User = JsonConvert.DeserializeObject<User>(content.Content);

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserMVC user)
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
        public JsonResult doesUserNameExist(string UserName)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user", Method.POST);
            var content = client.Execute(request).Content;
            request.AddParameter("username", UserName);

            var user = JsonConvert.DeserializeObject<User>(content);
            return Json(!(user == null));

        }

        [HttpPost]
        public JsonResult doesEmailExist(string Email)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user", Method.POST);
            request.AddParameter("email", Email);

            var content = client.Execute(request).Content;
            var user = JsonConvert.DeserializeObject<User>(content);

            return Json(!(user == null));
        }

        private UserLogic MVCToLogic(UserMVC userMVC)
        {
            var userLogic = new UserLogic();

            userLogic.Email = userMVC.Email;
            userLogic.Username = userMVC.Username;
            userLogic.Password = userMVC.Password;

            return userLogic;
        }
    }
}