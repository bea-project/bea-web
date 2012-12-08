using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bea.domain;
using bea.domain.location;

namespace bea.dal
{
    public abstract class InMemoryData : InMemoryDatabase
    {
        protected InMemoryData()
        {

            //-------------------------------------------
            //         LOCATION REFERENCE TABLES
            //-------------------------------------------
            
            //Create Provinces
            Province province = new Province();
            province.label = "Province Nord";
            Session.Save(province);
            Session.Flush();

            province = new Province();
            province.label = "Province Sud";
            Session.Save(province);
            Session.Flush();

            City city = new City();
            city.label = "Noumea";
            province = new Repository<Province>(Session).FilterBy(x => x.label.Equals("Province Sud")).First();
            province.AddCity(city);
            Session.Save(city);
            Session.SaveOrUpdate(province);
            Session.Flush();

            city = new City();
            city.label = "Koumac";
            province = new Repository<Province>(Session).FilterBy(x => x.label.Equals("Province Nord")).First();
            province.AddCity(city);
            Session.Save(city);
            Session.SaveOrUpdate(province);
            Session.Flush();
            //-------------------------------------------
            //         USER TABLE
            //-------------------------------------------


            //Create User 1
            User user = new User();
            user.email = "bruno.deprez@gmail.com";
            user.password = "mypassword";
            Session.Save(user);
            Session.Flush();

            //Create User 2
            user = new User();
            user.email = "nicolas.raynaud@gmail.com";
            user.password = "mypassword";
            Session.Save(user);
            Session.Flush();

            //-------------------------------------------
            //         AD TABLE
            //-------------------------------------------


            //Create Ad 1
            Ad ad = new Ad();
            ad.title = "Le bateau en Alu a ma tontine";
            ad.body = "Awa j'vend la plate a ma tontine pour allez baigner a la passe de Dumbea";

            //Add User 1 as creator of Ad1, automaticall setting the created by for the Ad
            user = new Repository<User>(Session).FilterBy(x => x.email.Equals("bruno.deprez@gmail.com")).First();
            city = new Repository<City>(Session).FilterBy(x => x.label.Equals("Noumea")).First();
            user.AddAd(ad);
            city.AddAd(ad);
            Session.SaveOrUpdate(user);
            Session.SaveOrUpdate(city);
            Session.Save(ad);
            Session.Flush();

        }
    }
}
