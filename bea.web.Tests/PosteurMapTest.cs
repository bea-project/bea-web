using bea.dal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using bea.dal.entities;
using System.Collections.Generic;
using System.Linq;


namespace bea.Tests
{
    
    
    /// <summary>
    ///This is a test class for PosteurMapTest and is intended
    ///to contain all PosteurMapTest Unit Tests
    ///</summary>
    [TestClass()]
    public abstract class PosteurMapTest : InMemoryData
    {
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///</summary>
        [TestMethod()]
        public void PosteurDataBaseMapping()
        {
            NHibernateHelper helper = new NHibernateHelper();
            Assert.IsNotNull(helper);
            UnitOfWork unitOfWork = new UnitOfWork(helper.SessionFactory);
            Assert.IsNotNull(unitOfWork);
            Repository<Posteur> repo = new Repository<Posteur>(unitOfWork.Session);
            Assert.IsNotNull(repo);
            List<Posteur> posteurs = repo.All().ToList();
            Assert.IsTrue(posteurs.Count > 0);  
        }

        /// <summary>
        ///</summary>
        //[TestMethod()]
        //public void PosteurAnnoncesDataBaseMapping()
        //{
        //    NHibernateHelper helper = new NHibernateHelper();
        //    Assert.IsNotNull(helper);
        //    UnitOfWork unitOfWork = new UnitOfWork(helper.SessionFactory);
        //    Assert.IsNotNull(unitOfWork);
        //    Repository<Posteur> repo = new Repository<Posteur>(unitOfWork.Session);
        //    Assert.IsNotNull(repo);
        //    List<Posteur> posteurs = repo.All().ToList();
        //    Assert.IsTrue(posteurs.Count > 0);
        //    Posteur posteur = posteurs[0];
        //    Assert.IsTrue(posteur.annonces.Count > 0);
        //}

        //[TestMethod()]
        //public void PosteurInMemoryDataBaseMapping()
        //{
        //    Repository<Posteur> repo = new Repository<Posteur>(Session);
        //    Assert.IsNotNull(repo);
        //    List<Posteur> posteurs = repo.All().ToList();
        //    Assert.IsTrue(posteurs.Count > 0);
        //}
    }
}
