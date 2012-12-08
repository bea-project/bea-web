using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using bea.core.dal;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Criterion;

namespace bea_dal.repository
{
    public class Repository : IRepository
    {
        protected ISessionFactory _sessionFactory;

        public Repository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        #region IRepository<T> Members

        public T Load<T>(object id)
        {
            return _sessionFactory.GetCurrentSession().Load<T>(id);
        }

        public T Get<T>(object id)
        {
            return _sessionFactory.GetCurrentSession().Get<T>(id);
        }
        public IList<T> GetAll<T>()
        {
            return _sessionFactory.GetCurrentSession().CreateCriteria(typeof(T)).List<T>();
        }
        public void Update<T>(T obj)
        {
            _sessionFactory.GetCurrentSession().Update(obj);
        }

        public void Save<T>(T obj)
        {
            _sessionFactory.GetCurrentSession().Save(obj);
        }

        public void Delete<T>(T obj)
        {
            _sessionFactory.GetCurrentSession().Delete(obj);
        }

        public void Flush()
        {
            _sessionFactory.GetCurrentSession().Flush();
        }

        public void SaveOrUpdate<T>(T obj)
        {
            _sessionFactory.GetCurrentSession().SaveOrUpdate(obj);
        }

        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> matchingCriteria)
        {
            return _sessionFactory.GetCurrentSession().Query<T>().Where(matchingCriteria);
        }

        public int CountAll<T>()
        {
            return _sessionFactory.GetCurrentSession().CreateCriteria(typeof(T)).SetProjection(Projections.Count("Id")).UniqueResult<int>();
        }

        public void Evict<T>(T obj)
        {
            _sessionFactory.GetCurrentSession().Evict(obj);
        }

        public void Refresh<T>(T obj)
        {
            _sessionFactory.GetCurrentSession().Refresh(obj);
        }

        public void Clear()
        {
            _sessionFactory.GetCurrentSession().Clear();
        }

        #endregion
    }
}
