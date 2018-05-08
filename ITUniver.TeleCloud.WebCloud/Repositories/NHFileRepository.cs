using ITUniver.TeleCalc.Web.Repositories;
using ITUniver.TeleCloud.WebCloud.Interfaces;
using ITUniver.TeleCloud.WebCloud.Models;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCloud.WebCloud.Repositories
{
    public class NHFileRepository : IRepository<FileItemModel>
    {
        public FileItemModel Clone(FileItemModel obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FileItemModel> Find(string condition)
        {
            var session = NHibernateHelper.GetCurrentSession();
            var queryFiles = $@"select * from TFile {condition}";
            return session.CreateSQLQuery(queryFiles)
            .SetResultTransformer(Transformers.AliasToBean(typeof(FileItemModel)))
            .List<FileItemModel>().ToList();
        }

        public IEnumerable<FileItemModel> Find()
        {
            return NHibernateHelper.GetCurrentSession().CreateCriteria<FileItemModel>().List<FileItemModel>();
        }
        public FileItemModel Load(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(FileItemModel obj)
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            using (ITransaction tx = session.BeginTransaction())
            {
                session.SaveOrUpdate(obj);
                tx.Commit();
            }

            return true;
        }
    }
}