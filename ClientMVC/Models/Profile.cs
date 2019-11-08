using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientMVC.Models
{
    public class Profile
    {
           
            public int ID { get; set; }
          
            public string FirstName { get; set; }
        
            public string LastName { get; set; }
           
            public String Description { get; set; }
           
            public decimal Longitude { get; set; }
           
            public decimal Latitude { get; set; }
           
            public string ProfilePicture { get; set; }

        }
}