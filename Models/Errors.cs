using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class Errors
    {
        public static class BandErrors
        {
            public static string EmptyName { get { return "The band's name cannot be empty."; } }
            public static string DuplicateName { get { return "That name is already taken."; } }
            public static string ConcurrencyError { get { return "Someone made changes to this band while you were editing it. Check the changes and try again."; } }
        }
    }
}
