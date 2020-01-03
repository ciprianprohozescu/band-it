using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Controllers;
using UserControllerLogic = Controllers.UserController;
using Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace ServicesWebAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserController userController;
        IFileController fileController;

        public UserController()
        {
            userController = new UserControllerLogic();
            fileController = new FileController();
        }
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

        [Route("api/user/delete/{id}")]
        public User Delete(int id)
        {
            userController.Delete(id);

            return userController.GetById(id);
        }

        [Route("api/user/{id}")]
        public User Get(int id)
        {
            IUserController userController = new UserControllerLogic();
            return userController.GetById(id);
        }

        [Route("api/user/login")]
        [HttpGet]
        public User LogIn(string username, string password)
        {
            return userController.LogIn(username, password);
        }

        [Route("api/user/update/profilepicture")]
        [HttpPut]
        public void UpdateProfilePicture(User user)
        {
            userController.UpdateProfilePicture(user.ID, user.ProfilePicture);
        }

        [Route("api/user/update")]
        [HttpPost]
        public User Update(User user)
        {
            return userController.Update(user);
        }

        [Route("api/user/add/file")]
        [HttpPost]
        public void AddFile(User user)
        {
            fileController.SaveFile("user", user.ID, user.Files[0]);
        }

        [Route("api/user/delete/file/{id}")]
        [HttpDelete]
        public void DeleteFile(int id)
        {
            fileController.DeleteFile("user", id);
        }

        [Route("api/user/savelocation")]
        [HttpPut]
        public void SaveLocation(User user)
        {
            userController.SaveLocation(user);
        }
    }
}