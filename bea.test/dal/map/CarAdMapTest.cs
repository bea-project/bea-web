using System;
using Bea.Core.Dal;
using Bea.Dal.Repository;
using Bea.Domain;
using Bea.Domain.Categories;
using Bea.Domain.Location;
using Bea.Domain.Ads;
using Bea.Test.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Bea.Test.Dal.map
{
    [TestClass]
    public class CarAdMapTest : DataAccessTestBase
    {
        [TestMethod]
        public void CarAd_mapping_standalonetable()
        {
            ISessionFactory sessionFactory = NhibernateHelper.SessionFactory;
            IRepository repo = new Repository(sessionFactory);

            using (ITransaction transaction = sessionFactory.GetCurrentSession().BeginTransaction())
            {
                Province p = new Province { Label = "Province Sud" };
                City c = new City { Label = "Nouméa", Province = p };
                p.AddCity(c);
                repo.Save(p);
                repo.Save(c);

                User u = new User
                {
                    Email = "email",
                    Password = "8"
                };
                repo.Save(u);

                Category cat = new Category
                {
                    Label = "label"
                };
                repo.Save(cat);

                CarAd carAd = new CarAd()
                {
                    Title = "title",
                    Body = "bidy",
                    CreationDate = DateTime.Now,
                    IsOffer = true,
                    CreatedBy = u,
                    City = c,
                    Category = cat,
                    Kilometers = 2000,
                    Year = 2013
                };
                repo.Save(carAd);

                Ad ad = new Ad()
                {
                    Title = "title",
                    Body = "bidy",
                    CreationDate = DateTime.Now,
                    IsOffer = true,
                    CreatedBy = u,
                    City = c,
                    Category = cat
                };
                repo.Save(ad);
                repo.Flush();

            }
        }
    }
}
