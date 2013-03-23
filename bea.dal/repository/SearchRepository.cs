using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain.Ads;
using Bea.Domain.Search;
using NHibernate;
using NHibernate.Criterion;

namespace Bea.Dal.Repository
{
    public class SearchRepository : ISearchRepository
    {
        protected ISessionFactory _sessionFactory;

        public SearchRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// Method creating criteria for querying over SearchAdCache table with common restrictions
        /// </summary>
        /// <param name="andSearchStrings">The ads search strings</param>
        /// <param name="cityId">The city where to look for</param>
        /// <param name="categoryIds">The categories included in the search</param>
        /// <returns>A criteria initialized with restrictions</returns>
        private ICriteria createRootCriteria(string[] andSearchStrings, int? cityId, int[] categoryIds)
        {
            ICriteria query = _sessionFactory.GetCurrentSession().CreateCriteria<SearchAdCache>();

            // Add AND clause between search strings
            if (andSearchStrings != null && andSearchStrings.Length != 0)
            {
                Conjunction andQuery = Restrictions.Conjunction();

                foreach (String andString in andSearchStrings)
                {
                    Disjunction subAndQuery = Restrictions.Disjunction();
                    subAndQuery.Add(Restrictions.Like("Title", andString, MatchMode.Anywhere));
                    subAndQuery.Add(Restrictions.Like("Body", andString, MatchMode.Anywhere));
                    andQuery.Add(subAndQuery);
                }
                query.Add(andQuery);
            }

            // Add AND clause to the location (either city or province)
            if (cityId.HasValue)
                query.Add(Restrictions.Eq("City.Id", cityId.Value));

            // Add AND clause to the category
            if (categoryIds != null && categoryIds.Length > 0)
                query.Add(Restrictions.In("Category.Id", categoryIds));

            // Order results by creation date descending (most recent first)
            query.AddOrder(Order.Desc("CreationDate"));

            return query;
        }

        /// <summary>
        /// Method creating an advanced DetachedCriteria for any subtype of VehicleAd, based on the parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="minKm"></param>
        /// <param name="maxKm"></param>
        /// <param name="minYear"></param>
        /// <param name="maxYear"></param>
        /// <param name="brandId"></param>
        /// <param name="fueldId"></param>
        /// <param name="isAuto"></param>
        /// <param name="engineSizeMin"></param>
        /// <param name="engineSizeMax"></param>
        /// <returns>A DetachedCriteria initialized with restrictions</returns>
        private DetachedCriteria createVehicleCriteria<T>(int? minKm, int? maxKm, int? minYear, int? maxYear, int? brandId, int? fueldId, Boolean? isAuto, int? engineSizeMin, int? engineSizeMax) where T : VehicleAd
        {
            DetachedCriteria crit = DetachedCriteria.For<T>();

            if (minKm.HasValue)
                crit.Add(Restrictions.Gt("Kilometers", minKm.Value));

            if (maxKm.HasValue)
                crit.Add(Restrictions.Lt("Kilometers", maxKm.Value));

            if (minYear.HasValue)
                crit.Add(Restrictions.Gt("Year", minYear.Value));

            if (maxYear.HasValue)
                crit.Add(Restrictions.Lt("Year", maxYear.Value));

            if (brandId.HasValue)
                crit.Add(Restrictions.Eq("Brand.Id", brandId));

            if (fueldId.HasValue)
                crit.Add(Restrictions.Eq("Fuel.Id", fueldId));

            if (isAuto.HasValue)
                crit.Add(Restrictions.Eq("IsAutomatic", isAuto));

            if (engineSizeMin.HasValue)
                crit.Add(Restrictions.Gt("EngineSize", engineSizeMin));

            if (engineSizeMax.HasValue)
                crit.Add(Restrictions.Lt("EngineSize", engineSizeMax));

            crit.SetProjection(Projections.Property("Id"));

            return crit;
        }

        /// <summary>
        /// Searches through all subtypes of VehicleAd, based on given properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="andSearchStrings"></param>
        /// <param name="cityId"></param>
        /// <param name="categoryIds"></param>
        /// <param name="minKm"></param>
        /// <param name="maxKm"></param>
        /// <param name="minYear"></param>
        /// <param name="maxYear"></param>
        /// <param name="brandId"></param>
        /// <param name="fueldId"></param>
        /// <param name="isAuto"></param>
        /// <param name="engineSizeMin"></param>
        /// <param name="engineSizeMax"></param>
        /// <returns></returns>
        public IList<SearchAdCache> SearchVehicleAds<T>(string[] andSearchStrings, int? cityId, int[] categoryIds, int? minKm, int? maxKm, int? minYear, int? maxYear, int? brandId, int? fueldId, Boolean? isAuto, int? engineSizeMin, int? engineSizeMax) where T : VehicleAd
        {
            ICriteria query = createRootCriteria(andSearchStrings, cityId, categoryIds);

            DetachedCriteria crit = createVehicleCriteria<T>(minKm, maxKm, minYear, maxYear, brandId, fueldId, isAuto, engineSizeMin, engineSizeMax);

            query.Add(Subqueries.PropertyIn("AdId", crit));

            return query.List<SearchAdCache>();
        }

        /// <summary>
        /// Searches through all SearchAdCache based on given properties
        /// </summary>
        /// <param name="andSearchStrings"></param>
        /// <param name="cityId"></param>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public IList<SearchAdCache> SearchAds(string[] andSearchStrings = null, int? cityId = null, int[] categoryIds = null)
        {
            ICriteria query = createRootCriteria(andSearchStrings, cityId, categoryIds);

            return query.List<SearchAdCache>();
        }
    }
}
