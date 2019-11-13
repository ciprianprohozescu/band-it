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
        public List<User> Users { get; set; }
    }
}