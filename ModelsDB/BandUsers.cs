using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class BandUsers
    {
        public BandUsers()
        {
            BandUserSkills = new HashSet<BandUserSkills>();
        }

        public int Id { get; set; }
        public int? PermisionId { get; set; }
        public int? UserId { get; set; }
        public int? BandId { get; set; }

        public virtual Bands Band { get; set; }
        public virtual Permissions Permision { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<BandUserSkills> BandUserSkills { get; set; }
    }
}
