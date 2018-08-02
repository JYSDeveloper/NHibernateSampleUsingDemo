using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using Domain.Dao;
using NHibernate;
using Domain;
namespace NHibernateSampleUsingDemo
{
    /// <summary>
    /// Summary description for BaseTest
    /// </summary>
    [TestClass]
    public class BaseTest
    {
        public BaseTest()
        {
            //
            // TODO: Add constructor logic here
            //
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
        public void SaveSingletonEntity()
        {
            var entity = new SingletonEntity();
            var dao = new BaseDao();
            dao.Save(entity);
            Assert.AreNotEqual(entity.Id, 0);
        }

        /// <summary>
        /// When you save some value by save methond, before session colse, don't reset properties value,
        /// reset value will trigger update method
        /// </summary>
        [TestMethod]
        public void SaveWithUpdate()
        {
            var entity = new SingletonEntity();
            ISession session = SessionHelper.GetSession();
            session.Save(entity);
            entity.Name = "UpdateValue";
            session.Flush();
            //Assert.AreEqual(entity.Version, 2);
            Assert.AreEqual(entity.Name, "UpdateValue");
        }

        [TestMethod]
        public void GetWithUpdate()
        {
            
            ISession session = SessionHelper.GetSession();
            var entity = session.Get<SingletonEntity>((long)1);
            entity.Name = "UpdateValue";
            session.Flush();
            //Assert.AreEqual(entity.Version, 3);
            Assert.AreEqual(entity.Name, "UpdateValue");
        }
    }
}
