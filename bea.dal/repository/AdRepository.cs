using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Domain.Ads;
using Bea.Domain.Location;
using Bea.Domain.Search;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;

namespace Bea.Dal.Repository
{
    public class AdRepository : IAdRepository
    {
        protected ISessionFactory _sessionFactory;

        public AdRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IDictionary<City, int> CountAdsByCity()
        {
            var query = (from c in _sessionFactory.GetCurrentSession().Query<City>().Fetch(x => x.Province)
                         select new { City = c, Count = c.Ads.Count });

            IDictionary<City, int> result = query.ToList().ToDictionary(x => x.City, x => x.Count);

            return result;
        }

        public IDictionary<User, int> CountAdsByUser()
        {
            var query = (from a in _sessionFactory.GetCurrentSession().Query<Ad>()
                         group a by a.CreatedBy into g
                         select new { User = g.Key, Count = g.Count() });

            IDictionary<User, int> result = query.ToList().ToDictionary(x => x.User, x => x.Count);
            return result;
        }

        public IList<BaseAd> GetAllAds()
        {
            return _sessionFactory.GetCurrentSession().Query<BaseAd>().Fetch(x => x.CreatedBy).Fetch(x => x.City).ToList();
        }

        public IList<BaseAd> GetAdsByEmail(String email)
        {
            return _sessionFactory.GetCurrentSession().Query<BaseAd>()
                .Fetch(x => x.CreatedBy)
                .Fetch(x=>x.Category)
                .Where(x=>x.CreatedBy.Email.Equals(email) && x.IsActivated==true && x.IsDeleted==false)
                .ToList();
        }

        public AdTypeEnum GetAdType(long adId)
        {
            AdTypeEnum result = _sessionFactory.GetCurrentSession().Query<BaseAd>()
                .Where(x => x.Id == adId)
                .Select(x => x.Category.Type)
                .SingleOrDefault();

            return result;
        }

        public Boolean CanDeleteAd(long adId)
        {
            return _sessionFactory.GetCurrentSession().Query<BaseAd>()
                .Where(x => x.Id == adId)
                .Select(x => !x.IsDeleted)
                .SingleOrDefault();
        }

        public T GetAdById<T>(long adId) where T : BaseAd
        {
            T ad = _sessionFactory.GetCurrentSession().Query<T>()
                .Fetch(x => x.CreatedBy)
                .Fetch(x => x.City)
                .Where(x => x.Id == adId).FirstOrDefault();

            return ad;
        }

        public IList<Ad> SearchAdsByTitle(string searchString)
        {
            IQueryable<Ad> query = _sessionFactory.GetCurrentSession().Query<Ad>();

            if (!String.IsNullOrEmpty(searchString))
                query = query.Where(a => a.Title.Contains(searchString));

            return query.OrderByDescending(a => a.CreationDate).ToList();
        }

        public IList<SearchAdCache> SearchAds(string[] andSearchStrings = null, string[] orSearchStrings = null, int? provinceId = null, int? cityId = null, int[] categoryIds = null)
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

            // Add OR clause between search strings
            if (orSearchStrings != null && orSearchStrings.Length != 0)
            {
                Disjunction orQuery = Restrictions.Disjunction();
                orSearchStrings.ForEach(s => orQuery.Add(Restrictions.Like("Title", s, MatchMode.Anywhere)));
                orSearchStrings.ForEach(s => orQuery.Add(Restrictions.Like("Body", s, MatchMode.Anywhere)));

                query.Add(orQuery);
            }

            // Add AND clause to the location (either city or province)
            if (cityId.HasValue)
                query.Add(Restrictions.Eq("City.Id", cityId.Value));
            else if (provinceId.HasValue)
                query.Add(Restrictions.Eq("Province.Id", provinceId.Value));

            // Add AND clause to the category
            if (categoryIds != null && categoryIds.Length > 0)
                query.Add(Restrictions.In("Category.Id", categoryIds));

            // Order results by creation date descending (most recent first)
            query.AddOrder(Order.Desc("CreationDate"));

            return query.List<SearchAdCache>();
        }
    }
}
