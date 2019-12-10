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
        public static class UserErrors
        {
            public static string EmptyUsername { get { return "The username cannot be empty."; } }
            public static string UsernameTooShort { get { return "The username must be at least 5 characters long."; } }
            public static string DuplicateUsername { get { return "That username is already taken."; } }
            public static string EmptyEmail { get { return "The email cannot be empty."; } }
            public static string DuplicateEmail { get { return "That email is already taken."; } }
            public static string EmptyPassword { get { return "The password cannot be empty."; } }
            public static string PasswordTooShort { get { return "The password must be at least 5 characters long."; } }
        }
    }
}
