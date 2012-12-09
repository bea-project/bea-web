using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bea.Domain;
using Bea.Domain.Location;
using NHibernate;
using NHibernate.Linq;

namespace Bea.Web.NhibernateHelper
{
    public class InMemoryDataInjector
    {
        private ISessionFactory _sessionFactory;

        public InMemoryDataInjector(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
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
                //         AD TABLE
                //-------------------------------------------


                //Create Ad 1
                Ad ad = new Ad();
                ad.Title = "Le bateau en Alu a ma tontine";
                ad.Body = "Awa j'vend la plate a ma tontine pour allez baigner a la passe de Dumbea";

                //Add User 1 as creator of Ad1, automatically setting the created by for the Ad
                user.AddAd(ad);
                city.AddAd(ad);
                _sessionFactory.GetCurrentSession().SaveOrUpdate(user);
                _sessionFactory.GetCurrentSession().SaveOrUpdate(city);
                _sessionFactory.GetCurrentSession().Save(ad);

                //Create Ad 2
                Ad ad2 = new Ad();
                ad2.Title = "Suzuki GSXR 1000";
                ad2.Body = "Une moto qu'elle envoye";

                //Add User 2 as creator of Ad2, automatically setting the created by for the Ad
                user2.AddAd(ad2);
                city2.AddAd(ad2);
                _sessionFactory.GetCurrentSession().SaveOrUpdate(user2);
                _sessionFactory.GetCurrentSession().SaveOrUpdate(city2);
                _sessionFactory.GetCurrentSession().Save(ad2);

                _sessionFactory.GetCurrentSession().Flush();

                transaction.Commit();
            }
        }
    }
}