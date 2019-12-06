using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public interface IBandController
    {
        List<Band> Get(string search, double distance = -1, double markerLat = 0, double markerLng = 0);
        Band GetById(int id);
        Band GetByName(string name);
        Band Update(Band band);
    }
}
