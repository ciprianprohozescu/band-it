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

namespace ClientMVC.Controllers
{
    public class BandController : Controller
    {
        [HttpGet]
        public ActionResult Index(string search, double distance = -1, double markerLat = 0.0, double markerLng = 0.0)
        {
            //if(Session["ID"] == null)
            //{
            //    return View("Error");
            //}

            var model = new BandIndex();

            model.Search = search;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("band", Method.GET);

            if (distance > -1)
            {
                request.AddParameter("search",  search);
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

            model.Bands = new List<Band>();
            foreach (var band in bands)
            {
                model.Bands.Add(band);
            }

            return View(model);
        }
    }
}