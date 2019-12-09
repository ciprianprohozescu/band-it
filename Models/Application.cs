using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Application
    {
        public int ID { get; set; }
        public DateTime Sent { get; set; }
        public string Message { get; set; }
        public bool? Result { get; set; }
        public DateTime Deleted { get; set; }
        public int UserID { get; set; }
        public int BandID { get; set; }
        public User User { get; set; }
    }
}
