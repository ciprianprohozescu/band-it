using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using User = Models.User;

namespace ClientMVC.Models
{
    public class UserIndex
    {
        public string Search { get; set; }
        public double Distance { get; set; }
        public bool DistanceEnabled { get; set; }
        public List<User> Users { get; set; }
    }
}