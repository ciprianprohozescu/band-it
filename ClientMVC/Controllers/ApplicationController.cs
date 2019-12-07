using ClientMVC.Models;
using Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientMVC.Controllers
{
    public class ApplicationController : Controller
    {
       
        [HttpPost]
        public ActionResult Apply(int userID, int bandID, string message)
        {
            var application = new Application();
            application.BandID = bandID;
            application.UserID = userID;
            application.Message = message;

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest("/application", Method.POST);
            request.AddJsonBody(application);
            var response = client.Execute(request);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public ActionResult Accept(int applicationID, int userID, int bandID)
        {
            if (Session["ID"] == null)
            {
                return View("Error");
            }

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"application/accept/" +
                $"{applicationID}/{userID}/{bandID}", Method.POST);
            client.Execute(request);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        [HttpPost]
        public ActionResult Decline(int id)
        {
            if (Session["ID"] == null)
            {
                return View("Error");
            }

            var client = new RestClient(ConfigurationManager.AppSettings.Get("APIURL"));
            var request = new RestRequest($"application/decline/{id}", Method.POST);
            client.Execute(request);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }

        private Application MVCToLogic(ApplicationForm applicationMVC)
        {
            var applicationLogic = new Application();

            if(applicationMVC != null)
            {
                applicationLogic.Message = applicationMVC.Message;
                applicationLogic.BandID = applicationMVC.BandID;
                applicationLogic.UserID = applicationMVC.UserID;

                return applicationLogic;
            }

            return null;
        }
    }
}