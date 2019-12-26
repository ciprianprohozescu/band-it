using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Genres
    {
        public Genres()
        {
            BandGenres = new HashSet<BandGenres>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual ICollection<BandGenres> BandGenres { get; set; }
    }
}
