using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;
using ProfileDB = ModelsDB.Profile;
using ProfileLogic = Models.Profile;

namespace Controllers
{
    class ProfileController
    {
        public List<ProfileLogic> GetAll()
        {
            var profilesAccess = new ProfilesAccess();
            var profileDB = profilesAccess.GetAllProfiles();
            var profileLogic = new List<ProfileLogic>();

            profileDB.ForEach(x => profileLogic.Add(DBToLogic(x)));

            return profileLogic;
        }
        private ProfileLogic DBToLogic(ProfileDB profileDB)
        {
            var profileLogic = new ProfileLogic();

            profileLogic.ID = profileDB.ID;
            profileLogic.UserID = profileDB.UserID;
            profileLogic.FirstName = profileDB.FirstName;
            profileLogic.LastName = profileDB.LastName;
            profileLogic.Description = profileDB.Description;
            profileLogic.Longitude = profileDB.Longitude;
            profileLogic.Latitude = profileDB.Latitude;
            profileLogic.ProfilePicture = profileDB.ProfilePicture;
            profileLogic.Deleted = profileDB.Deleted;

            return profileLogic;
        }
    }
}
