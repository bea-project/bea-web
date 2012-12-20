using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain.Location;

namespace Bea.Core.Dal
{
    public interface ILocationRepository
    {
        /// Get the City from the label
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The user with the email</returns>
        City GetCityFromLabel(string label);

        /// Get All provinces
        /// Required to create the AdCreateModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IList<Province> GetAllProvinces();
    }
}
