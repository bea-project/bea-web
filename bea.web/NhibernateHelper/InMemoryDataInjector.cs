﻿using System;
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
                provinceNord.AddCity(new City { LabelUrlPart = "Poya-Nord", Label = "Poya Nord" });
                provinceNord.AddCity(new City { LabelUrlPart = "Pouembout", Label = "Pouembout" });
                provinceNord.AddCity(new City { LabelUrlPart = "Kone", Label = "Koné" });
                provinceNord.AddCity(new City { LabelUrlPart = "Voh", Label = "Voh" });
                provinceNord.AddCity(new City { LabelUrlPart = "Kaala-Gomen", Label = "Kaala-Gomen" });
                provinceNord.AddCity(new City { LabelUrlPart = "Koumac", Label = "Koumac" });
                provinceNord.AddCity(new City { LabelUrlPart = "Poum", Label = "Poum" });
                provinceNord.AddCity(new City { LabelUrlPart = "Iles-Belep", Label = "Iles Belep" });
                provinceNord.AddCity(new City { LabelUrlPart = "Ouegoa", Label = "Ouégoa" });
                provinceNord.AddCity(new City { LabelUrlPart = "Pouebo", Label = "Pouébo" });
                provinceNord.AddCity(new City { LabelUrlPart = "Hienghene", Label = "Hienghène" });
                provinceNord.AddCity(new City { LabelUrlPart = "Touho", Label = "Touho" });
                provinceNord.AddCity(new City { LabelUrlPart = "Poindimie", Label = "Poindimié" });
                provinceNord.AddCity(new City { LabelUrlPart = "Ponerihouen", Label = "Ponérihouen" });
                provinceNord.AddCity(new City { LabelUrlPart = "Houailou", Label = "Houaïlou" });
                provinceNord.AddCity(new City { LabelUrlPart = "Kouaoua", Label = "Kouaoua" });
                provinceNord.AddCity(new City { LabelUrlPart = "Canala", Label = "Canala" });
                _repository.Save(provinceNord);

                Province provinceSud = new Province();
                provinceSud.Label = "Province Sud";

                City noumea = new City();
                noumea.Label = "Nouméa";
                noumea.LabelUrlPart = "Noumea";

                noumea.AddDistrict(new District { Label = "Centre-Ville" });
                noumea.AddDistrict(new District { Label = "Nouville" });
                noumea.AddDistrict(new District { Label = "Quartier Latin" });
                noumea.AddDistrict(new District { Label = "Vallée du Génie" });
                noumea.AddDistrict(new District { Label = "Artillerie" });
                noumea.AddDistrict(new District { Label = "Orphelinat" });
                noumea.AddDistrict(new District { Label = "Baie des Citrons" });
                noumea.AddDistrict(new District { Label = "Anse Vata" });
                noumea.AddDistrict(new District { Label = "Val Plaisance" });
                noumea.AddDistrict(new District { Label = "N'Géa" });
                noumea.AddDistrict(new District { Label = "Receiving" });
                noumea.AddDistrict(new District { Label = "Motor Pool" });
                noumea.AddDistrict(new District { Label = "Trianon" });
                noumea.AddDistrict(new District { Label = "Riviere-Salée" });
                noumea.AddDistrict(new District { Label = "Ducos" });
                noumea.AddDistrict(new District { Label = "Magenta" });
                noumea.AddDistrict(new District { Label = "Haut-Magenta" });
                noumea.AddDistrict(new District { Label = "Aérodrome" });
                noumea.AddDistrict(new District { Label = "PK4" });
                noumea.AddDistrict(new District { Label = "PK6" });
                noumea.AddDistrict(new District { Label = "PK7" });
                noumea.AddDistrict(new District { Label = "Tina" });
                noumea.AddDistrict(new District { Label = "Normandie" });
                noumea.AddDistrict(new District { Label = "Vallée des Colons" });
                noumea.AddDistrict(new District { Label = "Faubourg Blanchot" });
                noumea.AddDistrict(new District { Label = "Montravel" });
                noumea.AddDistrict(new District { Label = "Montagne Coupée" });
                noumea.AddDistrict(new District { Label = "Vallée du Tir" });
                noumea.AddDistrict(new District { Label = "Doniambo" });
                noumea.AddDistrict(new District { Label = "Ouémo" });
                noumea.AddDistrict(new District { Label = "Portes de Fer" });
                provinceSud.AddCity(noumea);
                _repository.Save(noumea);

                City dumbea = new City();
                dumbea.Label = "Dumbéa";
                dumbea.LabelUrlPart = "Dumbea";
                dumbea.AddDistrict(new District { Label = "Auteuil" });
                dumbea.AddDistrict(new District { Label = "Centre" });
                dumbea.AddDistrict(new District { Label = "Dumbéa sur Mer" });
                dumbea.AddDistrict(new District { Label = "Koghi" });
                dumbea.AddDistrict(new District { Label = "Koutio" });
                dumbea.AddDistrict(new District { Label = "Nakutakoin" });
                dumbea.AddDistrict(new District { Label = "Plaine de Koé" });
                dumbea.AddDistrict(new District { Label = "Pointe à la Dorade" });
                dumbea.AddDistrict(new District { Label = "ZAC Panda" });
                provinceSud.AddCity(dumbea);
                _repository.Save(dumbea);

                City montDor = new City();
                montDor.Label = "Mont-Dore";
                montDor.LabelUrlPart = "Mont-Dore";
                montDor.AddDistrict(new District { Label = "Boulari" });
                montDor.AddDistrict(new District { Label = "La Conception" });
                montDor.AddDistrict(new District { Label = "La Coulée" });
                montDor.AddDistrict(new District { Label = "Mont-Dore Sud" });
                montDor.AddDistrict(new District { Label = "Plum" });
                montDor.AddDistrict(new District { Label = "Pont des Francais" });
                montDor.AddDistrict(new District { Label = "Robinson" });
                montDor.AddDistrict(new District { Label = "Saint-Louis" });
                montDor.AddDistrict(new District { Label = "Saint-Michel" });
                montDor.AddDistrict(new District { Label = "Vallon-Dore" });
                montDor.AddDistrict(new District { Label = "Yahoué" });
                provinceSud.AddCity(montDor);
                _repository.Save(montDor);

                City paita = new City();
                paita.Label = "Païta";
                paita.LabelUrlPart = "Paita";
                paita.AddDistrict(new District { Label = "Val Boisé" });
                paita.AddDistrict(new District { Label = "Tontouta" });
                paita.AddDistrict(new District { Label = "Tamoa" });
                paita.AddDistrict(new District { Label = "Savannah" });
                paita.AddDistrict(new District { Label = "Centre" });
                paita.AddDistrict(new District { Label = "Naia" });
                paita.AddDistrict(new District { Label = "Mont Mou" });
                paita.AddDistrict(new District { Label = "Beauvallon" });

                provinceSud.AddCity(paita);
                _repository.Save(paita);

                provinceSud.AddCity(new City { LabelUrlPart = "Thio", Label = "Thio" });
                provinceSud.AddCity(new City { LabelUrlPart = "Yate", Label = "Yaté" });
                provinceSud.AddCity(new City { LabelUrlPart = "Ile-des-Pins", Label = "Ile des Pins" });
                provinceSud.AddCity(new City { LabelUrlPart = "Boulouparis", Label = "Boulouparis" });
                provinceSud.AddCity(new City { LabelUrlPart = "La-Foa", Label = "La Foa" });
                provinceSud.AddCity(new City { LabelUrlPart = "Sarramea", Label = "Sarraméa" });
                provinceSud.AddCity(new City { LabelUrlPart = "Farino", Label = "Farino" });
                provinceSud.AddCity(new City { LabelUrlPart = "Moindou", Label = "Moindou" });
                provinceSud.AddCity(new City { LabelUrlPart = "Bourail", Label = "Bourail" });
                provinceSud.AddCity(new City { LabelUrlPart = "Poya-Sud", Label = "Poya Sud" });
                _repository.Save(provinceSud);

                Province ilesLoyaute = new Province();
                ilesLoyaute.Label = "Iles Loyauté";
                ilesLoyaute.AddCity(new City { LabelUrlPart = "Ouvea", Label = "Ouvéa" });
                ilesLoyaute.AddCity(new City { LabelUrlPart = "Lifou", Label = "Lifou" });
                ilesLoyaute.AddCity(new City { LabelUrlPart = "Mare", Label = "Maré" });
                _repository.Save(ilesLoyaute);

                _repository.Flush();
            }

        }

        public void InsertReferences()
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
            _repository.Save(new WaterSportType { Label = "Plongée" });
            _repository.Save(new WaterSportType { Label = "Chasse sous-marine" });
            _repository.Save(new WaterSportType { Label = "Planche a voile" });
            _repository.Save(new WaterSportType { Label = "Autre" });

            _repository.Save(new RealEstateType { Label = "Maison" });
            _repository.Save(new RealEstateType { Label = "Appartement" });
            _repository.Save(new RealEstateType { Label = "Terrain" });
            _repository.Save(new RealEstateType { Label = "Local Commercial" });
            _repository.Save(new RealEstateType { Label = "Dock" });

            _repository.Save(new DeletionReason { Label = "J'ai vendu / loué mon bien sur bea.nc" });
            _repository.Save(new DeletionReason { Label = "J'ai vendu / loué mon bien sur un autre site" });
            _repository.Save(new DeletionReason { Label = "J'ai vendu / loué mon bien par un autre moyen" });
            _repository.Save(new DeletionReason { Label = "Je souhaite renouveler mon annonce pour la faire apparaitre en tête de liste" });

            _repository.Save(new SpamAdType { Label = "Annonce commerciale" });
            _repository.Save(new SpamAdType { Label = "Mauvaise rubrique" });
            _repository.Save(new SpamAdType { Label = "Contenu à caractère diffamatoire" });
            _repository.Save(new SpamAdType { Label = "Contenu vulgaire" });
            _repository.Save(new SpamAdType { Label = "Fraude" });
            _repository.Save(new SpamAdType { Label = "Autre" });

            _repository.Flush();
        }

        public void InsertCategories()
        {
            if (_repository.CountAll<Category>() != 0)
                return;

            Category vehicles = new Category();
            vehicles.ImageName = "car";
            vehicles.Label = "Véhicules";
            vehicles.LabelUrlPart = "Vehicules";
            vehicles.AddCategory(new Category { ImageName = "car", Label = "Voitures", LabelUrlPart = "Voitures", Type = AdTypeEnum.CarAd });
            vehicles.AddCategory(new Category { ImageName = "car", Label = "Motos & Scooters", LabelUrlPart = "Motos-Scooters", Type = AdTypeEnum.MotoAd });
            vehicles.AddCategory(new Category { ImageName = "car", Label = "Voiturettes", LabelUrlPart = "Voiturettes", Type = AdTypeEnum.VehiculeAd });
            vehicles.AddCategory(new Category { ImageName = "car", Label = "4 x 4", LabelUrlPart = "4x4-Tout-Terrain", Type = AdTypeEnum.CarAd });
            vehicles.AddCategory(new Category { ImageName = "car", Label = "Autres Véhicules", LabelUrlPart = "AutresVehicules", Type = AdTypeEnum.OtherVehiculeAd });
            vehicles.AddCategory(new Category { ImageName = "car", Label = "Equipement auto", LabelUrlPart = "Equipement-Auto", Type = AdTypeEnum.Ad });
            vehicles.AddCategory(new Category { ImageName = "car", Label = "Equipement moto", LabelUrlPart = "Equipement-Moto", Type = AdTypeEnum.Ad });
            vehicles.AddCategory(new Category { ImageName = "car", Label = "Tuning", LabelUrlPart = "Tuning", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(vehicles);

            Category nautisme = new Category();
            nautisme.ImageName = "ship";
            nautisme.Label = "Nautisme";
            nautisme.LabelUrlPart = "Nautisme";
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Bateaux à moteur", LabelUrlPart = "BateauxMoteur", Type = AdTypeEnum.MotorBoatAd });
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Propulseurs", LabelUrlPart = "Propulseurs", Type = AdTypeEnum.MotorBoatEngineAd });
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Voiliers", LabelUrlPart = "Voiliers", Type = AdTypeEnum.SailingBoatAd });
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Accastillage / Sécurité", LabelUrlPart = "Accastillage-Securite", Type = AdTypeEnum.Ad });
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Remorques", LabelUrlPart = "Remorques", Type = AdTypeEnum.Ad });
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Sports / Loisirs", LabelUrlPart = "Sports-Loisirs", Type = AdTypeEnum.WaterSportAd });
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Matériel de pêche", LabelUrlPart = "Peche", Type = AdTypeEnum.Ad });
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Moto Marines", LabelUrlPart = "MotoMarines", Type = AdTypeEnum.Ad });
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Equipement / Electronique", LabelUrlPart = "Equipement-Electronique", Type = AdTypeEnum.Ad });
            nautisme.AddCategory(new Category { ImageName = "ship", Label = "Divers", LabelUrlPart = "Divers", Type = AdTypeEnum.Ad });

            _sessionFactory.GetCurrentSession().Save(nautisme);

            Category realEstate = new Category();
            realEstate.ImageName = "building";
            realEstate.Label = "Immobilier";
            realEstate.LabelUrlPart = "Immobilier";
            realEstate.AddCategory(new Category { ImageName = "building", Label = "Locations", LabelUrlPart = "Locations", Type = AdTypeEnum.RealEstateAd });
            realEstate.AddCategory(new Category { ImageName = "building", Label = "Ventes immobilières", LabelUrlPart = "Ventes", Type = AdTypeEnum.RealEstateAd });
            realEstate.AddCategory(new Category { ImageName = "building", Label = "Colocations", LabelUrlPart = "Colocations", Type = AdTypeEnum.RealEstateAd });
            realEstate.AddCategory(new Category { ImageName = "building", Label = "Locations de vacances", LabelUrlPart = "Vacances", Type = AdTypeEnum.RealEstateAd });
            realEstate.AddCategory(new Category { ImageName = "building", Label = "Gardiennage", LabelUrlPart = "Gardiennage", Type = AdTypeEnum.RealEstateAd });
            _sessionFactory.GetCurrentSession().Save(realEstate);

            Category multimedia = new Category();
            multimedia.ImageName = "tv";
            multimedia.Label = "Multimedia";
            multimedia.LabelUrlPart = "Multimedia";
            multimedia.AddCategory(new Category { ImageName = "tv", Label = "Informatique", LabelUrlPart = "Informatique", Type = AdTypeEnum.Ad });
            multimedia.AddCategory(new Category { ImageName = "tv", Label = "Consoles & Jeux vidéo", LabelUrlPart = "Jeux-Videos-Consoles", Type = AdTypeEnum.Ad });
            multimedia.AddCategory(new Category { ImageName = "tv", Label = "Image & Son", LabelUrlPart = "Image-Son", Type = AdTypeEnum.Ad });
            multimedia.AddCategory(new Category { ImageName = "tv", Label = "Téléphonie", LabelUrlPart = "Telephonie", Type = AdTypeEnum.Ad });
            multimedia.AddCategory(new Category { ImageName = "tv", Label = "DVD & Films", LabelUrlPart = "DVD-Films", Type = AdTypeEnum.Ad });
            multimedia.AddCategory(new Category { ImageName = "tv", Label = "CD & Musique", LabelUrlPart = "CD-Musique", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(multimedia);

            Category maison = new Category();
            maison.ImageName = "home";
            maison.Label = "Maison";
            maison.LabelUrlPart = "Maison";
            maison.AddCategory(new Category { ImageName = "home", Label = "Meubles", LabelUrlPart = "Meubles", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { ImageName = "home", Label = "Electroménager", LabelUrlPart = "Electromenager", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { ImageName = "home", Label = "Arts de la table", LabelUrlPart = "Arts-Table", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { ImageName = "home", Label = "Décoration", LabelUrlPart = "Decoration", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { ImageName = "home", Label = "Linge de maison", LabelUrlPart = "Linge-Maison", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { ImageName = "home", Label = "Jardin & Bricolage", LabelUrlPart = "Bricolage", Type = AdTypeEnum.Ad });
            maison.AddCategory(new Category { ImageName = "home", Label = "Bébé", LabelUrlPart = "Bebe", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(maison);

            Category beaute = new Category();
            beaute.ImageName = "hanger";
            beaute.Label = "Beauté & Mode";
            beaute.LabelUrlPart = "Beaute-Mode";
            beaute.AddCategory(new Category { ImageName = "home", Label = "Vêtements", LabelUrlPart = "Vetements", Type = AdTypeEnum.Ad });
            beaute.AddCategory(new Category { ImageName = "home", Label = "Chaussures", LabelUrlPart = "Chassures", Type = AdTypeEnum.Ad });
            beaute.AddCategory(new Category { ImageName = "home", Label = "Accessoires & Bagagerie", LabelUrlPart = "Accessoires-Bagagerie", Type = AdTypeEnum.Ad });
            beaute.AddCategory(new Category { ImageName = "home", Label = "Montres & Bijoux", LabelUrlPart = "Montres-Bijoux", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(beaute);

            Category loisirs = new Category();
            loisirs.ImageName = "ball";
            loisirs.Label = "Loisirs";
            loisirs.LabelUrlPart = "Loisirs";
            loisirs.AddCategory(new Category { ImageName = "ball", Label = "Livres", LabelUrlPart = "Livres", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { ImageName = "ball", Label = "Animaux", LabelUrlPart = "Animaux", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { ImageName = "ball", Label = "Vélos", LabelUrlPart = "Velos", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { ImageName = "ball", Label = "Sports & Hobbies", LabelUrlPart = "Sports-Hobbies", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { ImageName = "ball", Label = "Instruments de musique", LabelUrlPart = "Instruments-Musique", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { ImageName = "ball", Label = "Collections", LabelUrlPart = "Collections", Type = AdTypeEnum.Ad });
            loisirs.AddCategory(new Category { ImageName = "ball", Label = "Jeux & Jouets", LabelUrlPart = "Jeux-Jouets", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(loisirs);

            Category service = new Category();
            service.ImageName = "shake";
            service.Label = "Emplois & Services";
            service.LabelUrlPart = "Emplois-services";
            service.AddCategory(new Category { ImageName = "shake", Label = "Offres d'emplois", LabelUrlPart = "Offres-Emplois", Type = AdTypeEnum.Ad });
            service.AddCategory(new Category { ImageName = "shake", Label = "Offres de service", LabelUrlPart = "Offres-Service", Type = AdTypeEnum.Ad });
            _sessionFactory.GetCurrentSession().Save(service);

            //Category autre = new Category();
            //autre.Label = "Autres";
            //autre.LabelUrlPart = "Autres";
            //autre.AddCategory(new Category { Label = "Autres", LabelUrlPart = "Autres", Type = AdTypeEnum.Ad });
            //_sessionFactory.GetCurrentSession().Save(autre);
        }

        public void InsertReferenceData()
        {
            using (ITransaction transaction = _sessionFactory.GetCurrentSession().BeginTransaction())
            {
                InsertLocations();
                InsertReferences();
                InsertCategories();

                transaction.Commit();
            }
        }

        public void InsertInMemoryData()
        {
            //-------------------------------------------
            //         LOCATION REFERENCE TABLES
            //-------------------------------------------
            using (ITransaction transaction = _sessionFactory.GetCurrentSession().BeginTransaction())
            {
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
                _sessionFactory.GetCurrentSession().Save(user2);

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

                    Faker.Lorem.Words(3).ForEach(s => ad.Title += " " + s);
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