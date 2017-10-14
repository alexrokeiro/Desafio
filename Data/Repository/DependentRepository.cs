using Data.Nhibernate;
using Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
