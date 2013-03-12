using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;

namespace Bea.Core.Dal
{
    public interface ICategoryRepository
    {
        Category GetCategoryFromUrlPart(String categoryUrlPart);
        Category GetCategoryFromLabel(string label);
    }
}
