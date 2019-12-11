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
    public class SkillControllerTest
    {
        static TestHelpers testHelpers;
        static ISkillController skillController;
        static IUserController userController;

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
            userController = new UserController();

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
            var skillExpected = skillController.GetByName("Triangle");
            var skillActual = skillController.GetById(skillExpected.ID);

            Assert.AreEqual(skillExpected.Name, skillActual.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var user = userController.Get().First();
            var skill = skillController.Get().First();

            user.Skills.Add(skill);
            skillController.Add(user);

            skillController.Delete(user);

            user = userController.Get().First();

            Assert.AreEqual(0, user.Skills.Count);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            testHelpers.ClearData();
        }
    }
}
