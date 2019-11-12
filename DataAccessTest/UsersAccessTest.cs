using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess;
using ModelsDB;
using System.Linq;

namespace DataAccessTest
{
    /// <summary>
    /// Summary description for UsersAccessTest
    /// </summary>
    [TestClass]
    public class UsersAccessTest
    {
        public UsersAccessTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        static UsersAccess usersAccess;
        static BandItEntities db;

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            usersAccess = new UsersAccess();
            db = new BandItEntities();

            #region Test data
            db.Profiles.RemoveRange(db.Profiles.ToList());
            db.Users.RemoveRange(db.Users.ToList());

            var user = new User();
            var profile = new Profile();

            user.Username = "Andrei1337";
            user.Email = "andrei@gmail.com";
            user.Password = "1234";
            user.Salt = "sgsefwer";

            profile.FirstName = "Andrei";
            profile.LastName = "Mataoanu";

            db.Profiles.Add(profile);
            user.Profiles.Add(profile);
            db.Users.Add(user);

            user = new User();
            profile = new Profile();

            user.Username = "Ciprian1337";
            user.Email = "ciprian@gmail.com";
            user.Password = "1234";
            user.Salt = "sgsefwer";
            profile.FirstName = "Ciprian";
            profile.LastName = "Prohozescu";

            
            db.Profiles.Add(profile);
            user.Profiles.Add(profile);
            db.Users.Add(user);

            db.SaveChanges();
            #endregion
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetAllUsersTest()
        {
            var users = usersAccess.Get("");

            #region Assert
            Assert.AreEqual("Ciprian1337", users[0].Username);
            Assert.AreEqual("Prohozescu", users[0].Profiles.FirstOrDefault().LastName);

            Assert.AreEqual("Andrei1337", users[1].Username);
            Assert.AreEqual("Mataoanu", users[1].Profiles.FirstOrDefault().LastName);
            #endregion
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            db = new BandItEntities();
            db.Profiles.RemoveRange(db.Profiles.ToList());
            db.Users.RemoveRange(db.Users.ToList());
            db.SaveChanges();
        }
    }
}
