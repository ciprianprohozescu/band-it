using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class Blocks
    {
        public int Id { get; set; }
        public int BlockerId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime Blocked { get; set; }
        public DateTime? Deleted { get; set; }

        public virtual Users Blocker { get; set; }
        public virtual Users Receiver { get; set; }
    }
}
