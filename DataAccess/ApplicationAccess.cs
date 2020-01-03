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
        BandItContext db;

        public ApplicationAccess()
        {
            db = ContextProvider.Instance.DB;
        }

        public void Add(Applications application)
        {
            application.Sent = DateTime.Now;
            application.Result = null;
            db.Users.SingleOrDefault(x => x.Id == application.UserId).Applications.Add(application);
            db.Bands.SingleOrDefault(x => x.Id == application.BandId).Applications.Add(application);
            db.Applications.Add(application);
            db.SaveChanges();
        }

        public Applications Get (int id)
        {
            var application = db.Applications
                .Where(a => a.Id == id)
                .Where(a => a.Deleted == null)
                .FirstOrDefault<Applications>();
            return application;
        }

        public List<Applications> Get()
        {
            var applications = db.Applications
                .Where(a => a.Deleted == null)
                .OrderByDescending(a => a.Id);
            return applications.ToList();
        }
        
        public void Accept(int id)
        {
            var application = db.Applications.SingleOrDefault(a => a.Id == id);
            application.Result = true;
            db.SaveChanges();
        }

        public void Decline(int id)
        {
            var application = db.Applications.SingleOrDefault(a => a.Id == id);
            application.Result = false;
            db.SaveChanges();
        }

        public Applications Get (string message)
        {
            var application = db.Applications
                .Where(a => a.Message.StartsWith(message) && a.Message.EndsWith(message))
                .Where(a => a.Deleted == null)
                .FirstOrDefault<Applications>();
            return application;
        }
    }
}
