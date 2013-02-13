using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bea.Models.Search.WaterSport
{
    public class AdMotorBoatEngineSearchModel : AdSearchModel
    {
        public int? SelectedTypeId { get; set; }
        public int? MinYearSelectedId { get; set; }
        public int? MaxYearSelectedId { get; set; }
        public int? MinHpSelectedId { get; set; }
        public int? MaxHpSelectedId { get; set; }
    }
}
