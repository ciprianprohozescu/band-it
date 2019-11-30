using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Models
{
    public class User
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public LatLng Location { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime Deleted { get; set; }
        public List<Skill> Skills { get; set; }
        public List<string> Files { get; set; }
    }
}
