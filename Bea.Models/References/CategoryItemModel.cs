using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Categories;

namespace Bea.Models.References
{
    public class CategoryItemModel
    {
        public int Id { get; set; }
        public String Label { get; set; }
        public Boolean IsGroup { get; set; }

    }
}
