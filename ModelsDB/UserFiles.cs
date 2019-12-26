using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class UserFiles
    {
        public int UserId { get; set; }
        public int FileId { get; set; }

        public virtual Files File { get; set; }
        public virtual Users User { get; set; }
    }
}
