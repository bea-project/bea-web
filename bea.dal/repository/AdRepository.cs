using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Domain.Admin;
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
                .Fetch(x => x.Category)
                .Where(x => x.CreatedBy.Email.Equals(email) && x.IsActivated == true && x.IsDeleted == false)
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

        public SpamAdRequest GetSpamRequestAd(long adId)
        {
            return _sessionFactory.GetCurrentSession().Query<SpamAdRequest>()
                .Where(x => x.Ad.Id == adId)
                .SingleOrDefault();
        }
    }
}
