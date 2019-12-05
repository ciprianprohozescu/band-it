using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Models;
using ModelsDB;
using DataAccess;
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
        static TestHelpers testHelpers;
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

        [ClassInitialize]
        public static void Initialize(TestContext context) 
        {
            testHelpers = new TestHelpers();
            userController = new UserController();
            db = new ModelsDB.BandItEntities();

            testHelpers.ClearData();
            testHelpers.InsertTestData();
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
            var userActual = userController.GetById(userExpected.ID);

            Assert.AreEqual(userExpected.Username, userActual.Username);
        }

        [TestMethod]
        public void GetByEmailTest()
        {
            var userExpected = userController.Get("Ciprian")[0];
            var userActual = userController.GetByEmail(userExpected.Email);

            Assert.AreEqual(userExpected.Username, userActual.Username);
        }

        [TestMethod]
        public void GetByUsernameTest()
        {
            var user = userController.GetByUsername("Ciprian1337");
            #region Assert
            Assert.AreEqual(user.Email, "ciprian@gmail.com");
            #endregion
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
        public void DeleteTest()
        {
            var user = userController.GetByUsername("Radu1337");
            userController.Delete(user.ID);
            var user2 = userController.GetByUsername("Radu1337");
            #region Assert
            Assert.IsNull(user2);
            #endregion
        }

        [TestMethod]
        public void LogInTest()
        {
            var newUser = new UserLogic();
            newUser.Username = "NewUser";
            newUser.Email = "newuser@gmail.com";
            newUser.Password = "iamnewuser";

            userController.Add(newUser);

            var user = userController.LogIn("NewUser", "iamnewuser");
            Assert.AreEqual("newuser@gmail.com", user.Email);

            user = userController.LogIn("NewUser", "1234");
            Assert.IsNull(user);
        }
        [TestMethod]
        public void SaveLocationTest()
        {
            userController = new UserController();
            var sampleUser = userController.GetByUsername("Radu1337");
            userController.SaveLocation(sampleUser);
            var lat = userController.GetByUsername("Radu1337").Location.Latitude;
            var lng = userController.GetByUsername("Radu1337").Location.Longitude;
            //Checks user's latitude
            Assert.AreEqual((decimal)53.003310, (decimal)lat);
            //Checks user's longitude
            Assert.AreEqual((decimal)13.982130, (decimal)lng);
        }

        [TestMethod]
        public void UpdateProfilePictureTest()
        {
            userController.UpdateProfilePicture(userController.GetByUsername("Andrei1337").ID, "newpic.jpg");

            Assert.AreEqual("newpic.jpg", userController.GetByUsername("Andrei1337").ProfilePicture);
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
