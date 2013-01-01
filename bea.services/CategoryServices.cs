using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Core.Dal;
using Bea.Domain.Category;

namespace Bea.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Get all the Category Groups with the categories
        /// </summary>
        /// <returns>A list of Category Groups</returns>
        public List<CategoryElement> GetAllCategoryGroupsWithCategories()
        {
            return _categoryRepository.GetAllCategoryGroupsWithCategories();
        }

        /// <summary>
        /// Get all the Category by Id
        /// </summary>
        /// <returns>A Category</returns>
        public CategoryElement GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }
    }
}
