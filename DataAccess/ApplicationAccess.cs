using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ApplicationAccess
    {
        BandItEntities db;

        public ApplicationAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public void Add(Application application)
        {
            application.Sent = DateTime.Now;
            application.Result = null;
            db.Users.SingleOrDefault(x => x.ID == application.UserID).Applications.Add(application);
            db.Bands.SingleOrDefault(x => x.ID == application.BandID).Applications.Add(application);
            db.Applications.Add(application);
            db.SaveChanges();
        }

        public Application Get (int id)
        {
            var application = db.Applications
                .Where(a => a.ID == id)
                .Where(a => a.Deleted == null)
                .FirstOrDefault<Application>();
            return application;
        }

        public List<Application> Get()
        {
            var applications = db.Applications
                .Where(a => a.Deleted == null)
                .OrderByDescending(a => a.ID);
            return applications.ToList();
        }
        
        public void Accept(int id)
        {
            var application = db.Applications.SingleOrDefault(a => a.ID == id);
            application.Result = true;
            db.SaveChanges();
        }

        public void Decline(int id)
        {
            var application = db.Applications.SingleOrDefault(a => a.ID == id);
            application.Result = false;
            db.SaveChanges();
        }

        public Application Get (string message)
        {
            var application = db.Applications
                .Where(a => a.Message.StartsWith(message) && a.Message.EndsWith(message))
                .Where(a => a.Deleted == null)
                .FirstOrDefault<Application>();
            return application;
        }
    }
}
