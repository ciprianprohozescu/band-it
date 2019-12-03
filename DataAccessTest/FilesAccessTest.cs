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
    /// Summary description for FileAccessTest
    /// </summary>
    [TestClass]
    public class FilesAccessTest
    {
        static TestHelpers testHelpers;
        static FilesAccess filesAccess;
        static BandItEntities db;

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

        [ClassInitialize()]
        public static void Initialize(TestContext context) 
        {
            testHelpers = new TestHelpers();
            filesAccess = new FilesAccess();
            db = ContextProvider.Instance.DB;

            testHelpers.ClearData();
            testHelpers.InsertTestData();
        }

        [TestMethod]
        public void SaveFileTest()
        {
            var user = db.Users.FirstOrDefault();

            filesAccess.SaveFile("user", user.ID, "newfile.txt");

            Assert.AreEqual("newfile.txt", user.Files.LastOrDefault().Name);
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            var user = db.Users.FirstOrDefault();

            filesAccess.SaveFile("user", user.ID, "newfile.txt");

            filesAccess.DeleteFile("user", user.Files.LastOrDefault().ID);

            Assert.IsNotNull(user.Files.LastOrDefault().Deleted);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
