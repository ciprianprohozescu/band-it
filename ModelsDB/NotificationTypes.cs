using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class NotificationTypes
    {
        public NotificationTypes()
        {
            Notifications = new HashSet<Notifications>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<Notifications> Notifications { get; set; }
    }
}
