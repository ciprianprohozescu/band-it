using DataAccess;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BandDB = ModelsDB.Band;

namespace Controllers
{
    public class BandController : IBandController
    {
        BandsAccess bandsAccess;
        IGenreController genreController;

        public BandController()
        {
            bandsAccess = new BandsAccess();
            genreController = new GenreController();
        }
        public List<Band> Get(string search, double distance = -1, double markerLat = 0, double markerLng = 0)
        {
            var bandsDB = bandsAccess.Get(search);
            var bands = new List<Band>();

            bandsDB.ForEach(bandDB => bands.Add(DBToLogic(bandDB)));

            if(distance == -1)
            {
                return bands;
            }

            var marker = new LatLng(markerLat, markerLng);
            var filteredBands = new List<Band>();

            foreach (var band in bands)
            {
                if(band.Location == null)
                {
                    continue;
                }
                var bandDistance = Helpers.HaversineDistance(marker, band.Location);
                if(bandDistance <= distance)
                {
                    filteredBands.Add(band);
                }
            }
            return filteredBands;

        }

        private Band DBToLogic(BandDB bandDB)
        {
            var band = new Band();
            if (bandDB != null)
            {
                band.ID = bandDB.ID;
                band.Name = bandDB.Name;
                band.Description = bandDB.Description;
                if(bandDB.Latitude != null && bandDB.Longitude != null)
                {
                    band.Location = new LatLng((double)bandDB.Latitude, (double)bandDB.Longitude);
                }
                band.ProfilePicture = bandDB.ProfilePicture;
                band.Genres = new List<Genre>();
                foreach (var genre in bandDB.Genres)
                {
                    band.Genres.Add(genreController.DBToLogic(genre));
                }

                return band;
            }

            return null;
        }
    }
}
