using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BandControllerLogic = Controllers.BandController;
using Models;
using Controllers;

namespace ServicesWebAPI.Controllers
{
    public class BandController : ApiController
    {
        IBandController bandController;
        public BandController()
        {
            bandController = new BandControllerLogic();
        }
        [Route("api/band")]
        public List<Band> Get(string search = "", double distance = -1, double markerLat = 0, double markerLng = 0)
        {
            IBandController bandController = new BandControllerLogic();
            if (distance > -1)
            {
                return bandController.Get(search, distance, markerLat, markerLng);
            }
            return bandController.Get(search);
        }

        [Route("api/band/{id}")]
        public Band Get(int id)
        {
            IBandController bandController = new BandControllerLogic();
            return bandController.GetById(id);
        }

        [Route("api/band/update")]
        [HttpPost]
        public Band Update(Band band)
        {
            IBandController bandController = new BandControllerLogic();
            var newBand = bandController.Update(band);

            return newBand;
        }

        [Route("api/band/register")]
        public void Post(Band band)
        {
            IBandController bandController = new BandControllerLogic();
            bandController.Add(band);
        }

        [Route("api/band/name")]
        public Band GetByName(string name)
        {
            IBandController bandController = new BandControllerLogic();
            return bandController.GetByName(name);
        }
    }
}
