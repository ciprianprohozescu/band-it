using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserLogic = Models.User;
using Controllers;

namespace ServicesWebAPI.Controllers
{
    public class UserController : ApiController
    {
        public List<UserLogic> Get(string search = "")
        {
            var userController = new UserController();
            var users = userController.Get(search);
            return users;
        }
    }
}
