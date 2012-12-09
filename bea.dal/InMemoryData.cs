using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;
using Bea.Domain.location;

namespace Bea.Dal
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
            province.Label = "Province Nord";
            Session.Save(province);
            Session.Flush();

            province = new Province();
            province.Label = "Province Sud";
            Session.Save(province);
            Session.Flush();

            City city = new City();
            city.Label = "Noumea";
            province = new Repository<Province>(Session).FilterBy(x => x.Label.Equals("Province Sud")).First();
            province.AddCity(city);
            Session.Save(city);
            Session.SaveOrUpdate(province);
            Session.Flush();

            city = new City();
            city.Label = "Koumac";
            province = new Repository<Province>(Session).FilterBy(x => x.Label.Equals("Province Nord")).First();
            province.AddCity(city);
            Session.Save(city);
            Session.SaveOrUpdate(province);
            Session.Flush();
            //-------------------------------------------
            //         USER TABLE
            //-------------------------------------------


            //Create User 1
            User user = new User();
            user.Email = "bruno.deprez@gmail.com";
            user.Password = "mypassword";
            Session.Save(user);
            Session.Flush();

            //Create User 2
            user = new User();
            user.Email = "nicolas.raynaud@gmail.com";
            user.Password = "mypassword";
            Session.Save(user);
            Session.Flush();

            //-------------------------------------------
            //         AD TABLE
            //-------------------------------------------


            //Create Ad 1
            Ad ad = new Ad();
            ad.Title = "Le bateau en Alu a ma tontine";
            ad.Body = "Awa j'vend la plate a ma tontine pour allez baigner a la passe de Dumbea";

            //Add User 1 as creator of Ad1, automaticall setting the created by for the Ad
            user = new Repository<User>(Session).FilterBy(x => x.Email.Equals("bruno.deprez@gmail.com")).First();
            city = new Repository<City>(Session).FilterBy(x => x.Label.Equals("Noumea")).First();
            user.AddAd(ad);
            city.AddAd(ad);
            Session.SaveOrUpdate(user);
            Session.SaveOrUpdate(city);
            Session.Save(ad);
            Session.Flush();

        }
    }
}
