using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class BandUserSkills
    {
        public int BandUserId { get; set; }
        public int SkillId { get; set; }

        public virtual BandUsers BandUser { get; set; }
        public virtual Skills Skill { get; set; }
    }
}
