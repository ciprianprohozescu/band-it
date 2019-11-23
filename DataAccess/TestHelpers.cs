using ModelsDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TestHelpers
    {
        BandItEntities db;

        public TestHelpers()
        {
            db = new BandItEntities();
        }
        public void ClearData()
        {
            db.Users.RemoveRange(db.Users.ToList());
            db.SaveChanges();
        }
        public void InsertTestData()
        {
            var user = new User();

            user.Username = "Andrei1337";
            user.Email = "andrei@gmail.com";
            user.Password = "1234";
            user.Salt = "sgsefwer";
            user.FirstName = "Andrei";
            user.LastName = "Mataoanu";
            user.Latitude = (decimal)57.006620;
            user.Longitude = (decimal)9.879727;

            db.Users.Add(user);

            user = new User();

            user.Username = "Ciprian1337";
            user.Email = "ciprian@gmail.com";
            user.Password = "1234";
            user.Salt = "sgsefwer";
            user.FirstName = "Ciprian";
            user.LastName = "Prohozescu";
            user.Latitude = (decimal)52.006620;
            user.Longitude = (decimal)14.879727;

            db.Users.Add(user);

            user = new User();

            user.Username = "Radu1337";
            user.Email = "radu@gmail.com";
            user.Password = "1234";
            user.Salt = "sgsefwer";
            user.FirstName = "Radu";
            user.LastName = "Matusa";
            user.Latitude = (decimal)53.003310;
            user.Longitude = (decimal)13.982130;

            db.Users.Add(user);

            db.SaveChanges();
        }
    }
}
