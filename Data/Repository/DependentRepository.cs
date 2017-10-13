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
        public void Salvar(Dependent dependent)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                session.Save(dependent);
            }
        }
    }
}
