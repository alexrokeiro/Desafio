using Domain.Model.Models;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    public class DependentMap : ClassMap<Dependent>
    {
        public DependentMap()
        {
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.IdUser, "IdEmployee");
            Table("Dependent");
        }
    }
}
