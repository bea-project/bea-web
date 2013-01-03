using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;

namespace Bea.Core.Services
{
    public interface ICategoryServices
    {
        /// <summary>
        /// Get all the Category Groups with the categories
        /// </summary>
        /// <returns>A list of Category Groups</returns>
        List<Category> GetAllCategoryGroupsWithCategories();

        /// <summary>
        /// Get all the Category by Id
        /// </summary>
        /// <returns>A Category</returns>
        Category GetCategoryById(int categoryId);
    }
}
