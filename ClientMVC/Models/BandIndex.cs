using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace ClientMVC.Models
{
    public class BandIndex
    {
        public string Search { get; set; }
        public double Distance { get; set; }
        public bool DistanceEnabled { get; set; }
        public List<Band> Bands { get; set; }
        public List<Application> Applications { get; set; }
        public User User { get; set; }
    }
}