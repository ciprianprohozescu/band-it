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
        public String Description { get; set; }
        [DataMember]
        public decimal Longitude { get; set; }
        [DataMember]
        public decimal Latitude { get; set; }
        
        public Profile (int id, string firstName, string lastName, string description, decimal longitude, decimal latitude )
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Description = description;
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

    }
}
