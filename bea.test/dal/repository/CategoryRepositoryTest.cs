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
    }
}
