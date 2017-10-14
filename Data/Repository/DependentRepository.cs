using Data.Nhibernate;
using Domain.Model.Models;
using NHibernate;

namespace Data.Repository
{
    public class DependentRepository
    {
        public void Save(Dependent dependent)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Save(dependent);
                    tran.Commit();
                }
            }
        }

        public void Delete(Dependent dependent)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Delete(dependent);
                    tran.Commit();
                }
            }
        }

        public Dependent GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Dependent>(id);
            }
        }
    }
}
