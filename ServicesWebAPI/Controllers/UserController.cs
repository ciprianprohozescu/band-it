using System;
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
        public void Post(User user)
        {
            IUserController userController = new UserControllerLogic ();
            userController.Add(user);           
        }

        public void GetUsername(String username)
        {
            IUserController userController = new UserControllerLogic();
            userController.GetByUsername(username);
        }
        public void GetEmail(String email)
        {
            IUserController userController = new UserControllerLogic();
            userController.GetByEmail(email);
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

        public void Delete(int id)
        {
            IUserController userController = new UserControllerLogic();
            userController.Delete(id);

        }

        [Route("api/user/{id}")]
        public User Get(int id)
        {
            IUserController userController = new UserControllerLogic();
            return userController.GetByID(id);
        }
    }
}