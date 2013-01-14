using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;

namespace Bea.Core.Dal
{
    public interface ICategoryRepository
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
    }
}
