using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Core.dal
{
    public interface IRepository
    {
        T Load<T>(object id);
        T Get<T>(object id);
        IList<T> GetAll<T>();
        void Save<T>(T obj);
        void Update<T>(T obj);
        void Delete<T>(T obj);
        void Flush();
        int CountAll<T>();
        void Evict<T>(T obj);
        void Refresh<T>(T obj);
        void Clear();
        void SaveOrUpdate<T>(T obj);
    }
}
