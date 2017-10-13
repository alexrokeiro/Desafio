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
    public class RoleRepository
    {
        public Role ObterPorId(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Role>(id);
            }

        }
    }
}
