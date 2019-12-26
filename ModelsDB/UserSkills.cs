using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class UserSkills
    {
        public int UserId { get; set; }
        public int SkillId { get; set; }

        public virtual Skills Skill { get; set; }
        public virtual Users User { get; set; }
    }
}
