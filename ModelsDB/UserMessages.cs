using System;
using System.Collections.Generic;

namespace ModelsDB
{
    public partial class UserMessages
    {
        public int UserId { get; set; }
        public int MessageId { get; set; }

        public virtual Messages Message { get; set; }
        public virtual Users User { get; set; }
    }
}
