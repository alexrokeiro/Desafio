using Data.Nhibernate;
using Domain.Model.Models;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository
{
    public class EmployeeRepository
    {

        public void Save(Employee usuario)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                session.Save(usuario);
            }
        }

        public void Update(Employee usuario)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    session.Update(usuario);
                    tran.Commit();
                }
            }
        }

        public void Delete(Employee usuario)
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

        public List<Employee> List(string name, bool like)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var users = new List<Employee>();
                if (like)
                    users = session.CreateCriteria<Employee>().Add(Restrictions.InsensitiveLike("Name", name, MatchMode.Anywhere)).List<Employee>().ToList();
                else
                    users = session.CreateCriteria<Employee>().Add(Restrictions.InsensitiveLike("Name", name, MatchMode.Exact)).List<Employee>().ToList();

                foreach (var user in users)
                {
                    NHibernateUtil.Initialize(user.Role);
                    NHibernateUtil.Initialize(user.Dependents);
                }

                return users;
            }
        }

        public Employee GetById(int id, bool loadRelationships = false)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var user = session.Get<Employee>(id);
                if (user == null)
                    return user;

                if (!loadRelationships)
                    return user;

                NHibernateUtil.Initialize(user.Role);
                NHibernateUtil.Initialize(user.Dependents);
                return user;
            }

        }
    }
}
