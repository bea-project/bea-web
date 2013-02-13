using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Search.RealEstate
{
    public class AdRealEstateSearchModel : AdSearchModel
    {
        public int? MinRoomNbSelectedId { get; set; }
        public int? MaxRoomNbSelectedId { get; set; }
        public int? SelectedTypeId { get; set; }
        public int? SelectedDistrictId { get; set; }

    }
}
