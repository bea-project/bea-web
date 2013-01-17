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
using Bea.Domain.Categories;
using Bea.Domain.Reference;
using Bea.Domain.Ads;
using Bea.Domain.Search;

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

        public void InsertVehicleReferences()
        {
            if (_repository.CountAll<VehicleBrand>() != 0)
                return;

            _repository.Save(new VehicleBrand { Label = "Alfa Romeo", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Alpina" });
            _repository.Save(new VehicleBrand { Label = "Ariel" });
            _repository.Save(new VehicleBrand { Label = "Ascari" });
            _repository.Save(new VehicleBrand { Label = "Asia Motors" });
            _repository.Save(new VehicleBrand { Label = "Aston Martin" });
            _repository.Save(new VehicleBrand { Label = "Audi", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Austin" });
            _repository.Save(new VehicleBrand { Label = "Autobianchi" });
            _repository.Save(new VehicleBrand { Label = "Bentley" });
            _repository.Save(new VehicleBrand { Label = "BMW", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Bristol" });
            _repository.Save(new VehicleBrand { Label = "Brooke" });
            _repository.Save(new VehicleBrand { Label = "Buick" });
            _repository.Save(new VehicleBrand { Label = "Cadillac" });
            _repository.Save(new VehicleBrand { Label = "Callaway" });
            _repository.Save(new VehicleBrand { Label = "Campagna" });
            _repository.Save(new VehicleBrand { Label = "Carver" });
            _repository.Save(new VehicleBrand { Label = "Caterham" });
            _repository.Save(new VehicleBrand { Label = "Chevrolet", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Chrysler", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Citroen", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Corvette" });
            _repository.Save(new VehicleBrand { Label = "Dacia", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Daewoo", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Daihatsu", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Daimler" });
            _repository.Save(new VehicleBrand { Label = "Datsun" });
            _repository.Save(new VehicleBrand { Label = "Dodge" });
            _repository.Save(new VehicleBrand { Label = "Donkervoort" });
            _repository.Save(new VehicleBrand { Label = "Elfin" });
            _repository.Save(new VehicleBrand { Label = "Ferrari" });
            _repository.Save(new VehicleBrand { Label = "Fiat", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Ford", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "FSO" });
            _repository.Save(new VehicleBrand { Label = "Galloper" });
            _repository.Save(new VehicleBrand { Label = "Gumpert" });
            _repository.Save(new VehicleBrand { Label = "Holden" });
            _repository.Save(new VehicleBrand { Label = "Honda", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Hummer" });
            _repository.Save(new VehicleBrand { Label = "Hyundai", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Infiniti" });
            _repository.Save(new VehicleBrand { Label = "Innocenti" });
            _repository.Save(new VehicleBrand { Label = "Invicta" });
            _repository.Save(new VehicleBrand { Label = "Isuzu", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Jaguar", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Jeep", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "JMC" });
            _repository.Save(new VehicleBrand { Label = "Josse" });
            _repository.Save(new VehicleBrand { Label = "Kia", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Koenigsegg" });
            _repository.Save(new VehicleBrand { Label = "Lada" });
            _repository.Save(new VehicleBrand { Label = "Lamborghini" });
            _repository.Save(new VehicleBrand { Label = "Lancia", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Land Rover" });
            _repository.Save(new VehicleBrand { Label = "Lexus" });
            _repository.Save(new VehicleBrand { Label = "Lincoln" });
            _repository.Save(new VehicleBrand { Label = "Lobini" });
            _repository.Save(new VehicleBrand { Label = "Lotus" });
            _repository.Save(new VehicleBrand { Label = "Marcos" });
            _repository.Save(new VehicleBrand { Label = "Maserati" });
            _repository.Save(new VehicleBrand { Label = "Maybach" });
            _repository.Save(new VehicleBrand { Label = "Mazda", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "MB Roadcars" });
            _repository.Save(new VehicleBrand { Label = "Mega" });
            _repository.Save(new VehicleBrand { Label = "Mercedes", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Mercury" });
            _repository.Save(new VehicleBrand { Label = "MG" });
            _repository.Save(new VehicleBrand { Label = "Mini" });
            _repository.Save(new VehicleBrand { Label = "Mitsubishi", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Morgan" });
            _repository.Save(new VehicleBrand { Label = "Morris" });
            _repository.Save(new VehicleBrand { Label = "NICE" });
            _repository.Save(new VehicleBrand { Label = "Nissan", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Noble" });
            _repository.Save(new VehicleBrand { Label = "Opel", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Pagani" });
            _repository.Save(new VehicleBrand { Label = "Perodua" });
            _repository.Save(new VehicleBrand { Label = "Peugeot", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "PGO" });
            _repository.Save(new VehicleBrand { Label = "Pontiac" });
            _repository.Save(new VehicleBrand { Label = "Porsche" });
            _repository.Save(new VehicleBrand { Label = "Princess" });
            _repository.Save(new VehicleBrand { Label = "Proton" });
            _repository.Save(new VehicleBrand { Label = "Radical" });
            _repository.Save(new VehicleBrand { Label = "Renault", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Rolls-Royce" });
            _repository.Save(new VehicleBrand { Label = "Rover" });
            _repository.Save(new VehicleBrand { Label = "Saab", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Saturn" });
            _repository.Save(new VehicleBrand { Label = "Seat", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Shelby" });
            _repository.Save(new VehicleBrand { Label = "Skoda", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Smart", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Spectre" });
            _repository.Save(new VehicleBrand { Label = "Spyker" });
            _repository.Save(new VehicleBrand { Label = "Ssangyong", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "SSC" });
            _repository.Save(new VehicleBrand { Label = "Subaru", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Superformance" });
            _repository.Save(new VehicleBrand { Label = "Suzuki", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Talbot" });
            _repository.Save(new VehicleBrand { Label = "Tata" });
            _repository.Save(new VehicleBrand { Label = "Tesla" });
            _repository.Save(new VehicleBrand { Label = "Toyota", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Triumph" });
            _repository.Save(new VehicleBrand { Label = "TVR" });
            _repository.Save(new VehicleBrand { Label = "Unique" });
            _repository.Save(new VehicleBrand { Label = "Vauxhall" });
            _repository.Save(new VehicleBrand { Label = "Volkswagen", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Volvo", IsMainBrand = true });
            _repository.Save(new VehicleBrand { Label = "Westfield" });
            _repository.Save(new VehicleBrand { Label = "Yugo" });
            _repository.Save(new VehicleBrand { Label = "Autre" });

            _repository.Save(new CarFuel { Label = "Essence" });
            _repository.Save(new CarFuel { Label = "Diesel" });
            _repository.Save(new CarFuel { Label = "GPL" });
            _repository.Save(new CarFuel { Label = "Electrique" });
            _repository.Save(new CarFuel { Label = "Autre" });

            _repository.Flush();
        }

        public void InsertInMemoryData()
        {
            //-------------------------------------------
            //         LOCATION REFERENCE TABLES
            //-------------------------------------------
            using (ITransaction transaction = _sessionFactory.GetCurrentSession().BeginTransaction())
            {

                InsertLocations();
                InsertVehicleReferences();

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
                vehicles.Label = "Véhicules";
                vehicles.AddCategory(new Category { Label = "Voitures", Type=AdTypeEnum.CarAd });
                vehicles.AddCategory(new Category { Label = "Motos", Type=AdTypeEnum.MotoAd });
                vehicles.AddCategory(new Category { Label = "Utlitaires", Type=AdTypeEnum.OtherVehiculeAd });
                _sessionFactory.GetCurrentSession().Save(vehicles);

                CategoryGroup nautisme = new CategoryGroup();
                nautisme.Label = "Nautisme";
                nautisme.AddCategory(new Category { Label = "Bateaux à moteur" });
                nautisme.AddCategory(new Category { Label = "Kite Surf" });
                nautisme.AddCategory(new Category { Label = "Planches à voile" });
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
                    BaseAd ad = null;

                    if (i % 2 == 0)
                    {
                        ad = new CarAd();
                        (ad as CarAd).Kilometers = Faker.RandomNumber.Next(2300, 350000);
                        (ad as CarAd).Year = Faker.RandomNumber.Next(1990, 2013);
                        (ad as CarAd).IsAutomatic = (i%5 == 0);
                    }
                    else
                        ad = new Ad();

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
                    ad.CreatedBy = user;
                    c.AddAd(ad);
                    vehicles.Categories[0].AddAd(ad);
                    _sessionFactory.GetCurrentSession().Save(ad);

                    SearchAdCache cacheAd = new SearchAdCache(ad);
                    _sessionFactory.GetCurrentSession().Save(cacheAd);
                }

                _sessionFactory.GetCurrentSession().Update(img1);
                _sessionFactory.GetCurrentSession().Update(img2);


                //-------------------------------------------
                //         REFERENCE TABLES
                //-------------------------------------------


                transaction.Commit();
            }
        }
    }
}