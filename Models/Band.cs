﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Band
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameError { get; set; }
        public string Description { get; set; }
        public LatLng Location { get; set; }
        public string ProfilePicture { get; set; }
        public string InviteMessage { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Application> Applications { get; set; }
        public List<BandUser> BandUsers { get; set; }
        public long RowVersion { get; set; }
        public string RowVersionError { get; set; }
        public DateTime Deleted { get; set; }
    }
}
