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
using Models;

namespace ClientMVC.Controllers
{
    public class UserController : Controller
    {

        [HttpGet]
        public ActionResult Index(string search, double distance = -1)
        {
            if (Session["ID"] == null)
            {
                return View("Error");
            }

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));

            int userId = (int)Session["ID"];
            
            var model = new UserIndex();

            var userRequest = new RestRequest($"user/{userId}", Method.GET);
            model.User = JsonConvert.DeserializeObject<User>(client.Execute(userRequest).Content);

            model.Search = search;

            var request = new RestRequest("user", Method.GET);
            request.AddParameter("search", search);

            if (distance > -1)
            {
                request.AddParameter("distance", distance);
                request.AddParameter("markerLat", model.User.Location.Latitude);
                request.AddParameter("markerLng", model.User.Location.Longitude);

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
                if (user.ID != model.User.ID)
                {
                    model.Users.Add(user);
                }
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

            model.Music = fileController.GetMusic(model.User.Files);
            for (var i = 0; i < model.Music.Count; ++i)
            {
                model.Music[i].Name = storageLocation + model.Music[i].Name;
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
        public ActionResult SaveFile(int id, string type, HttpPostedFileBase file)
        {
            if (Session["ID"] == null || (int)Session["ID"] != id)
            {
                return View("Error");
            }

            var fileController = new FileController();
            fileController.SaveFile("user", id, type, file);

            return RedirectToAction($"Show/{id}");
        }

        [HttpPost]
        public ActionResult SaveLocation(int id, double lng, double lat)
        {
            if (Session["ID"] == null || (int)Session["ID"] != id)
            {
                return View("Error");
            }

            User user = new User();
            user.ID = id;
            user.Location = new LatLng(lat, lng);

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));

            var request = new RestRequest("user/savelocation", Method.PUT);
            request.AddJsonBody(user);

            client.Execute(request);

            return RedirectToAction($"Show/{id}");
        }

        [HttpPost]
        public ActionResult DeleteFile(int userID, int fileID, string fileName)
        {
            var fileController = new FileController();
            fileController.DeleteFile("user", userID, fileID, fileName);

            return RedirectToAction($"Show/{userID}");
        }
    }
}