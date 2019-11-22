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
            skillsAccess = new SkillsAccess();
            db = new BandItEntities();

            db.Skills.RemoveRange(db.Skills.ToList());

            var skill = new Skill();
            skill.Name = "Vocalist";
            db.Skills.Add(skill);

            skill = new Skill();
            skill.Name = "Triangle";
            db.Skills.Add(skill);

            db.SaveChanges();
        }

        [TestMethod]
        public void GetAllSkillsTest()
        {
            var skills = skillsAccess.Get();

            Assert.AreEqual(2, skills.Count);

            Assert.AreEqual("Triangle", skills[0].Name);
            Assert.AreEqual("Vocalist", skills[1].Name);
        }

        //[TestMethod]
        //public void GetByNameTest()
        //{
        //    var skill = skillsAccess.GetByName("");
        //}

        [TestMethod]
        public void GetByIDTest()
        {
            var skillExpected = skillsAccess.GetByName("Vocalist");
            var skillActual = skillsAccess.GetByID(skillExpected.ID);

            Assert.AreEqual(skillExpected.Name, skillActual.Name);

            skillActual = skillsAccess.GetByID(1);

            Assert.IsNull(skillActual);
        }

        [ClassCleanup]
        public static void Cleanup()
        {
            db = new BandItEntities();
            db.Skills.RemoveRange(db.Skills.ToList());
            db.SaveChanges();
        }
    }
}
