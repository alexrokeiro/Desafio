using Data.Nhibernate;
using Domain.Model.Models;
using NHibernate;

namespace Data.Repository
{
    public class RoleRepository
    {
        public Role GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<Role>(id);
            }

        }
    }
}
