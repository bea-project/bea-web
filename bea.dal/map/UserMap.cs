using System;
using System.Collections.Generic;
using System.Text;
using Bea.Domain;
using FluentNHibernate.Mapping;

namespace Bea.Dal.Map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.UserId).GeneratedBy.Identity();
            Map(x => x.Email).Not.Nullable();
            Map(x => x.Password).Not.Nullable();
            Map(x => x.CreationDate);
            Map(x => x.Firstname);
            Map(x => x.Lastname);
        }
    }
}
