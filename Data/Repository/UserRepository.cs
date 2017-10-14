using Data.Nhibernate;
using Models;
using NHibernate;
using NHibernate.Criterion;
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
                var users = new List<User>();
                if (like)
                    users = session.CreateCriteria<User>().Add(Restrictions.InsensitiveLike("Name", name, MatchMode.Anywhere)).List<User>().ToList();
                else
                    users = session.CreateCriteria<User>().Add(Restrictions.InsensitiveLike("Name", name, MatchMode.Exact)).List<User>().ToList();

                foreach (var user in users)
                {
                    NHibernateUtil.Initialize(user.Role);
                    NHibernateUtil.Initialize(user.Dependents);
                }

                return users;
            }
        }

        public User ObterPorId(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var user = session.Get<User>(id);
                if (user == null)
                    return user;

                NHibernateUtil.Initialize(user.Role);
                NHibernateUtil.Initialize(user.Dependents);
                return user;
            }

        }

    }
}
