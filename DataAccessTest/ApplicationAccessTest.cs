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
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ApplicationAccessTest
    {
        static TestHelpers testHelpers;
        static ApplicationAccess applicationAccess;
        static BandItEntities db;

        public ApplicationAccessTest()
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
            applicationAccess = new ApplicationAccess();
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
        public void AddApplicationTest()
        {
            var application = new Application();
            application.Sent = DateTime.Now;
            application.Message = "Test Message";
            application.Result = true;
            application.BandID = db.Bands.ToList().FirstOrDefault().ID;
            application.UserID = db.Users.ToList().FirstOrDefault().ID;

            applicationAccess.Add(application);

            var application2 = db.Applications.Where(a => a.Message.StartsWith("Test Message") && a.Message.EndsWith("Test Message")).ToList().FirstOrDefault();

            Assert.AreEqual(application2.Message, application.Message);
        }

        [TestMethod]
        public void GetApplicationByIdTest()
        {
            var applicationExpected = applicationAccess.Get("Test Message");
            var applicationActual = applicationAccess.Get(applicationExpected.ID);

            Assert.AreEqual(applicationExpected.BandID, applicationActual.BandID);
        }

        [TestMethod]
        public void GetAllApplicationsTest()
        {
            var applications = applicationAccess.Get();

            #region Assert
            Assert.AreEqual(2, applications.Count);
            Assert.AreEqual("Test Message", applications[0].Message);
            #endregion
        }

        [TestMethod]
        public void AcceptApplicationTest()
        {
            var applications = applicationAccess.Get();
            applicationAccess.Accept(applications[0].ID);

            applications = applicationAccess.Get();
            Assert.AreEqual(applications[0].Result, true);
        }

        [TestMethod]
        public void DeclineApplicationTest()
        {
            var applications = applicationAccess.Get();
            applicationAccess.Decline(applications[1].ID);

            applications = applicationAccess.Get();
            Assert.AreEqual(applications[1].Result, false);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
           testHelpers.ClearData();
        }
    }
}
