using Domain.Model.Models;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Table("Role");
        }
            
    }
}
