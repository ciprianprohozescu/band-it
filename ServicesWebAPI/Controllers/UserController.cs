﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Controllers;
using UserControllerLogic = Controllers.UserController;
using Models;

namespace ServicesWebAPI.Controllers
{
    public class UserController : ApiController
    {
        [Route("api/user/register")]
        public void Post(User user)
        {
            IUserController userController = new UserControllerLogic ();
            userController.Add(user);           
        }

        [Route("api/user/username")]
        public User GetUsername(string username)
        {
            IUserController userController = new UserControllerLogic();
            return userController.GetByUsername(username);
        }

        [Route("api/user/email")]
        public User GetEmail(string email)
        {
            IUserController userController = new UserControllerLogic();
            return userController.GetByEmail(email);
        }

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

        [Route("api/delete/{id}")]
        public User Delete(int id)
        {
            IUserController userController = new UserControllerLogic();
            userController.Delete(id);

            return userController.Get(id);
        }

        [Route("api/user/{id}")]
        public User Get(int id)
        {
            IUserController userController = new UserControllerLogic();
            return userController.GetById(id);
        }
    }
}