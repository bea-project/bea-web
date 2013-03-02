using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Search.WaterSport
{
    public class SailingBoatAdSearchModel : AdSearchModel
    {
        public int? SelectedTypeId { get; set; }
        public int? SelectedHullTypeId { get; set; }
        public int? MinYearSelectedId { get; set; }
        public int? MaxYearSelectedId { get; set; }
        public int? MinLengthSelectedId { get; set; }
        public int? MaxLengthSelectedId { get; set; }
    }
}
