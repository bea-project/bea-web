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
        /// Get all the Category by Id
        /// </summary>
        /// <returns>A Category</returns>
        Category GetCategoryById(int categoryId);

        /// <summary>
        /// Lists all the categories adding groups as non selectable
        /// </summary>
        /// <returns>The list of categories with groups</returns>
        IList<CategoryItemModel> GetAllCategoriesAndGroups();

        /// <summary>
        /// Lists all the categories adding groups as non selectable
        /// </summary>
        /// <param name="categoryId">The category of which to return the same group's categories</param>
        /// <returns>The list of categories with their group</returns>
        IList<CategoryItemModel> GetAllCategoriesOfAGroup(int? categoryId);
        IList<CategoryItemModel> GetAllCategoriesOfAGroupFromUrlPart(String categoryUrlPart);

        /// <summary>
        /// List all children of a category
        /// </summary>
        /// <returns>The list of children labels</returns>
        IList<String> GetCategoryChildrenLabelFromParentId(int parentLabel);
    }
}
