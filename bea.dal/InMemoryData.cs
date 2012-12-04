using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bea.dal.entities;

namespace bea.dal
{
    public abstract class InMemoryData : InMemoryDatabase
    {
        protected InMemoryData()
        {
            //Create User 1
            User user = new User();
            user.email = "bruno.deprez@gmail.com";
            user.password = "mypassword";
            Session.Save(user);
            
            //Create User 2
            user = new User();
            user.email = "nicolas.raynaud@gmail.com";
            user.password = "mypassword";
            Session.Save(user);
            
            //Create Ad 1
            Ad ad = new Ad();
            ad.title = "Le bateau en Alu a ma tontine";
            ad.body = "Awa j'vend la plate a ma tontine pour allez baigner a la passe de Dumbea";

            //Add User 1 as creator of Ad1, automaticall setting the created by for the Ad
            user = new Repository<User>(Session).FilterBy(x => x.email.Equals("bruno.deprez@gmail.com")).First();
            user.AddAd(ad);
            Session.SaveOrUpdate(user);
            Session.Save(ad);

            //Flush session
            Session.Flush();
        }
    }
}
