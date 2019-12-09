using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BandUserDB = ModelsDB.BandUser;

namespace Controllers
{
    interface IBandUserController
    {
        BandUser DBToLogic(BandUserDB bandUserDB);
        void Add(int userID, int bandID);
    }
}
