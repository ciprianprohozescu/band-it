using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Notifications
    {
        public int Id { get; set; }
        public int? BandId { get; set; }
        public int? UserId { get; set; }
        public int TypeId { get; set; }
        public DateTime Sent { get; set; }
        public bool Status { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Bands Band { get; set; }
        public virtual NotificationTypes Type { get; set; }
        public virtual Users User { get; set; }
    }
}
