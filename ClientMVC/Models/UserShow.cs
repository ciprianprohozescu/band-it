using System;
using System.Collections.Generic;
using System.Linq;

using Models;

namespace ClientMVC.Models
{
    public class UserShow
    {
        public User User { get; set; }
        public string StorageLocation { get; set; }
        public List<File> Images { get; set; }
        public List<File> Music { get; set; }
        public List<Skill> Skills { get; set; }
        public int LoggedInUserId { get; set; }
    }
}