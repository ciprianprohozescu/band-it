using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Models;
using ModelsDB;
using UserLogic = Models.User;
using UserDB = ModelsDB.User;
using Controllers;
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
        static ModelsDB.BandItEntities db;

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
            db = new ModelsDB.BandItEntities();

            #region Test data
            db.Users.RemoveRange(db.Users.ToList());

            var user = new UserDB();

            user.Username = "Andrei1337";
            user.Email = "andrei@gmail.com";
            user.Password = "1234";
            user.Salt = "sgsefwer";

            user.FirstName = "Andrei";
            user.LastName = "Mataoanu";
            user.Latitude = (decimal)57.006620;
            user.Longitude = (decimal)9.879727;

            db.Users.Add(user);

            user = new UserDB();

            user.Username = "Ciprian1337";
            user.Email = "ciprian@gmail.com";
            user.Password = "1234";
            user.Salt = "sgsefwer";

            user.FirstName = "Ciprian";
            user.LastName = "Prohozescu";
            user.Latitude = (decimal)52.006620;
            user.Longitude = (decimal)14.879727;

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
        public void GetByIDTest()
        {
            var userExpected = userController.Get("Ciprian")[0];
            var userActual = userController.Get(userExpected.ID);

            Assert.AreEqual(userExpected.Username, userActual.Username);
        }

        [TestMethod]
        public void GetByDistanceTest()
        {
            var distance = 75.0;
            var marker = new Models.LatLng(57.006617, 9.879724);

            List<UserLogic> users = userController.Get("", distance, marker.Latitude, marker.Longitude);

            #region Assert
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual("Andrei1337", users[0].Username);
            #endregion
        }
        
        [TestMethod]
        public void Delete()
        {
            var user = userController.GetByUsername("Ciprian1337");
            userController.Delete(user.ID);
            var user2 = userController.GetByUsername("Ciprian1337");
            #region Assert
            Assert.IsNull(user2);
            #endregion
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            //db.Users.RemoveRange(db.Users.ToList());
            db.SaveChanges();
        
        }
    }
}
