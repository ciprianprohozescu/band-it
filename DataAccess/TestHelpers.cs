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
            db.Skills.RemoveRange(db.Skills.ToList());
            db.Bands.RemoveRange(db.Bands.ToList());
            db.Genres.RemoveRange(db.Genres.ToList());
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

            var skill = new Skill();
            skill.Name = "Vocalist";

            db.Skills.Add(skill);

            skill = new Skill();
            skill.Name = "Triangle";

            db.Skills.Add(skill);

            var band = new Band();
            band.Name = "Poleyn";
            band.Description = "This is the description of Poleyn";
            band.Latitude = (decimal)56.976405;
            band.Longitude = (decimal)9.910906;

            db.Bands.Add(band);

            band = new Band();
            band.Name = "Dansk Rap";
            band.Description = "This is the description of Dansk Rap";
            band.Latitude = (decimal)57.050832;
            band.Longitude = (decimal)9.910391;

            db.Bands.Add(band);

            band = new Band();
            band.Name = "LaLaLa";
            band.Description = "This is the description of LaLaLa";
            band.Latitude = (decimal)55.708916;
            band.Longitude = (decimal)12.483776;

            db.Bands.Add(band);

            var genre = new Genre();
            genre.Name = "Metal";

            db.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Rap";

            db.Genres.Add(genre);

            genre = new Genre();
            genre.Name = "Jazz";

            db.Genres.Add(genre);

            db.SaveChanges();
        }
    }
}
