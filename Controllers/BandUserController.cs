using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BandUserDB = ModelsDB.BandUser;

namespace Controllers
{
    public class BandUserController : IBandUserController
    {
        public void Add(int userID, int bandID)
        {
            var bandUserAccess = new BandUserAccess();
            var bandUser = new BandUser();
            bandUser.UserID = userID;
            bandUser.BandID = bandID;

            bandUserAccess.Add(LogicToDB(bandUser));
        }

        public BandUser DBToLogic(BandUserDB bandUserDB)
        {
            var bandUser = new BandUser();
            if(bandUserDB != null)
            {
                bandUser.ID = bandUserDB.ID;
                bandUser.BandID = (int)bandUserDB.BandID;
                bandUser.UserID = (int)bandUserDB.UserID;

                return bandUser;
            }

            return null;
        }

        private BandUserDB LogicToDB(BandUser bandUser)
        {
            var bandUserDB = new BandUserDB();
            if (bandUser != null)
            {
                bandUserDB.ID = bandUser.ID;
                bandUserDB.BandID = bandUser.BandID;
                bandUserDB.UserID = bandUser.UserID;

                return bandUserDB;
            }

            return null;
        }
    }
}
