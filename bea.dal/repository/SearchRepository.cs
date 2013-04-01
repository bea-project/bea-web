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

        public IList<SearchAdCache> AdvancedSearchAds<T>(AdSearchParameters parameters) where T : BaseAd
        {
            ICriteria query = createRootCriteria(parameters.AndSearchStrings, parameters.CityId, parameters.CategoryIds);

            DetachedCriteria crit = creatDetachedCriteria<T>(parameters);

            query.Add(Subqueries.PropertyIn("AdId", crit));

            return query.List<SearchAdCache>();
        }

        private DetachedCriteria creatDetachedCriteria<T>(AdSearchParameters parameters) where T : BaseAd
        {
            DetachedCriteria crit = DetachedCriteria.For<T>();

            if (parameters.MinPrice.HasValue)
                crit.Add(Restrictions.Ge("Price", parameters.MinPrice.Value));
            
            if (parameters.MaxPrice.HasValue)
                crit.Add(Restrictions.Le("Price", parameters.MaxPrice.Value));

            if (parameters.MinKm.HasValue)
                crit.Add(Restrictions.Ge("Kilometers", parameters.MinKm.Value));

            if (parameters.MaxKm.HasValue)
                crit.Add(Restrictions.Le("Kilometers", parameters.MaxKm.Value));

            if (parameters.MinYear.HasValue)
                crit.Add(Restrictions.Ge("Year", parameters.MinYear.Value));

            if (parameters.MaxYear.HasValue)
                crit.Add(Restrictions.Le("Year", parameters.MaxYear.Value));

            if (parameters.BrandId.HasValue)
                crit.Add(Restrictions.Eq("Brand.Id", parameters.BrandId));

            if (parameters.FueldId.HasValue)
                crit.Add(Restrictions.Eq("Fuel.Id", parameters.FueldId));

            if (parameters.IsAuto.HasValue)
                crit.Add(Restrictions.Eq("IsAutomatic", parameters.IsAuto));

            if (parameters.MinEngineSize.HasValue)
                crit.Add(Restrictions.Ge("EngineSize", parameters.MinEngineSize));

            if (parameters.MaxEngineSize.HasValue)
                crit.Add(Restrictions.Le("EngineSize", parameters.MaxEngineSize));

            if (parameters.MinNbRooms.HasValue)
                crit.Add(Restrictions.Ge("RoomsNumber", parameters.MinNbRooms));

            if (parameters.MaxNbRooms.HasValue)
                crit.Add(Restrictions.Le("RoomsNumber", parameters.MaxNbRooms));

            if (parameters.DistrictId.HasValue)
                crit.Add(Restrictions.Eq("District.Id", parameters.DistrictId));

            if (parameters.RealEstateTypeId.HasValue)
                crit.Add(Restrictions.Eq("Type.Id", parameters.RealEstateTypeId));

            if (parameters.IsFurnished.HasValue)
                crit.Add(Restrictions.Eq("IsFurnished", parameters.IsFurnished));

            if (parameters.MinSurfaceArea.HasValue)
                crit.Add(Restrictions.Ge("SurfaceArea", parameters.MinSurfaceArea));

            if (parameters.MaxSurfaceArea.HasValue)
                crit.Add(Restrictions.Le("SurfaceArea", parameters.MaxSurfaceArea));

            crit.SetProjection(Projections.Property("Id"));

            return crit;
        }
    }
}
