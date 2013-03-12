using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Dal.Repository;
using Bea.Domain.Categories;
using Bea.Test.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Bea.Test.Dal.repository
{
    [TestClass]
    public class CategoryRepositoryTest : DataAccessTestBase
    {
        [TestMethod]
        public void TestGetCategoryFromUrlPart_NoMatch_ReturnNull()
        {
             ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
             CategoryRepository catRepo = new CategoryRepository(sessionFactory);

             using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
             {
                 sessionFactory.GetCurrentSession().Save(new Category() { Label = "label", LabelUrlPart = "url" });

                 Assert.IsNull(catRepo.GetCategoryFromUrlPart("zeuh"));
             }
        }

        [TestMethod]
        public void TestGetCategoryFromUrlPart_1Match_ReturnItem()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            CategoryRepository catRepo = new CategoryRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                sessionFactory.GetCurrentSession().Save(new Category() { Label = "label", LabelUrlPart = "urlcat" });
                sessionFactory.GetCurrentSession().Save(new Category() { Label = "label22", LabelUrlPart = "urlcat6" });

                Category actual = catRepo.GetCategoryFromUrlPart("urlcat");

                Assert.AreEqual("label", actual.Label);
            }
        }

        [TestMethod]
        public void TestGetCategoryFromLabel_NoMatch_ReturnNull()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            CategoryRepository catRepo = new CategoryRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                sessionFactory.GetCurrentSession().Save(new Category() { Label = "label", LabelUrlPart = "url" });
                Assert.IsNull(catRepo.GetCategoryFromLabel("zeuh"));
            }
        }

        [TestMethod]
        public void TestGetCategoryFromLabel_1Match_ReturnItem()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            CategoryRepository catRepo = new CategoryRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                sessionFactory.GetCurrentSession().Save(new Category() { Label = "label", LabelUrlPart = "urlcat" });
                sessionFactory.GetCurrentSession().Save(new Category() { Label = "label22", LabelUrlPart = "urlcat6" });

                Category actual = catRepo.GetCategoryFromLabel("label");

                Assert.AreEqual("urlcat", actual.LabelUrlPart);
            }
        }

        [TestMethod]
        public void TestGetCategoryFromLabel_1Match_ReturnItemAndChildren()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            CategoryRepository catRepo = new CategoryRepository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                Category parent = new Category() { Label = "parentlabel", LabelUrlPart = "parentlabelurl" };
                sessionFactory.GetCurrentSession().Save(parent);
                Category child1 = new Category() { Label = "childlabel1", LabelUrlPart = "childlabel1url" };
                Category child2 = new Category() { Label = "childlabel2", LabelUrlPart = "childlabel2url" };
                parent.AddCategory(child1);
                parent.AddCategory(child2);
                sessionFactory.GetCurrentSession().SaveOrUpdate(parent);
                sessionFactory.GetCurrentSession().Save(child1);
                sessionFactory.GetCurrentSession().Save(child2);

                Category actual = catRepo.GetCategoryFromLabel("parentlabel");
                Assert.AreEqual(2, actual.SubCategories.Count);
                Assert.AreEqual("childlabel1url", actual.SubCategories[0].LabelUrlPart);
                Assert.AreEqual("childlabel2url", actual.SubCategories[1].LabelUrlPart);
            }
        }
    }
}
