﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using User = Models.User;
using ClientMVC.Models;

namespace ClientMVC.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index(string search)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user", Method.GET);
            request.AddParameter("search", search);
            var content = client.Execute(request).Content;
            ViewBag.Content = content;

            //var users = JsonConvert.DeserializeObject<List<User>>(content);

            var model = new UserIndex();
            model.Search = search;
            model.Users = new List<User>();

            return View(model);

            //return View(db.Profiles.Where(s => s.FirstName.StartsWith(search)
            //    || s.LastName.StartsWith(search) || search == null).ToList());
        }
    }
}