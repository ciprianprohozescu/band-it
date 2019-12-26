using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Applications
    {
        public int Id { get; set; }
        public int BandId { get; set; }
        public int UserId { get; set; }
        public DateTime Sent { get; set; }
        public string Message { get; set; }
        public bool? Result { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Bands Band { get; set; }
        public virtual Users User { get; set; }
    }
}
