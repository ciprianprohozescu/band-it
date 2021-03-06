using ClientMVC.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Band = Models.Band;
using User = Models.User;
using Application = Models.Application;

namespace ClientMVC.Controllers
{
    public class BandController : Controller
    {
        [HttpGet]
        public ActionResult Index(string search, double distance = -1, double markerLat = 0.0, double markerLng = 0.0)
        {
            if(Session["ID"] == null)
            {
                return View("Error");
            }

            var model = new BandIndex();

            model.Search = search;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("band", Method.GET);

            request.AddParameter("search", search);

            if (distance > -1)
            {
                request.AddParameter("distance", distance);
                request.AddParameter("markerLat", markerLat);
                request.AddParameter("markerLng", markerLng);

                model.Distance = distance;
                model.DistanceEnabled = true;
            }
            else
            {
                model.Distance = -1;
                model.DistanceEnabled = false;
            }

            var content = client.Execute(request).Content;
            var bands = JsonConvert.DeserializeObject<List<Band>>(content);

            model.Bands = bands;

            request = new RestRequest($"user/{Session["ID"]}", Method.GET);
            content = client.Execute(request).Content;
            var user = JsonConvert.DeserializeObject<User>(content);

            model.User = user;

            return View(model);
        }

        [HttpGet]
        public ActionResult Show(int id)
        {
            if (Session["ID"] == null)
            {
                return View("Error");
            }

            var model = new BandShow();

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"band/{id}", Method.GET); ;

            var content = client.Execute(request);
            model.Band = JsonConvert.DeserializeObject<Band>(content.Content);

            return View(model);
        }

        public ActionResult Create()
        {
            if (Session["ID"] == null)
            {
                return View("Error");
            }

            ViewBag.Title = $"Create a new band";

            var model = new BandForm();
            model.Action = $"register";
            model.Band = new Band();

            return View("Form", model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["ID"] == null)
            {
                return View("Error");
            }

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"band/{id}", Method.GET);
            var content = client.Execute(request).Content;

            var band = JsonConvert.DeserializeObject<Band>(content);

            if (band == null) //TODO: add check that user is member of band
            {
                return View("Error");
            }

            ViewBag.Title = $"Edit {band.Name}";

            var model = new BandForm();
            model.Action = $"update/{id}";
            model.Band = band;

            return View("Form", model);
        }

        [HttpPost]
        public ActionResult Register(string name, string description, string inviteMessage)
        {
            if (Session["ID"] == null)
            {
                return View("Error");
            }

            var band = new Band();
            band.Name = name;
            band.Description = description;
            band.InviteMessage = inviteMessage;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));

            var request = new RestRequest("/band/register", Method.POST);
            request.AddJsonBody(band);

            var content = client.Execute(request).Content;
            var responseBand = JsonConvert.DeserializeObject<Band>(content);

            if (responseBand.NameError == "")
            {
                return RedirectToAction("Index", "Band");
            }

            var model = new BandForm();
            model.Action = $"register";
            model.Band = responseBand;

            return View("Form", model);
        }

        [HttpPost]
        public ActionResult Update(int id, long rowVersion, string name, string description, string inviteMessage)
        {
            if (Session["ID"] == null)
            {
                return View("Error");
            }

            var band = new Band();
            band.ID = id;
            band.RowVersion = rowVersion;
            band.Name = name;
            band.Description = description;
            band.InviteMessage = inviteMessage;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));

            var request = new RestRequest($"band/update", Method.POST);
            request.AddJsonBody(band);

            var content = client.Execute(request).Content;
            var responseBand = JsonConvert.DeserializeObject<Band>(content);

            var model = new BandForm();
            model.Action = $"update/{id}";
            model.Band = responseBand;

            return View("Form", model);
        }
    }
}