using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using ModelsDB;

namespace DataAccessTest
{

    [TestClass]
    public class GenreAccessTest
    {
        static TestHelpers testHelpers;
        static GenreAccess genreAccess;
        static BandItEntities db;

        public GenreAccessTest()
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
            genreAccess = new GenreAccess();
            db = new BandItEntities();
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
        public void GetAllGenreTest()
        {
            var genre = genreAccess.Get();

            Assert.AreEqual(3, genre.Count);

            Assert.AreEqual("Jazz", genre[0].Name);
            Assert.AreEqual("Metal", genre[2].Name);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
