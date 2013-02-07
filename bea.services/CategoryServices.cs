using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Core.Dal;
using Bea.Domain.Categories;
using Bea.Models.References;

namespace Bea.Services
{
    public class CategoryServices : ICategoryServices
    {
        public static readonly int ID_MULTIPLIER = 1000000;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IRepository _repository;

        public CategoryServices(ICategoryRepository categoryRepository, IRepository repository)
        {
            _categoryRepository = categoryRepository;
            _repository = repository;
        }

        /// <summary>
        /// Get all the Category by Id
        /// </summary>
        /// <returns>A Category</returns>
        public Category GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public IList<Category> GetAllCategories()
        {
            return _repository.GetAll<Category>();
        }

        public IList<CategoryItemModel> GetAllCategoriesAndGroups()
        {
            IList<CategoryItemModel> result = new List<CategoryItemModel>();

            foreach (CategoryGroup group in _repository.GetAll<CategoryGroup>())
            {
                result.Add(new CategoryItemModel
                    {
                        Id = ID_MULTIPLIER + group.Id,
                        Label = group.Label.ToUpper(),
                        IsGroup = true
                    });

                group.Categories.ToList().ForEach(x => result.Add(new CategoryItemModel { Id = x.Id, Label = x.Label, IsGroup = false }));
            }

            return result;
        }
    }
}
