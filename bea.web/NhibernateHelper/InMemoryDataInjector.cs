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

        public void InsertLocations()
        {
            //-------------------------------------------
            //         LOCATION REFERENCE TABLES
            //-------------------------------------------

            if (_repository.CountAll<City>() == 0)
            {
                //Create Provinces
                Province provinceNord = new Province();
                provinceNord.Label = "Province Nord";
                provinceNord.AddCity(new City { Label = "Poya Nord" });
                provinceNord.AddCity(new City { Label = "Pouembout" });
                provinceNord.AddCity(new City { Label = "Koné" });
                provinceNord.AddCity(new City { Label = "Voh" });
                provinceNord.AddCity(new City { Label = "Kaala-Gomen" });
                provinceNord.AddCity(new City { Label = "Koumac" });
                provinceNord.AddCity(new City { Label = "Poum" });
                provinceNord.AddCity(new City { Label = "Iles Belep" });
                provinceNord.AddCity(new City { Label = "Ouégoa" });
                provinceNord.AddCity(new City { Label = "Pouébo" });
                provinceNord.AddCity(new City { Label = "Hienghène" });
                provinceNord.AddCity(new City { Label = "Touho" });
                provinceNord.AddCity(new City { Label = "Poindimié" });
                provinceNord.AddCity(new City { Label = "Ponérihouen" });
                provinceNord.AddCity(new City { Label = "Houaïlou" });
                provinceNord.AddCity(new City { Label = "Kouaoua" });
                provinceNord.AddCity(new City { Label = "Canala" });
                _repository.Save(provinceNord);


                Province provinceSud = new Province();
                provinceSud.Label = "Province Sud";
                provinceSud.AddCity(new City { Label = "Thio" });
                provinceSud.AddCity(new City { Label = "Yaté" });
                provinceSud.AddCity(new City { Label = "Ile des Pins" });
                provinceSud.AddCity(new City { Label = "Mont-Dore" });
                provinceSud.AddCity(new City { Label = "Nouméa" });
                provinceSud.AddCity(new City { Label = "Dumbéa" });
                provinceSud.AddCity(new City { Label = "Païta" });
                provinceSud.AddCity(new City { Label = "Boulouparis" });
                provinceSud.AddCity(new City { Label = "La Foa" });
                provinceSud.AddCity(new City { Label = "Sarraméa" });
                provinceSud.AddCity(new City { Label = "Farino" });
                provinceSud.AddCity(new City { Label = "Moindou" });
                provinceSud.AddCity(new City { Label = "Bourail" });
                provinceSud.AddCity(new City { Label = "Poya Sud" });
                _repository.Save(provinceSud);


                Province ilesLoyaute = new Province();
                ilesLoyaute.Label = "Iles Loyauté";
                ilesLoyaute.AddCity(new City { Label = "Ouvéa" });
                ilesLoyaute.AddCity(new City { Label = "Lifou" });
                ilesLoyaute.AddCity(new City { Label = "Maré" });
                _repository.Save(ilesLoyaute);

                _repository.Flush();
            }
             
        }

        public void InsertInMemoryData()
        {
            //-------------------------------------------
            //         LOCATION REFERENCE TABLES
            //-------------------------------------------
            using (ITransaction transaction = _sessionFactory.GetCurrentSession().BeginTransaction())
            {

                InsertLocations();
                City c = _repository.GetAll<City>().First();
                //-------------------------------------------
                //         USER TABLE
                //-------------------------------------------

                //Create User 1
                User user = new User();
                user.Email = "bruno.deprez@gmail.com";
                user.Password = "mypassword";
                user.Firstname = "Bruno";
                user.Lastname = "Secret";
                _sessionFactory.GetCurrentSession().Save(user);

                //Create User 2
                User user2 = new User();
                user2.Email = "nicolas.raynaud@gmail.com";
                user2.Password = "mypassword";
                user2.Firstname = "Nicolas";
                user2.Lastname = "Secret";
                _sessionFactory.GetCurrentSession().Save(user);

                //-------------------------------------------
                //         CATEGORY TABLES
                //-------------------------------------------


                CategoryGroup vehicles = new CategoryGroup();
                vehicles.Label = "Vehicules";
                vehicles.AddCategory(new CategoryElement { Label = "Voitures" });
                vehicles.AddCategory(new CategoryElement { Label = "Motos" });
                vehicles.AddCategory(new CategoryElement { Label = "Utlitaires" });
                _sessionFactory.GetCurrentSession().Save(vehicles);

                CategoryGroup nautisme = new CategoryGroup();
                nautisme.Label = "Nautisme";
                nautisme.AddCategory(new CategoryElement { Label = "Bateaux à moteur" });
                nautisme.AddCategory(new CategoryElement { Label = "Kite Surf" });
                nautisme.AddCategory(new CategoryElement { Label = "Planches à voile" });
                _sessionFactory.GetCurrentSession().Save(nautisme);

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
                    ad.IsOffer = true;
                    ad.CreationDate = DateTime.Now
                        .AddDays(Faker.RandomNumber.Next(-7, 7))
                        .AddHours(Faker.RandomNumber.Next(1, 23))
                        .AddMinutes(Faker.RandomNumber.Next(1, 59))
                        .AddSeconds(Faker.RandomNumber.Next(1, 59));
                    ad.Price = Faker.RandomNumber.Next(1, 300000);
                    ad.PhoneNumber = String.Format("{0}.{1}.{2}", Faker.RandomNumber.Next(20, 99), Faker.RandomNumber.Next(20, 99), Faker.RandomNumber.Next(20, 99));
                    if (i == 1)
                        ad.AddImage(img1);
                    if (i == 2)
                        ad.AddImage(img2);
                    user.AddAd(ad);
                    c.AddAd(ad);
                    vehicles.Categories[0].AddAd(ad);
                    _sessionFactory.GetCurrentSession().Save(ad);
                }

                _sessionFactory.GetCurrentSession().Update(img1);
                _sessionFactory.GetCurrentSession().Update(img2);


                //-------------------------------------------
                //         REFERENCE TABLES
                //-------------------------------------------

                //Car Brands
                _sessionFactory.GetCurrentSession().Save(new CarBrand { Label = "Renault" });
                _sessionFactory.GetCurrentSession().Save(new CarBrand { Label = "Peugeot" });
                _sessionFactory.GetCurrentSession().Save(new CarBrand { Label = "Citroën" });
                _sessionFactory.GetCurrentSession().Save(new CarBrand { Label = "Hyundai" });
                _sessionFactory.GetCurrentSession().Save(new CarBrand { Label = "Great Wall" });
                _sessionFactory.GetCurrentSession().Save(new CarBrand { Label = "Mercedes" });
                _sessionFactory.GetCurrentSession().Save(new CarBrand { Label = "BMW" });
                _sessionFactory.GetCurrentSession().Save(new CarBrand { Label = "Audi" });
                _sessionFactory.GetCurrentSession().Save(new CarBrand { Label = "Nissan" });

                _sessionFactory.GetCurrentSession().Save(new CarFuel { Label = "Essence" });
                _sessionFactory.GetCurrentSession().Save(new CarFuel { Label = "Diesel" });
                _sessionFactory.GetCurrentSession().Save(new CarFuel { Label = "GPL" });
                _sessionFactory.GetCurrentSession().Save(new CarFuel { Label = "Electrique" });
                _sessionFactory.GetCurrentSession().Save(new CarFuel { Label = "Autre" });

                _sessionFactory.GetCurrentSession().Flush();

                transaction.Commit();
            }
        }
    }
}