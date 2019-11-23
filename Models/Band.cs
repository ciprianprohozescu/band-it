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
        public string Description { get; set; }
        public LatLng Location { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime Deleted { get; set; }
    }
}