using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Models;
using ApplicationDB = ModelsDB.Applications;

namespace Controllers
{
    public class ApplicationController : IApplicationController
    {
        ApplicationAccess applicationAccess;
        IBandUserController bandUserController;

        public ApplicationController()
        {
            applicationAccess = new ApplicationAccess();
            bandUserController = new BandUserController();
        }

        public void Add(Application application)
        {
            var applicationAccess = new ApplicationAccess();
            var applicationDB = applicationAccess.Get(application.ID);
            if(applicationDB == null)
            {
                applicationAccess.Add(LogicToDB(application));
            }
        }

        public Application Get(int id)
        {
            var applicationAccess = new ApplicationAccess();
            var applicationDB = applicationAccess.Get(id);
            return DBToLogic(applicationDB);
        }

        public List<Application> Get()
        {
            var applicationAccess = new ApplicationAccess();
            var applicationsDB = applicationAccess.Get();
            var applicationsLogic = new List<Application>();

            for (int i = 0; i < applicationsDB.Count; i++)
            {
                applicationsLogic.Add(DBToLogic(applicationsDB[i]));
            }

            return applicationsLogic;
        }

        public void Accept(int applicationID, int userID, int bandID)
        {
            applicationAccess.Accept(applicationID);
            bandUserController.Add(userID, bandID);
        }

        public void Decline(int id)
        {
            applicationAccess.Decline(id);
        }

        public Application DBToLogic(ApplicationDB applicationDB)
        {
            var application = new Application();
            if(applicationDB != null)
            {
                application.ID = applicationDB.Id;
                application.Sent = applicationDB.Sent;
                application.Message = applicationDB.Message;
                application.Result = applicationDB.Result;
                application.BandID = applicationDB.BandId;
                application.UserID = applicationDB.UserId;

                return application;
            }

            return null;
        }

        private ApplicationDB LogicToDB(Application application)
        {
            var applicationDB = new ApplicationDB();
            if(application != null)
            {
                applicationDB.Id = application.ID;
                applicationDB.Sent = application.Sent;
                applicationDB.Message = application.Message;
                applicationDB.Result = application.Result;
                applicationDB.BandId = application.BandID;
                applicationDB.UserId = application.UserID;

                return applicationDB;
            }

            return null;

        }
    }
}
