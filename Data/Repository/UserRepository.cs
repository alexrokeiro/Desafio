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
    public class UserRepository
    {

        public void Salvar(User usuario)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                session.Save(usuario);
            }
        }

        public void Atualizar(User usuario)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    //session.SaveOrUpdate(usuario.Dependents);
                    session.Update(usuario);
                    tran.Commit();
                }
            }
        }

        public void Deletar(User usuario)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Delete(usuario);
                    tran.Commit();
                }
            }
        }

        public List<User> Listar(string name, bool like)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<User>().List().ToList();
            }
        }

        public User ObterPorId(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var user = session.Get<User>(id);
                NHibernateUtil.Initialize(user.Role);
                NHibernateUtil.Initialize(user.Dependents);
                return user;
            }

        }

    }
}
