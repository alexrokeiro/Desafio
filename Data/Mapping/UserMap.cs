using FluentNHibernate.Mapping;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id, "id").GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Birth, "Birth");
            Map(x => x.Email, "Email");
            Map(x => x.Genre, "Genre");
            References(x => x.Role, "IdRole");
            HasMany(x => x.Dependents).KeyColumn("IdUser").Cascade.All();
            Table("[dbo].[User]");
        }
    }
}
