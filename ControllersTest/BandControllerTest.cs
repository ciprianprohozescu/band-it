using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using Controllers;
using ModelsDB;

namespace ControllersTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BandControllerTest
    {
        static TestHelpers testHelpers;
        static IBandController bandController;

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
            bandController = new BandController();

            //testHelpers.ClearData();
            //testHelpers.InsertTestData();
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
        public void GetTest()
        {
            var bands = bandController.Get("");

            #region Assert
            Assert.AreEqual(3, bands.Count);

            Assert.AreEqual("Dansk Rap", bands[0].Name);
            Assert.AreEqual("LaLaLa", bands[2].Name);
            #endregion

            bands = bandController.Get("Nothing");
            Assert.AreEqual(0, bands.Count);

            bands = bandController.Get("Pol");

            #region Assert
            Assert.AreEqual(1, bands.Count);

            Assert.AreEqual("Poleyn", bands[0].Name);
            #endregion
        }

        [TestMethod]
        public void GetByIDTest()
        {
            var id = bandController.Get("Pol")[0].ID;
            var band = bandController.GetById(id);

            #region Assert
            Assert.AreEqual("Poleyn", band.Name);
            Assert.AreEqual("This is the description of Poleyn", band.Description);
            #endregion
        }

        [TestMethod]
        public void FindByNameTest()
        {
            var band = bandController.GetByName("Poleyn");

            Assert.AreEqual("This is the description of Poleyn", band.Description);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var band = bandController.Get("")[0];
            band.Name = "Music Band";
            band.Description = "We play music.";
            band.InviteMessage = "Join us.";

            bandController.Update(band);

            band = bandController.Get("")[0];

            #region Assert
            Assert.AreEqual("Music Band", band.Name);
            Assert.AreEqual("We play music.", band.Description);
            Assert.AreEqual("Join us.", band.InviteMessage);
            #endregion
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
