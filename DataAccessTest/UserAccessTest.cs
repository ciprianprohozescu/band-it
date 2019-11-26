using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelsDB;
using DataAccess;
using System.Linq;

namespace DataAccessTest
{
    /// <summary>
    /// Summary description for UserAccessTest
    /// </summary>
    [TestClass]
    public class UserAccessTest
    {
        static UsersAccess usersAccess;
        static BandItEntities db;
        

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            usersAccess = new UsersAccess();
            db = new BandItEntities();

            db.Profiles.RemoveRange(db.Profiles.ToList());
            db.Users.RemoveRange(db.Users.ToList());

            var user1 = new User();
            user1.Email = "Email";
            user1.Username = "Username";
            user1.Password = "Password";
            user1.Salt = "Salt";

            var user2 = new User();
            user2.Email = "Email2";
            user2.Username = "Username2";
            user2.Password = "Password2";
            user2.Salt = "Salt2";

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
        public void GetByUsername()
        {
            var user = usersAccess.FindByUsername("Username");

            Console.WriteLine(user.Username);

            Assert.AreEqual("Email", user.Email);
        }

        [TestMethod]
        public void GetByEmail()
        {
            var user = usersAccess.FindByEmail("Email");

            Console.WriteLine(user.Email);

            Assert.AreEqual("Username", user.Username);
        }

        [TestMethod]
        public void GetAll()
        {
            var users = usersAccess.FindAll();

            Console.WriteLine(users[1].Email);

            Assert.AreEqual("Username2", users[1].Username);
        }

        [TestMethod]
        public void AddUser()
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

        [ClassCleanup]
        public static void DeleteUser()
        {
            db = new BandItEntities();
            db.Profiles.RemoveRange(db.Profiles.ToList());
            db.Users.RemoveRange(db.Users.ToList());
            db.SaveChanges();
        }
    }
}
