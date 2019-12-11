using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDB = ModelsDB.Application;

namespace Controllers
{
    public interface IApplicationController
    {
        void Add(Application application);
        List<Application> Get();
        void Decline(int id);
        void Accept(int applicationID, int userID, int bandID);
        Application DBToLogic(ApplicationDB applicationDB);
    }
}
