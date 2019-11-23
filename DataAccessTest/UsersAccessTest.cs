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
        static TestHelpers testHelpers;
        static UsersAccess usersAccess;
        static BandItEntities db;

        public UsersAccessTest()
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

        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            testHelpers = new TestHelpers();
            usersAccess = new UsersAccess();
            db = new BandItEntities();

            testHelpers.ClearData();
            testHelpers.InsertTestData();
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
            Assert.AreEqual(2, users.Count);

            Assert.AreEqual("Ciprian1337", users[0].Username);
            Assert.AreEqual("Prohozescu", users[0].LastName);

            Assert.AreEqual("Andrei1337", users[1].Username);
            Assert.AreEqual("Mataoanu", users[1].LastName);
            #endregion

            users = usersAccess.Get("Ciprian");

            #region Assert
            Assert.AreEqual(1, users.Count);

            Assert.AreEqual("Ciprian1337", users[0].Username);
            Assert.AreEqual("Prohozescu", users[0].LastName);
            #endregion

            users = usersAccess.Get("Anon");

            Assert.AreEqual(0, users.Count);
        }

        [TestMethod]
        public void FindByUsernameTest()
        {
            var user = usersAccess.FindByUsername("Andrei1337");

            #region Assert
            Assert.AreEqual("andrei@gmail.com", user.Email);
            Assert.AreEqual("Andrei", user.FirstName);
            Assert.AreEqual("Mataoanu", user.LastName);
            #endregion
        }

        [TestMethod]
        public void DeleteTest()
        {
            var user = usersAccess.FindByUsername("Radu1337");
            usersAccess.Delete(user.ID);
            var user2 = usersAccess.FindByUsername("Radu1337");

            #region Assert
            Assert.IsNull(user2);
            #endregion
        }

        [TestMethod]
        public void FindByIDTest()
        {
            var userExpected = usersAccess.FindByUsername("Andrei1337");
            var userActual = usersAccess.FindByID(userExpected.ID);

            Assert.AreEqual(userExpected.Username, userActual.Username);

            userActual = usersAccess.FindByID(-5);

            Assert.IsNull(userActual);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
