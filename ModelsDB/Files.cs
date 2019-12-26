using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Files
    {
        public Files()
        {
            UserFiles = new HashSet<UserFiles>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<UserFiles> UserFiles { get; set; }
    }
}
