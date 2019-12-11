using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess;
using ModelsDB;
using System.Linq;
using Moq;
using System.Linq.Expressions;

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
            db = ContextProvider.Instance.DB;

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
            Assert.AreEqual(3, users.Count);

            Assert.AreEqual("Ciprian1337", users[1].Username);
            Assert.AreEqual("Prohozescu", users[1].LastName);

            Assert.AreEqual("Andrei1337", users[2].Username);
            Assert.AreEqual("Mataoanu", users[2].LastName);
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
        public void FindByEmailTest()
        {
            var user = usersAccess.FindByEmail("ciprian@gmail.com");

            Console.WriteLine(user.Email);

            Assert.AreEqual("Ciprian1337", user.Username);
        }

        [TestMethod]
        public void AddUserTest()
        {
            var user = new User();
            user.Email = "Test Email";
            user.Username = "Test Username";
            user.Password = "Test Password";
            user.Salt = "Test Salt";

            usersAccess.Add(user);

            var user2 = db.Users.Where(u => u.Username == "Test Username").ToList().FirstOrDefault();

            Assert.AreEqual(user2.Email, user.Email);
        }

        [TestMethod]
        public void FindByEmailOrUsernameTest()
        {
            var user = usersAccess.FindByEmailOrUsername("Andrei1337");

            #region Assert
            Assert.AreEqual("andrei@gmail.com", user.Email);
            Assert.AreEqual("Andrei", user.FirstName);
            Assert.AreEqual("Mataoanu", user.LastName);
            #endregion

            user = usersAccess.FindByEmailOrUsername("ciprian@gmail.com");

            #region Assert
            Assert.AreEqual("Ciprian1337", user.Username);
            Assert.AreEqual("Ciprian", user.FirstName);
            Assert.AreEqual("Prohozescu", user.LastName);
            #endregion

            user = usersAccess.FindByEmailOrUsername("anon");

            #region Assert
            Assert.IsNull(user);
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

        [TestMethod]
        public void SaveLocation()
        {
            var user = usersAccess.FindByUsername("Ciprian1337");

            user.Latitude = 50;
            user.Longitude = 50;

            usersAccess.SaveLocation(user);

            user = usersAccess.FindByUsername("Ciprian1337");

            #region Assert
            Assert.AreEqual(50, user.Latitude);
            Assert.AreEqual(50, user.Longitude);
            #endregion
        }

        [TestMethod]
        public void UpdateProfilePictureTest()
        {
            usersAccess.UpdateProfilePicture(usersAccess.FindByUsername("Andrei1337").ID, "newpic.jpg");

            Assert.AreEqual("newpic.jpg", usersAccess.FindByUsername("Andrei1337").ProfilePicture);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var user = usersAccess.Get("")[0];
            user.Username = "Marean99";
            user.FirstName = "Marian";
            user.LastName = "Dobra";
            user.Description = "I like music";
            user.Email = "marian@email.com";
            user.Password = "12345";

            usersAccess.Update(user);

            Assert.AreEqual("Marean99", user.Username);
            Assert.AreEqual("Marian", user.FirstName);
            Assert.AreEqual("Dobra", user.LastName);
            Assert.AreEqual("I like music", user.Description);
            Assert.AreEqual("marian@email.com", user.Email);
            Assert.AreEqual("12345", user.Password);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
