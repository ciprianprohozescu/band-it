using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Salt { get; set; }

        public User(int id, string email, string userName, string password, string salt)
        {
            this.ID = id;
            this.Email = email;
            this.UserName = userName;
            this.Password = password;
            this.Salt = salt;
        }
    }
}