using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Bea.Core.dal;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Criterion;

namespace Bea.Dal.repository
{
    public class Repository : IRepository
    {
        protected ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        #region IRepository<T> Members

        public T Load<T>(object id)
        {
            return _session.Load<T>(id);
        }

        public T Get<T>(object id)
        {
            return _session.Get<T>(id);
        }
        public IList<T> GetAll<T>()
        {
            return _session.CreateCriteria(typeof(T)).List<T>();
        }
        public void Update<T>(T obj)
        {
            _session.Update(obj);
        }

        public void Save<T>(T obj)
        {
            _session.Save(obj);
        }

        public void Delete<T>(T obj)
        {
            _session.Delete(obj);
        }

        public void Flush()
        {
            _session.Flush();
        }

        public void SaveOrUpdate<T>(T obj)
        {
            _session.SaveOrUpdate(obj);
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> matchingCriteria)
        {
            return _session.Query<T>().Where(matchingCriteria);
        }

        public int CountAll<T>()
        {
            return _session.CreateCriteria(typeof(T)).SetProjection(Projections.Count("Id")).UniqueResult<int>();
        }

        public void Evict<T>(T obj)
        {
            _session.Evict(obj);
        }

        public void Refresh<T>(T obj)
        {
            _session.Refresh(obj);
        }

        public void Clear()
        {
            _session.Clear();
        }

        #endregion
    }
}
