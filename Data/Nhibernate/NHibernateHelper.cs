using Data.Mapping;
using Domain.Model.Models;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace Data.Nhibernate
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                  .ConnectionString(@"Data Source=host;initial catalog=database;persist security info=True;user id=id;password=password;")
                              .ShowSql()
                )
               .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Employee>()
                              .AddFromAssemblyOf<Role>()
                              .AddFromAssemblyOf<Dependent>())
               .Mappings(m =>
                          m.FluentMappings.Add<EmployeeMap>()
                          .Add<RoleMap>()
                          .Add<DependentMap>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(false, false))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
