using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Bands
    {
        public Bands()
        {
            Applications = new HashSet<Applications>();
            BandGenres = new HashSet<BandGenres>();
            BandMessages = new HashSet<BandMessages>();
            BandUsers = new HashSet<BandUsers>();
            Invites = new HashSet<Invites>();
            Notifications = new HashSet<Notifications>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string ProfilePicture { get; set; }
        public string InviteMessage { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<Applications> Applications { get; set; }
        public virtual ICollection<BandGenres> BandGenres { get; set; }
        public virtual ICollection<BandMessages> BandMessages { get; set; }
        public virtual ICollection<BandUsers> BandUsers { get; set; }
        public virtual ICollection<Invites> Invites { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
    }
}
