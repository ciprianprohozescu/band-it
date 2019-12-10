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
    public class BandsAccessTest
    {
        static TestHelpers testHelpers;
        static BandsAccess bandsAccess;
        static BandItEntities db;
        public BandsAccessTest()
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
            bandsAccess = new BandsAccess();
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
        public void GetAllBandsTest()
        {
            var bands = bandsAccess.Get("");

            #region Assert
            Assert.AreEqual(3, bands.Count);

            Assert.AreEqual("LaLaLa", bands[0].Name);
            Assert.AreEqual("Poleyn", bands[2].Name);
            #endregion


            bands = bandsAccess.Get("Nothing");
            Assert.AreEqual(0, bands.Count);
        }

        [TestMethod]
        public void FindByNameTest()
        {
            var band = bandsAccess.FindByName("LaLaLa");

        }

        [TestMethod]
        public void AddTest()
        {
            var band = new Band();
            band.Name = "Test Name";

            bandsAccess.Add(band);

            var band2 = db.Bands.Where(b => b.Name == "Test Name").ToList().FirstOrDefault();

            Assert.AreEqual(band2.Name, band.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var band = bandsAccess.FindByName("Test Name");
            bandsAccess.Delete(band.ID);
            var band2 = bandsAccess.FindByName("Test Name");

            #region Assert
            Assert.IsNull(band2);
            #endregion
        }

        [TestMethod]
        public void AddTest()
        {
            var band = new Band();
            band.Name = "Test Name";

            bandsAccess.Add(band);

            var band2 = db.Bands.Where(b => b.Name == "Test Name").ToList().FirstOrDefault();

            Assert.AreEqual(band2.Name, band.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var band = bandsAccess.FindByName("Test Name");
            bandsAccess.Delete(band.ID);
            var band2 = bandsAccess.FindByName("Test Name");

            #region Assert
            Assert.IsNull(band2);
            #endregion
        }

        [TestMethod]
        public void FindByIDTest()
        {
            var id = bandsAccess.Get("Pol")[0].ID;
            var band = bandsAccess.FindByID(id);

            #region Assert
            Assert.AreEqual("Poleyn", band.Name);
            Assert.AreEqual("This is the description of Poleyn", band.Description);
            #endregion
        }

        [TestMethod]
        public void FindByNameTest()
        {
            var band = bandsAccess.FindByName("Poleyn");

            Assert.AreEqual("This is the description of Poleyn", band.Description);
        }

        [TestMethod]
        public void UpdateTest()
        {
            var band = bandsAccess.Get("")[0];
            band.Name = "Music Band";
            band.Description = "We play music.";
            band.InviteMessage = "Join us.";

            bandsAccess.Update(band);

            band = bandsAccess.Get("")[0];

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
