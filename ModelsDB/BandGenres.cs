using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class BandGenres
    {
        public int BandId { get; set; }
        public int GenreId { get; set; }

        public virtual Bands Band { get; set; }
        public virtual Genres Genre { get; set; }
    }
}
