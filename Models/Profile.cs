using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [DataContract]
    class Profile
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public decimal Longitude { get; set; }
        [DataMember]
        public decimal Latitude { get; set; }
        
        public Profile (int id, string fName, string lName, decimal longitude, decimal latidude)
        {
            this.ID = id;
            this.FirstName = fName;
            this.LastName = lName;
            this.Longitude = longitude;
            this.Latitude = latidude;
        }

        
    }

}
