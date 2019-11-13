using System;
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
        public List<User> Get(string search = "")
        {
            IUserController userController = new UserControllerLogic();
            var users = userController.Get(search);

            return users;
        }
    }
}
