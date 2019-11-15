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

        public List<User> Get(string search = "")
        {
            IUserController userController = new UserControllerLogic();
            var users = userController.Get(search);

            return users;
        }
    }
}