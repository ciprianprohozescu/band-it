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
        public static void setupUsers(TestContext context)
        {


            usersAccess = new UsersAccess();
            db = new BandItEntities();

            db.Profiles.RemoveRange(db.Profiles.ToList());
            db.Users.RemoveRange(db.Users.ToList());

            var user1 = new User();
            var user2 = new User();
            var profile1 = new Profile();
            var profile2 = new Profile();

            user1.Username = "Andrei1337";
            user1.Email = "andrei@gmail.com";
            user1.Password = "1234";
            user1.Salt = "sgsefwer";

            profile1.FirstName = "Andrei";
            profile1.LastName = "Mataoanu";

            user2.Username = "Ciprian1337";
            user2.Email = "ciprian@gmail.com";
            user2.Password = "1234";
            user2.Salt = "sgsefwer";
            profile2.FirstName = "Ciprian";
            profile2.LastName = "Prohozescu";

            db.Profiles.Add(profile1);
            db.Profiles.Add(profile2);

            user1.Profiles.Add(profile1);
            user2.Profiles.Add(profile2);

            db.Users.Add(user1);
            db.Users.Add(user2);
            db.SaveChanges();
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

            Assert.AreEqual("Ciprian1337", users[0].Username);
            Assert.AreEqual("Prohozescu", users[0].Profiles.FirstOrDefault().LastName);
            Assert.AreEqual("Andrei1337", users[1].Username);
            Assert.AreEqual("Mataoanu", users[1].Profiles.FirstOrDefault().LastName);
        }
        
        [ClassCleanup]
        public static void deleteUsers()
        {
            db = new BandItEntities();
            db.Profiles.RemoveRange(db.Profiles.ToList());
            db.Users.RemoveRange(db.Users.ToList());
            db.SaveChanges();
        }
    }
}
