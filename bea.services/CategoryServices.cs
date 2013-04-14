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
        private readonly IRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryServices(IRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Get all the Category by Id
        /// </summary>
        /// <returns>A Category</returns>
        public Category GetCategoryById(int categoryId)
        {
            return _repository.Get<Category>(categoryId);
        }

        public IList<CategoryItemModel> GetAllCategoriesAndGroups()
        {
            IList<CategoryItemModel> result = new List<CategoryItemModel>();

            IList<Category> categories = _repository.GetAll<Category>();

            foreach (Category c in categories.Where(x => x.SubCategories.Count != 0))
            {
                result.Add(new CategoryItemModel
                {
                    Id = c.Id,
                    Label = c.Label.ToUpper(),
                    IsGroup = true
                });

                c.SubCategories.ToList().ForEach(x => result.Add(new CategoryItemModel { Id = x.Id, Label = x.Label, IsGroup = false }));
            }

            return result;
        }

        public IList<String> GetCategoryChildrenLabelFromParentId(int parentId)
        {
            Category parentCategory = _repository.Get<Category>(parentId);
            if(parentCategory == null)
                return null;
            if (parentCategory.SubCategories.Count == 0)
                return null;
            List<String> childrenLabels = new List<String>();
            foreach (Category child in parentCategory.SubCategories)
                childrenLabels.Add(child.Label.ToUpper());
            return childrenLabels;
        }


        public IList<CategoryItemModel> GetAllCategoriesOfAGroup(int? categoryId)
        {
            if (!categoryId.HasValue)
                return new List<CategoryItemModel>();

            Category c = _repository.Get<Category>(categoryId);

            return GetAllCategoriesOfAGroup(c);
        }

        public IList<CategoryItemModel> GetAllCategoriesOfAGroupFromUrlPart(string categoryUrlPart)
        {
            if (String.IsNullOrEmpty(categoryUrlPart))
                return null;

            Category c = _categoryRepository.GetCategoryFromUrlPart(categoryUrlPart);

            return GetAllCategoriesOfAGroup(c);
        }

        private IList<CategoryItemModel> GetAllCategoriesOfAGroup(Category c)
        {
            if (c == null)
                return new List<CategoryItemModel>();

            IList<CategoryItemModel> result = new List<CategoryItemModel>();

            if (c.SubCategories.Count == 0)
                c = c.ParentCategory;

            result.Add(new CategoryItemModel
            {
                Id = c.Id,
                Label = c.Label.ToUpper(),
                IsGroup = true
            });

            foreach (Category subC in c.SubCategories)
            {
                result.Add(new CategoryItemModel
                {
                    Id = subC.Id,
                    Label = subC.Label,
                    IsGroup = false
                });
            }

            return result;
        }
    }
}
