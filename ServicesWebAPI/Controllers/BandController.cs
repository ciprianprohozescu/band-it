﻿using System;
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
    }
}