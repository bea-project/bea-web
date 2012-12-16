using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Domain.Location;
using log4net;
using log4net.Repository.Hierarchy;
using NHibernate;
using NHibernate.Linq;

namespace Bea.Web.NhibernateHelper
{
    public class InMemoryDataInjector
    {
        private static ILog log = log4net.LogManager.GetLogger(typeof(InMemoryDataInjector));
        private ISessionFactory _sessionFactory;
        private IRepository _repository;

        public InMemoryDataInjector(ISessionFactory sessionFactory, IRepository repository)
        {
            _sessionFactory = sessionFactory;
            _repository = repository;
        }

        public void InsertInMemoryData()
        {
            //-------------------------------------------
            //         LOCATION REFERENCE TABLES
            //-------------------------------------------
            using (ITransaction transaction = _sessionFactory.GetCurrentSession().BeginTransaction())
            {
                
                //Create Provinces
                Province provinceNord = new Province();
                provinceNord.Label = "Province Nord";
                _sessionFactory.GetCurrentSession().Save(provinceNord);

                Province provinceSud = new Province();
                provinceSud.Label = "Province Sud";
                _sessionFactory.GetCurrentSession().Save(provinceSud);

                City city = new City();
                city.Label = "Noumea";
                provinceSud.AddCity(city);
                _sessionFactory.GetCurrentSession().Save(city);

                City city2 = new City();
                city2.Label = "Koumac";
                provinceNord.AddCity(city2);
                _sessionFactory.GetCurrentSession().Save(city2);

                //-------------------------------------------
                //         USER TABLE
                //-------------------------------------------


                //Create User 1
                User user = new User();
                user.Email = "bruno.deprez@gmail.com";
                user.Password = "mypassword";
                _sessionFactory.GetCurrentSession().Save(user);

                //Create User 2
                User user2 = new User();
                user2.Email = "nicolas.raynaud@gmail.com";
                user2.Password = "mypassword";
                _sessionFactory.GetCurrentSession().Save(user);

                //-------------------------------------------
                //         IMAGES
                //-------------------------------------------
                AdImage img1 = new AdImage();
                img1.IsPrimary = true;
                img1.FileName = "toto.jpg";
                img1.ImageBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/App_Data/bateau-aluminium-04.jpg"));
                log.Info(_sessionFactory.GetCurrentSession().Save(img1));

                AdImage img2 = new AdImage();
                img2.IsPrimary = true;
                img2.FileName = "toto.jpg";
                img2.ImageBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/App_Data/suzuki-gsxr.jpg"));
                log.Info(_sessionFactory.GetCurrentSession().Save(img2));

                //-------------------------------------------
                //         AD TABLE
                //-------------------------------------------

                for (int i = 1; i < 31; i++)
                {
                    Ad ad = new Ad();
                    Faker.Lorem.Words(3).ForEach(s => ad.Title += " "+s);
                    ad.Body = Faker.Lorem.Paragraph();
                    ad.CreationDate = DateTime.Now
                        .AddDays(Faker.RandomNumber.Next(1, 7))
                        .AddHours(Faker.RandomNumber.Next(1, 23))
                        .AddMinutes(Faker.RandomNumber.Next(1, 59))
                        .AddSeconds(Faker.RandomNumber.Next(1, 59));
                    ad.Price = Faker.RandomNumber.Next(1, 300000);
                    if (i == 1)
                        ad.AddImage(img1);
                    if (i == 2)
                        ad.AddImage(img2);
                    user.AddAd(ad);
                    city.AddAd(ad);
                    _sessionFactory.GetCurrentSession().Save(ad);
                }

                _sessionFactory.GetCurrentSession().Update(img1);
                _sessionFactory.GetCurrentSession().Update(img2);

                _sessionFactory.GetCurrentSession().Flush();

                transaction.Commit();
            }
        }
    }
}