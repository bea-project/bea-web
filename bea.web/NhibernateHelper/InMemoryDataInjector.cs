using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bea.Domain;
using Bea.Domain.location;
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
                Province province = new Province();
                province.Label = "Province Nord";
                _sessionFactory.GetCurrentSession().Save(province);
                _sessionFactory.GetCurrentSession().Flush();

                province = new Province();
                province.Label = "Province Sud";
                _sessionFactory.GetCurrentSession().Save(province);
                _sessionFactory.GetCurrentSession().Flush();

                City city = new City();
                city.Label = "Noumea";
                province = _sessionFactory.GetCurrentSession().Query<Province>().Single(x => x.Label.Equals("Province Sud"));
                province.AddCity(city);
                _sessionFactory.GetCurrentSession().Save(city);
                _sessionFactory.GetCurrentSession().SaveOrUpdate(province);
                _sessionFactory.GetCurrentSession().Flush();

                city = new City();
                city.Label = "Koumac";
                province = _sessionFactory.GetCurrentSession().Query<Province>().Single(x => x.Label.Equals("Province Nord"));
                province.AddCity(city);
                _sessionFactory.GetCurrentSession().Save(city);
                _sessionFactory.GetCurrentSession().SaveOrUpdate(province);
                _sessionFactory.GetCurrentSession().Flush();
                //-------------------------------------------
                //         USER TABLE
                //-------------------------------------------


                //Create User 1
                User user = new User();
                user.Email = "bruno.deprez@gmail.com";
                user.Password = "mypassword";
                _sessionFactory.GetCurrentSession().Save(user);
                _sessionFactory.GetCurrentSession().Flush();

                //Create User 2
                user = new User();
                user.Email = "nicolas.raynaud@gmail.com";
                user.Password = "mypassword";
                _sessionFactory.GetCurrentSession().Save(user);
                _sessionFactory.GetCurrentSession().Flush();

                //-------------------------------------------
                //         AD TABLE
                //-------------------------------------------


                //Create Ad 1
                Ad ad = new Ad();
                ad.Title = "Le bateau en Alu a ma tontine";
                ad.Body = "Awa j'vend la plate a ma tontine pour allez baigner a la passe de Dumbea";

                //Add User 1 as creator of Ad1, automaticall setting the created by for the Ad
                user = _sessionFactory.GetCurrentSession().Query<User>().Single(x => x.Email.Equals("bruno.deprez@gmail.com"));
                city = _sessionFactory.GetCurrentSession().Query<City>().Single(x => x.Label.Equals("Noumea"));
                user.AddAd(ad);
                city.AddAd(ad);
                _sessionFactory.GetCurrentSession().SaveOrUpdate(user);
                _sessionFactory.GetCurrentSession().SaveOrUpdate(city);
                _sessionFactory.GetCurrentSession().Save(ad);
                _sessionFactory.GetCurrentSession().Flush();

                transaction.Commit();
            }
        }
    }
}