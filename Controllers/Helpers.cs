using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Models;

namespace Controllers
{
    public static class Helpers
    {
        /// <summary>
        /// Convert an angle from degrees to radians.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double ToRadians(this double angle)
        {
            return (Math.PI / 180) * angle;
        }

        /// <summary>
        /// Calculate the distance between 2 GPS coordinates.
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public static double HaversineDistance(LatLng pos1, LatLng pos2)
        {
            double R = 6371;
            var lat = (pos2.Latitude - pos1.Latitude).ToRadians();
            var lng = (pos2.Longitude - pos1.Longitude).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(pos1.Latitude.ToRadians()) * Math.Cos(pos2.Latitude.ToRadians()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }
    }
}
