using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApplicationDB = ModelsDB.Application;
using Models;
using DataAccess;
using Controllers;

namespace ControllersTest
{
    [TestClass]
    public class ApplicationControllerTest
    {
        static TestHelpers testHelpers;
        static IApplicationController applicationController;
        static IBandController bandController;
        static IUserController userController;
        static ApplicationAccess applicationAccess;
        static ModelsDB.BandItEntities db;

        public ApplicationControllerTest()
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
            applicationController = new ApplicationController();
            bandController = new BandController();
            userController = new UserController();
            db = new ModelsDB.BandItEntities();
            applicationAccess = new ApplicationAccess();

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
            application.BandID = bandController.Get("")[0].ID;
            application.UserID = userController.Get("")[0].ID;

            applicationController.Add(application);

            var application2 = applicationAccess.Get(application.Message);

            Assert.AreEqual(application2.Message, application.Message);
        }

        [TestMethod]
        public void GetApplicationsTest()
        {
            var applications = applicationController.Get();

            #region Assert
            Assert.AreEqual(2, applications.Count);

            Assert.AreEqual("Test Message", applications[0].Message);

            Assert.AreEqual("This is the messege", applications[1].Message);
            #endregion
        }

        [TestMethod]
        public void AcceptApplicationTest()
        {
            var applications = applicationController.Get();
            applicationController.Accept(applications[0].ID, applications[0].UserID, applications[0].BandID);

            applications = applicationController.Get();
            Assert.AreEqual(applications[0].Result, true);
        }

        [TestMethod]
        public void DeclineApplicationTest()
        {
            var applications = applicationController.Get();
            applicationController.Decline(applications[1].ID);

            applications = applicationController.Get();
            Assert.AreEqual(applications[1].Result, false);
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
