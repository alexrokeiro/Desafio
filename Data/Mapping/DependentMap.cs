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
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.IdUser,"Id_User");
            Table("Dependent");
        }
    }
}
