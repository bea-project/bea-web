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
            _repository.Save(new VehicleBrand { Label = "Land Rover", IsMainBrand = true });
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
            _repository.Save(new VehicleBrand { Label = "Porsche", IsMainBrand = true });
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

            _repository.Save(new MotoBrand { Label = "Aprilia" });
            _repository.Save(new MotoBrand { Label = "Benelli" });
            _repository.Save(new MotoBrand { Label = "BMW" });
            _repository.Save(new MotoBrand { Label = "Buell" });
            _repository.Save(new MotoBrand { Label = "Cagiva" });
            _repository.Save(new MotoBrand { Label = "Daelim" });
            _repository.Save(new MotoBrand { Label = "Dax" });
            _repository.Save(new MotoBrand { Label = "Dayun" });
            _repository.Save(new MotoBrand { Label = "Derbi" });
            _repository.Save(new MotoBrand { Label = "Ducati" });
            _repository.Save(new MotoBrand { Label = "Gas" });
            _repository.Save(new MotoBrand { Label = "Gilera" });
            _repository.Save(new MotoBrand { Label = "Harley-Davidson" });
            _repository.Save(new MotoBrand { Label = "Honda" });
            _repository.Save(new MotoBrand { Label = "Husqvarna" });
            _repository.Save(new MotoBrand { Label = "Kawasaki" });
            _repository.Save(new MotoBrand { Label = "KTM" });
            _repository.Save(new MotoBrand { Label = "Kymco" });
            _repository.Save(new MotoBrand { Label = "MBK" });
            _repository.Save(new MotoBrand { Label = "MBS" });
            _repository.Save(new MotoBrand { Label = "Moto Guzzi" });
            _repository.Save(new MotoBrand { Label = "Peugeot" });
            _repository.Save(new MotoBrand { Label = "Piaggio" });
            _repository.Save(new MotoBrand { Label = "Suzuki" });
            _repository.Save(new MotoBrand { Label = "Sym" });
            _repository.Save(new MotoBrand { Label = "Triumph" });
            _repository.Save(new MotoBrand { Label = "Vespa" });
            _repository.Save(new MotoBrand { Label = "Yamaha" });
            _repository.Save(new MotoBrand { Label = "Autre" });


            _repository.Save(new CarFuel { Label = "Essence" });
            _repository.Save(new CarFuel { Label = "Diesel" });
            //_repository.Save(new CarFuel { Label = "GPL" });
            //_repository.Save(new CarFuel { Label = "Electrique" });
            _repository.Save(new CarFuel { Label = "Autre" });

            _repository.Save(new MotorBoatType { Label = "Aluminium" });
            _repository.Save(new MotorBoatType { Label = "Fibre" });
            _repository.Save(new MotorBoatType { Label = "Semi-Rigide" });
            _repository.Save(new MotorBoatType { Label = "Bois" });
            _repository.Save(new MotorBoatType { Label = "Autre" });

            _repository.Save(new SailingBoatType { Label = "Aluminium" });
            _repository.Save(new SailingBoatType { Label = "Fibre" });
            _repository.Save(new SailingBoatType { Label = "Acier" });
            _repository.Save(new SailingBoatType { Label = "Bois" });
            _repository.Save(new SailingBoatType { Label = "Autre" });

            _repository.Save(new SailingBoatHullType { Label = "Monocoque" });
            _repository.Save(new SailingBoatHullType { Label = "Catamaran" });
            _repository.Save(new SailingBoatHullType { Label = "Trimaran" });
            _repository.Save(new SailingBoatHullType { Label = "Autre" });

            _repository.Save(new MotorBoatEngineType { Label = "2-Temps" });
            _repository.Save(new MotorBoatEngineType { Label = "4-Temps" });
            _repository.Save(new MotorBoatEngineType { Label = "Turbine" });

            _repository.Save(new WaterSportType { Label = "Kite-Surf" });
            _repository.Save(new WaterSportType { Label = "Surf" });
            _repository.Save(new WaterSportType { Label = "Wake" });
            _repository.Save(new WaterSportType { Label = "Plongee" });
            _repository.Save(new WaterSportType { Label = "Chasse sous-marine" });
            _repository.Save(new WaterSportType { Label = "Planche a voile" });
            _repository.Save(new WaterSportType { Label = "Autre" });

            _repository.Flush();
        }

        public void InsertCategories()
        {
            if (_repository.CountAll<Category>() != 0)
                return;

            CategoryGroup vehicles = new CategoryGroup();
            vehicles.Label = "Véhicules";
            vehicles.AddCategory(new Category { Label = "Voitures", Type = AdTypeEnum.CarAd });
            vehicles.AddCategory(new Category { Label = "Motos", Type = AdTypeEnum.MotoAd });
            vehicles.AddCategory(new Category { Label = "Voiturettes", Type = AdTypeEnum.VehiculeAd });
            vehicles.AddCategory(new Category { Label = "4 x 4", Type = AdTypeEnum.CarAd });
            vehicles.AddCategory(new Category { Label = "Autres Vehicules", Type = AdTypeEnum.OtherVehiculeAd });
            vehicles.AddCategory(new Category { Label = "Equipement auto", Type = AdTypeEnum.Ad });
            vehicles.AddCategory(new Category { Label = "Equipement moto", Type = AdTypeEnum.Ad });
            vehicles.AddCategory(new Category { Label = "Tuning", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(vehicles);

            CategoryGroup nautisme = new CategoryGroup();
            nautisme.Label = "Nautisme";
            nautisme.AddCategory(new Category { Label = "Bateaux à moteur", Type = AdTypeEnum.MotorBoatAd });
            nautisme.AddCategory(new Category { Label = "Propulseurs", Type = AdTypeEnum.MotorBoatEngineAd });
            nautisme.AddCategory(new Category { Label = "Voiliers", Type = AdTypeEnum.SailingBoatAd });
            nautisme.AddCategory(new Category { Label = "Sports / Loisirs", Type = AdTypeEnum.WaterSportAd });
            nautisme.AddCategory(new Category { Label = "Matériel de pêche", Type = AdTypeEnum.Ad });
            nautisme.AddCategory(new Category { Label = "Planches à voile", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(nautisme);

            CategoryGroup realEstate = new CategoryGroup();
            realEstate.Label = "Immobilier";
            realEstate.AddCategory(new Category { Label = "Locations", Type = AdTypeEnum.RealEstateAd });
            realEstate.AddCategory(new Category { Label = "Ventes immobilières", Type = AdTypeEnum.RealEstateAd });
            realEstate.AddCategory(new Category { Label = "Colocations", Type = AdTypeEnum.RealEstateAd });
            realEstate.AddCategory(new Category { Label = "Locations de vacances", Type = AdTypeEnum.RealEstateAd });
            realEstate.AddCategory(new Category { Label = "Bureaux et commerces", Type = AdTypeEnum.RealEstateAd });
            _sessionFactory.GetCurrentSession().Save(realEstate);

            CategoryGroup multimedia = new CategoryGroup();
            multimedia.Label = "Multimedia";
            multimedia.AddCategory(new Category { Label = "Informatique", Type = AdTypeEnum.Ad });
            multimedia.AddCategory(new Category { Label = "Consoles & Jeux vidéo", Type = AdTypeEnum.Ad });
            multimedia.AddCategory(new Category { Label = "Image & Son", Type = AdTypeEnum.Ad });
            multimedia.AddCategory(new Category { Label = "Téléphonie", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(multimedia);

            CategoryGroup maison = new CategoryGroup();
            maison.Label = "Maison";
            maison.AddCategory(new Category { Label = "Meubles", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Electroménager", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Arts de la table", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Décoration", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Linge de maison", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Bricolage", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Jardin", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Vêtements", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Chaussures", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Accessoires & Bagagerie", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Montres & Bijoux", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Equipements bébé", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { Label = "Vêtements bébé", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(maison);

            CategoryGroup loisirs = new CategoryGroup();
            loisirs.Label = "Loisirs";
            loisirs.AddCategory(new Category { Label = "DVD & Films", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { Label = "CD & Musique", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { Label = "Livres", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { Label = "Animaux", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { Label = "Vélos", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { Label = "Sports & Hobbies", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { Label = "Instruments de musique", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { Label = "Collections", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { Label = "Jeux & Jouets", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(loisirs);

            CategoryGroup autre = new CategoryGroup();
            autre.Label = "Autres";
            autre.AddCategory(new Category { Label = "Autres", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(autre);
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
                InsertCategories();

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
                //         IMAGES
                //-------------------------------------------
                AdImage img1 = new AdImage();
                img1.Id = Guid.Parse("acd04523-f2fc-47b5-a7af-50b33c49694f");
                img1.IsPrimary = true;
                img1.UploadedDate = new DateTime();
                log.Info(_sessionFactory.GetCurrentSession().Save(img1));

                AdImage img2 = new AdImage();
                img2.Id = Guid.Parse("262dcc07-8c90-4af8-b99c-12e48afac89f");
                img2.IsPrimary = true;
                img2.UploadedDate = new DateTime();
                log.Info(_sessionFactory.GetCurrentSession().Save(img2));

                //-------------------------------------------
                //         AD TABLE
                //-------------------------------------------

                var normalCategories = _sessionFactory.GetCurrentSession().Query<Category>()
                    .Where(x => x.Type == AdTypeEnum.Ad)
                    .ToList();

                int nbCategories = normalCategories.Count;
                int nbCities = _repository.CountAll<City>();

                for (int i = 1; i < 31; i++)
                {
                    BaseAd ad = null;

                    ad = new Ad();
                    normalCategories[Faker.RandomNumber.Next(0, nbCategories - 1)].AddAd(ad);

                    Faker.Lorem.Words(3).ForEach(s => ad.Title += " "+s);
                    ad.Body = Faker.Lorem.Paragraph();
                    ad.IsOffer = true;
                    
                    ad.CreationDate = DateTime.Now
                        .AddDays(Faker.RandomNumber.Next(-7, 0))
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

                    ad.IsActivated = false;
                    ad.ActivationToken = "activateme";

                    _repository.Get<City>(Faker.RandomNumber.Next(1, nbCities)).AddAd(ad);
                    
                    _sessionFactory.GetCurrentSession().Save(ad);

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