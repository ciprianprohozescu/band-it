using Controllers;
using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersTest
{
    [TestClass]
    class SkillControllerTest
    {
        static TestHelpers testHelpers;
        static ISkillController skillController;
        static ModelsDB.BandItEntities db;

        private TestContext testContextInstance;

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
            skillController = new SkillController();
            db = new ModelsDB.BandItEntities();

            testHelpers.ClearData();
            testHelpers.InsertTestData();
        }

        [TestMethod]
        public void GetTest()
        {
            var skills = skillController.Get();

            Assert.AreEqual(2, skills.Count);

            Assert.AreEqual("Triangle", skills[0].Name);
            Assert.AreEqual("Vocalist", skills[1].Name);
        }

        [TestMethod]
        public void GetByIDTest()
        {
            var skillExpected = skillController.GetByName("Vocalist");
            var skillActual = skillController.GetById(skillExpected.ID);

            Assert.AreEqual(skillExpected.Name, skillActual.Name);

            skillActual = skillController.GetById(-5);

            Assert.IsNull(skillActual);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var skill = skillController.GetByName("Vocalist");
            skillController.Delete(skill.ID);
            var skill2 = skillController.GetByName("Vocalist");

            Assert.IsNull(skill2);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
