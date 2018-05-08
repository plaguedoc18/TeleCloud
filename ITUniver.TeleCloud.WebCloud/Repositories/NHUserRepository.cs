using ITUniver.TeleCalc.Web.Repositories;
using ITUniver.TeleCloud.WebCloud.Models;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCloud.WebCloud.Repositories
{
    public class NHUserRepository : Interfaces.IRepository<User>
    {
        public User Clone(User obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Find(string condition)
        {
            var session = NHibernateHelper.GetCurrentSession();
            var queryUsers = $@"select * from Users{condition}";
            return session.CreateSQLQuery(queryUsers)
            .SetResultTransformer(Transformers.AliasToBean(typeof(User)))
            .List<User>().ToList();
        }
        public IEnumerable<User> Find()
        {
            return NHibernateHelper.GetCurrentSession().CreateCriteria<User>().List<User>();
        }

        public User Load(int id)
        {
            return NHibernateHelper.GetCurrentSession().Load<User>(id);
        }

        public bool Save(User obj)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    session.Save(obj);
                    tx.Commit();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}