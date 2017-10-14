using Domain.Model.Models;
using FluentNHibernate.Mapping;

namespace Data.Mapping
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id, "id").GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Birth, "Birth");
            Map(x => x.Email, "Email");
            Map(x => x.Genre, "Genre");
            References(x => x.Role, "IdRole");
            HasMany(x => x.Dependents).KeyColumn("IdEmployee").Cascade.All();
            Table("Employee");
        }
    }
}
