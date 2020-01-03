using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClientMVC.Models
{
    public class BandShow
    {
        public Band Band { get; set; }
        public int LoggedInUserId { get; set; }
    }
}