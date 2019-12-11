using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using ModelsDB;
using System.Linq;

namespace DataAccessTest
{

    [TestClass]
    public class BandUserAccessTest
    {
        static TestHelpers testHelpers;
        static BandUserAccess bandUserAccess;
        static BandItEntities db;

        public BandUserAccessTest()
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
            bandUserAccess = new BandUserAccess();
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
        public void AddBandUserTest()
        {
            var bandUser = new BandUser();

            var band = new Band();
            band.Name = "Test Name";
            band.Description = "Test Description";

            bandUser.Band = band;

            bandUserAccess.Add(bandUser);

            var bandUser2 = db.BandUsers.Where(b => b.Band.Name == "Test Name").ToList().FirstOrDefault();

            Assert.AreEqual(bandUser.Band.Description, bandUser2.Band.Description);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
