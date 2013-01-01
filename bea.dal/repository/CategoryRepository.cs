using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using NHibernate;
using Bea.Domain.Category;
using NHibernate.Linq;

namespace Bea.Dal.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        protected ISessionFactory _sessionFactory;

        public CategoryRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public List<CategoryElement> GetAllCategoryGroupsWithCategories()
        {
            return _sessionFactory.GetCurrentSession().Query<CategoryElement>().ToList();
        }

        public CategoryElement GetCategoryById(int categoryId)
        {
            return _sessionFactory.GetCurrentSession().Query<CategoryElement>().Where(x => x.Id == categoryId).FirstOrDefault();
        }
    }
}
