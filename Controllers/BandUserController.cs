using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BandUserDB = ModelsDB.BandUsers;

namespace Controllers
{
    public class BandUserController : IBandUserController
    {
        public void Add(int userID, int bandID)
        {
            BandUserAccess bandUserAccess = new BandUserAccess();
            BandUser bandUser = new BandUser();
            bandUser.UserID = userID;
            bandUser.BandID = bandID;

            bandUserAccess.Add(LogicToDB(bandUser));
        }

        public BandUser DBToLogic(BandUserDB bandUserDB)
        {
            BandUser bandUser = new BandUser();
            if(bandUserDB != null)
            {
                bandUser.ID = bandUserDB.Id;
                bandUser.BandID = (int)bandUserDB.BandId;
                bandUser.UserID = (int)bandUserDB.UserId;

                return bandUser;
            }

            return null;
        }

        private BandUserDB LogicToDB(BandUser bandUser)
        {
            var bandUserDB = new BandUserDB();
            if (bandUser != null)
            {
                bandUserDB.Id = bandUser.ID;
                bandUserDB.BandId = bandUser.BandID;
                bandUserDB.UserId = bandUser.UserID;

                return bandUserDB;
            }

            return null;
        }
    }
}
