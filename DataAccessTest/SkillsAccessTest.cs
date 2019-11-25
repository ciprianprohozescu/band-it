using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using DataAccess;
using ModelsDB;


namespace DataAccessTest
{
    [TestClass]
    public class SkillsAccessTest
    {
        static TestHelpers testHelpers;
        static SkillsAccess skillsAccess;
        static BandItEntities db;

        public SkillsAccessTest()
        {
        }

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
            skillsAccess = new SkillsAccess();
            db = new BandItEntities();

            //db.Skills.RemoveRange(db.Skills.ToList());

            //var skill = new Skill();
            //skill.Name = "Vocalist";
            //db.Skills.Add(skill);

            //skill = new Skill();
            //skill.Name = "Triangle";
            //db.Skills.Add(skill);

            //db.SaveChanges();

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
        public void GetAllSkillsTest()
        {
            var skills = skillsAccess.Get();

            Assert.AreEqual(2, skills.Count);

            Assert.AreEqual("Triangle", skills[0].Name);
            Assert.AreEqual("Vocalist", skills[1].Name);
        }

        [TestMethod]
        public void GetByIDTest()
        {
            var skillExpected = skillsAccess.GetByName("Vocalist");
            var skillActual = skillsAccess.GetByID(skillExpected.ID);

            Assert.AreEqual(skillExpected.Name, skillActual.Name);

            skillActual = skillsAccess.GetByID(-5);

            Assert.IsNull(skillActual);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            //db = new BandItEntities();
            //db.Skills.RemoveRange(db.Skills.ToList());
            //db.SaveChanges();

            testHelpers.ClearData();
        }
    }
}
