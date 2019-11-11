        public string Description { get; set; }
﻿using System;
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

        public User(int id, string email, string username, string password, string salt)
        {
            this.ID = id;
            this.Email = email;
            this.Username = username;
            this.Password = password;
            this.Salt = salt;
        }
        public User() { }
    }
}
