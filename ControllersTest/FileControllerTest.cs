using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using Controllers;
using System.Linq;

namespace ControllersTest
{
    /// <summary>
    /// Summary description for FileControllerTest
    /// </summary>
    [TestClass]
    public class FileControllerTest
    {
        static TestHelpers testHelpers;
        static IFileController fileController;
        static IUserController userController;

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
            fileController = new FileController();
            userController = new UserController();

            testHelpers.ClearData();
            testHelpers.InsertTestData();
        }

        [TestMethod]
        public void SaveFileTest()
        {
            var file = new Models.File();
            file.Name = "newfile.txt";

            fileController.SaveFile("user", userController.GetByUsername("Andrei1337").ID, file);

            Assert.AreEqual("newfile.txt", userController.GetByUsername("Andrei1337").Files.LastOrDefault().Name);
        }

        [TestMethod]
        public void DeleteFileTest()
        {
            var file = new Models.File();
            file.Name = "newfile.txt";

            var user = userController.GetByUsername("Andrei1337");

            fileController.SaveFile("user", user.ID, file);

            user = userController.GetByUsername("Andrei1337");

            fileController.DeleteFile("user", user.Files[0].ID);

            user = userController.GetByUsername("Andrei1337");

            Assert.AreEqual(0, user.Files.Count);
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
