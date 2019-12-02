using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Models;

namespace ClientMVC.Models
{
    public class UserShow
    {
        public User User { get; set; }
        public string StorageLocation { get; set; }
        public List<File> Images { get; set; }
        public List<File> Music { get; set; }
    }
}