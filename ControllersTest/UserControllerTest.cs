using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Controllers;
using ModelsDB;
using System.Linq;

namespace ControllersTest
{
    /// <summary>
    /// Summary description for UserControllerTest
    /// </summary>
    [TestClass]
    public class UserControllerTest
    {
        static IUserController userController;
        static BandItEntities db;

        public UserControllerTest()
        {
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

        [ClassInitialize()]
        public static void Initialize(TestContext testContext) 
        {
            userController = new UserController();
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

        [TestMethod]
        public void GetTest()
        {
            var users = userController.Get("");

            #region Assert
            Assert.AreEqual(2, users.Count);

            Assert.AreEqual("Ciprian1337", users[0].Username);
            Assert.AreEqual("Prohozescu", users[0].LastName);

            Assert.AreEqual("Andrei1337", users[1].Username);
            Assert.AreEqual("Mataoanu", users[1].LastName);
            #endregion

            users = userController.Get("Ciprian");

            #region Assert
            Assert.AreEqual(1, users.Count);

            Assert.AreEqual("Ciprian1337", users[0].Username);
            Assert.AreEqual("Prohozescu", users[0].LastName);
            #endregion

            users = userController.Get("Anon");

            #region Assert
            Assert.AreEqual(0, users.Count);
            #endregion
        }

        [TestMethod]
        public void GetByUsername()
        {
            var user = userController.GetByUsername("Username");

            Console.WriteLine(user.Username);

            Assert.AreEqual("Email", user.Email);
        }

        [TestMethod]
        public void GetByEmail()
        {
            var user = userController.GetByEmail("Email");

            Console.WriteLine(user.Email);

            Assert.AreEqual("Username", user.Username);
        }
        [TestMethod]
        public void GetAll()
        {
            var users = userController.Get();

            Console.WriteLine(users[1].Email);

            Assert.AreEqual("Username2", users[1].Username);
        }

        [ClassCleanup]
        public static void Delete()
        {
            db = new BandItEntities();
            db.Profiles.RemoveRange(db.Profiles.ToList());
            db.Users.RemoveRange(db.Users.ToList());
            db.SaveChanges();
        }
    }
}
