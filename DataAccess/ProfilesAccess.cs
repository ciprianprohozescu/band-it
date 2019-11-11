using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProfilesAccess
    {
        BandItEntities db = new BandItEntities();
        public List<Profile> GetAllProfiles()
        {
            var profilesQuery = from profile in db.Profiles
                                orderby profile.ID descending
                                select profile;

            return profilesQuery.ToList<Profile>();
        }
        public Profile FindByName(string firstName, string lastName)
        {
            var profile = db.Profiles
                .Where(x => x.FirstName == firstName && x.LastName == lastName)
                .FirstOrDefault<Profile>();

            return profile;
        }
        public void AddProfile(Profile profile)
        {
            db.Profiles.Add(profile);
            db.SaveChanges();
        }
    }
}
