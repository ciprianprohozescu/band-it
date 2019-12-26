using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Messages
    {
        public Messages()
        {
            BandMessages = new HashSet<BandMessages>();
            UserMessages = new HashSet<UserMessages>();
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public DateTime Sent { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Users Sender { get; set; }
        public virtual ICollection<BandMessages> BandMessages { get; set; }
        public virtual ICollection<UserMessages> UserMessages { get; set; }
    }
}
