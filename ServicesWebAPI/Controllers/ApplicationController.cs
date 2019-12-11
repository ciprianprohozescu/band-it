using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApplicationControllerLogic = Controllers.ApplicationController;
using Models;
using Controllers;

namespace ServicesWebAPI.Controllers
{
    public class ApplicationController : ApiController  
    {

        [Route("api/application")]
        public void Post(Application application)
        {
            IApplicationController applicationController = new ApplicationControllerLogic();
            applicationController.Add(application);
        }

        [Route("api/application")]
        public List<Application> Get()
        {
            IApplicationController applicationController = new ApplicationControllerLogic();
            return applicationController.Get();
        }

        [Route("api/application/accept/{applicationID}/{userID}/{bandID}")]
        public void Accept(int applicationID, int userID, int bandID)
        {
            IApplicationController applicationController = new ApplicationControllerLogic();
            applicationController.Accept(applicationID, userID, bandID);
        }

        [Route("api/application/decline/{id}")]
        public void Decline(int id)
        {
            IApplicationController applicationController = new ApplicationControllerLogic();
            applicationController.Decline(id);
        }
    }
}
