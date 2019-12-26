using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class BandMessages
    {
        public int BandId { get; set; }
        public int MessageId { get; set; }

        public virtual Bands Band { get; set; }
        public virtual Messages Message { get; set; }
    }
}
