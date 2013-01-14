using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Core.Dal;
using Bea.Domain.Categories;

namespace Bea.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRepository _repository;

        public CategoryServices(ICategoryRepository categoryRepository, IRepository repository)
        {
            _categoryRepository = categoryRepository;
            _repository = repository;
        }

        /// <summary>
        /// Get all the Category Groups with the categories
        /// </summary>
        /// <returns>A list of Category Groups</returns>
        //public List<Category> GetAllCategoryGroupsWithCategories()
        //{
        //    return _categoryRepository.GetAllCategoryGroupsWithCategories();
        //}

        /// <summary>
        /// Get all the Category by Id
        /// </summary>
        /// <returns>A Category</returns>
        public Category GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public List<Category> GetAllCategories()
        {
            return _repository.GetAll<Category>().ToList();
        }
    }
}
