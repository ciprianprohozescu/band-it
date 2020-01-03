using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BandControllerLogic = Controllers.BandController;
using Models;
using Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ServicesWebAPI.Controllers
{
    [ApiController]
    public class BandController : ControllerBase
    {

        IBandController bandController;

        public BandController()
        {
            bandController = new BandControllerLogic();
        }

        [Route("api/band")]
        public List<Band> Get(string search = "", double distance = -1, double markerLat = 0, double markerLng = 0)
        {
            if (distance > -1)
            {
                return bandController.Get(search, distance, markerLat, markerLng);
            }
            return bandController.Get(search);
        }

        [Route("api/band/{id}")]
        public Band Get(int id)
        {
            return bandController.GetById(id);
        }

        [Route("api/band/update")]
        [HttpPost]
        public Band Update(Band band)
        {
            return bandController.Update(band);
        }

        [Route("api/band/register")]
        public Band Post(Band band)
        {
            return bandController.Add(band);
        }

        [Route("api/band/name")]
        public Band GetByName(string name)
        {
            return bandController.GetByName(name);
        }
    }
}
