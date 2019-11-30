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
            if (Session["ID"] == null)
            {
                return View("Error");
            }

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
            if (Session["ID"] == null)
            {
                return View("Error");
            }

            var model = new UserShow();

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"user/{id}", Method.GET);

            var content = client.Execute(request);
            model.User = JsonConvert.DeserializeObject<User>(content.Content);

            var storageLocation = $"/Content/Uploads/Users/{id}/";

            if (model.User.ProfilePicture != null)
            {
                model.User.ProfilePicture = storageLocation + model.User.ProfilePicture;
            }

            var fileController = new FileController();

            model.Images = fileController.GetImages(model.User.Files);
            for (var i = 0; i < model.Images.Count; ++i)
            {
                model.Images[i].Name = storageLocation + model.Images[i].Name;
            }
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserForm user)
        {
            if (ModelState.IsValid)
            {
                var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
                var request = new RestRequest("/user/register", Method.POST);
                request.AddJsonBody(MVCToLogic(user));
                var response = client.Execute(request);
                Console.WriteLine(request.ToString());
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public JsonResult DoesUserNameExist(string Username)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user/username", Method.GET);
            request.AddParameter("username", Username);


            var content = client.Execute(request).Content;
            var user = JsonConvert.DeserializeObject<User>(content);
            return Json(user == null);

        }

        [HttpPost]
        public JsonResult doesEmailExist(string Email)
        {
            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("user/email", Method.GET);
            request.AddParameter("email", Email);

            var content = client.Execute(request);
            var user = JsonConvert.DeserializeObject<User>(content.Content);
            return Json(user == null);
        }

        private User MVCToLogic(UserForm userMVC)
        {
            var userLogic = new User();

            userLogic.Email = userMVC.Email;
            userLogic.Username = userMVC.Username;
            userLogic.Password = userMVC.Password;

            return userLogic;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["ID"] == null || (int)Session["ID"] != id)
            {
                return View("Error");
            }

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"user/delete/{id}", Method.DELETE);
            client.Execute(request);

            return RedirectToAction("LogOut", "Home");
        }

        [HttpPost]
        public ActionResult SaveProfilePicture(int id, HttpPostedFileBase profilePicture)
        {
            if (Session["ID"] == null || (int)Session["ID"] != id)
            {
                return View("Error");
            }

            var fileController = new FileController();
            fileController.SaveFile("user", id, "profilePicture", profilePicture);

            return RedirectToAction($"Show/{id}");
        }

        [HttpPost]
        public ActionResult SaveImage(int id, HttpPostedFileBase image)
        {
            if (Session["ID"] == null || (int)Session["ID"] != id)
            {
                return View("Error");
            }

            var fileController = new FileController();
            fileController.SaveFile("user", id, "image", image);

            return RedirectToAction($"Show/{id}");
        }
    }
}