using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Invites
    {
        public int Id { get; set; }
        public int BandId { get; set; }
        public int UserId { get; set; }
        public DateTime Sent { get; set; }
        public bool? Result { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Bands Band { get; set; }
        public virtual Users User { get; set; }
    }
}
