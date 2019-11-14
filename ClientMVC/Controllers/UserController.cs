using System;
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
    }
}