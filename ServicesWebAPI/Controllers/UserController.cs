﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Models;
using Controllers;
using UserControllerLogic = Controllers.UserController;

namespace ServicesWebAPI.Controllers
{
    public class UserController : ApiController
    {
        [Route("api/user")]
        public List<User> Get(string search = "", double distance = -1, double markerLat = 0, double markerLng = 0)
        {
            IUserController userController = new UserControllerLogic();

            if (distance > -1)
            {
                return userController.Get(search, distance, markerLat, markerLng);
            }
            return userController.Get(search);
        }
    }
}
