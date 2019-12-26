using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Permissions
    {
        public Permissions()
        {
            BandUsers = new HashSet<BandUsers>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<BandUsers> BandUsers { get; set; }
    }
}
