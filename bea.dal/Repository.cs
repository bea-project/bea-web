using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace bea.dal
{
    /// <typeparam name="T">Any entity that can be mapped to a Fluent NHibernate entity.</typeparam>
    public class Repository<T> : IDisposable where T : class
    {
        private bool _disposed; // For IDisposable
        private readonly ISession Session;

        public Repository(ISession session)
        {
            this.Session = session;
        }

        public T FindBy(object id)
        {
            try
            {
                return this.Session.Get<T>(id);
            }
            catch (ObjectNotFoundException)
            {
                return null;
            }
        }

        public bool Add(T entity)
        {
            this.Session.SaveOrUpdate(entity);
            return true;
        }

        public bool Add(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                this.Session.SaveOrUpdate(item);
            }

            return true;
        }

        /// <summary>
        /// Updates an entity.
        /// It can handle merging a disconnected object, but that _doesn't_
        /// happen by default because merges can be dangerous.
        /// </summary>
        /// <param name="entity">Entity to be updated</param>
        /// <param name="autoMerge">true to merge automatically, false to not merge</param>
        /// <returns>true if update succeeded, false otherwise</returns>
        public bool Update(T entity, bool autoMerge = false)
        {
            bool requiresMerge = RequiresMerge(entity);

            if (requiresMerge && !autoMerge)
            {
                return false;
            }

            if (requiresMerge)
            {
                this.Session.Merge<T>(entity);
            }
            else
            {
                this.Session.SaveOrUpdate(entity);
            }

            return true;
        }

        public bool RequiresMerge(T entity)
        {
            return !this.Session.Contains(entity);
        }

        public bool DeleteById(object id)
        {
            var item = this.FindBy(id);
            return this.Delete(item);
        }

        public bool Delete(T entity)
        {
            if (null == entity)
            {
                return false;
            }
            this.Session.Delete(entity);
            return true;
        }

        public bool Delete(IEnumerable<T> entities)
        {
            bool success = true;

            foreach (T item in entities)
            {
                if (null == item)
                {
                    success = false;
                    break;
                }

                this.Session.Delete(item);
            }

            return success;
        }

        public IQueryable<T> All()
        {
            return this.Session.Query<T>();
        }

        
        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return this.FilterBy(expression).SingleOrDefault();
        }

        
        public IQueryable<T> FilterBy(Expression<Func<T, bool>> expression)
        {
            return this.All().Where(expression).AsQueryable();
        }

        ~Repository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called
            if (!this._disposed)
            {
                // If disposing is true, dispose all managed and unmanaged resources
                if (disposing)
                {
                    lock (this.Session)
                    {
                        if (this.Session.IsOpen)
                        {
                            this.Session.Close();
                        }
                    }
                }

                this._disposed = true;
            }
        }
    }
}
