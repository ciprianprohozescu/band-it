using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Skills
    {
        public Skills()
        {
            BandUserSkills = new HashSet<BandUserSkills>();
            UserSkills = new HashSet<UserSkills>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<BandUserSkills> BandUserSkills { get; set; }
        public virtual ICollection<UserSkills> UserSkills { get; set; }
    }
}
