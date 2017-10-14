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
                  .ConnectionString(@"Data Source=CHAMALEON\SQLDEV;initial catalog=cd_meiospagamento;persist security info=True;user id=sa;password=dbh8JVsQW4sJK4a4btvN;")
                              .ShowSql()
                )
               .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<User>()
                              .AddFromAssemblyOf<Role>()
                              .AddFromAssemblyOf<Dependent>())
               .Mappings(m =>
                          m.FluentMappings.Add<UserMap>()
                          .Add<RoleMap>()
                          .Add<DependentMap>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(false, false))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
