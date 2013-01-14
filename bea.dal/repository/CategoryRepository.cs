using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using NHibernate;
using Bea.Domain.Categories;
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

        //public List<Category> GetAllCategoryGroupsWithCategories()
        //{
        //    return _sessionFactory.GetCurrentSession().Query<Category>().ToList();
        //}

        public Category GetCategoryById(int categoryId)
        {
            return _sessionFactory.GetCurrentSession().Query<Category>().Where(x => x.Id == categoryId).FirstOrDefault();
        }
    }
}
