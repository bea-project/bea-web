using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;
using Bea.Models.References;

namespace Bea.Core.Services
{
    public interface ICategoryServices
    {
        /// <summary>
        /// Get all the Category Groups with the categories
        /// </summary>
        /// <returns>A list of Category Groups</returns>
        //List<Category> GetAllCategoryGroupsWithCategories();

        /// <summary>
        /// Get all the Category by Id
        /// </summary>
        /// <returns>A Category</returns>
        Category GetCategoryById(int categoryId);

        /// <summary>
        /// Returns all categories
        /// </summary>
        /// <returns></returns>
        IList<Category> GetAllCategories();

        /// <summary>
        /// Lists all the categories adding groups as non selectable
        /// </summary>
        /// <returns>The list of categories with groups</returns>
        IList<CategoryItemModel> GetAllCategoriesAndGroups();

        /// <summary>
        /// List all children of a category
        /// </summary>
        /// <returns>The list of children labels</returns>
        //IList<String> GetCategoryChildrenLabelFromParentLabel(String parentLabel);

        /// <summary>
        /// List all children of a category
        /// </summary>
        /// <returns>The list of children labels</returns>
        IList<String> GetCategoryChildrenLabelFromParentId(int parentLabel);
    }
}
