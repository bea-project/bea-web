using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Location;

namespace Bea.Core.Dal
{
    public interface ILocationRepository
    {
        /// Get the City from the label Url part
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        City GetCityFromLabelUrlPart(string labelUrlPart);

        /// Get All provinces
        /// Required to create the AdCreateModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<Province> GetAllProvinces();

        /// Get All Cities From a province
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<City> GetCitiesFromProvince(int provinceId);
    }
}
