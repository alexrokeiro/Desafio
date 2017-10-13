using FluentNHibernate.Mapping;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class DependentMap : ClassMap<Dependent>
    {
        public DependentMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            References(x => x.User) // you'll need 'Property' in your class definition too
            .Column("Id_User")
            .Cascade.All();
        }
    }
}
