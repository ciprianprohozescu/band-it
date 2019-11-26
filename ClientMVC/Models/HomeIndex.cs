using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Models;

namespace ClientMVC.Models
{
    public class HomeIndex
    {
        public bool LogInFailed { get; set; }
        public User User { get; set; }
    }
}