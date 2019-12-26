using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Users
    {
        public Users()
        {
            Applications = new HashSet<Applications>();
            BandUsers = new HashSet<BandUsers>();
            BlocksBlocker = new HashSet<Blocks>();
            BlocksReceiver = new HashSet<Blocks>();
            Invites = new HashSet<Invites>();
            Messages = new HashSet<Messages>();
            Notifications = new HashSet<Notifications>();
            Permissions = new HashSet<Permissions>();
            UserFiles = new HashSet<UserFiles>();
            UserMessages = new HashSet<UserMessages>();
            UserSkills = new HashSet<UserSkills>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<Applications> Applications { get; set; }
        public virtual ICollection<BandUsers> BandUsers { get; set; }
        public virtual ICollection<Blocks> BlocksBlocker { get; set; }
        public virtual ICollection<Blocks> BlocksReceiver { get; set; }
        public virtual ICollection<Invites> Invites { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
        public virtual ICollection<Permissions> Permissions { get; set; }
        public virtual ICollection<UserFiles> UserFiles { get; set; }
        public virtual ICollection<UserMessages> UserMessages { get; set; }
        public virtual ICollection<UserSkills> UserSkills { get; set; }
    }
}
