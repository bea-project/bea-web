using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain.Categories;
using NHibernate;
using NHibernate.Linq;

namespace Bea.Dal.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public CategoryRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }
        public Category GetCategoryFromUrlPart(string categoryUrlPart)
        {
            return _sessionFactory.GetCurrentSession()
                        .Query<Category>()
                        .Where(c => c.LabelUrlPart.ToLower().Equals(categoryUrlPart.ToLower()))
                        .SingleOrDefault();
        }
        public Category GetCategoryFromLabel(string label)
        {
            return _sessionFactory.GetCurrentSession()
                                  .Query<Category>()
                                  .Where(c => c.Label.ToLower().Equals(label.ToLower()))
                                  .SingleOrDefault();
        }

    }
}
