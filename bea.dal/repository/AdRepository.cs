﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Dal;
using Bea.Domain;
using Bea.Domain.Location;
using NHibernate;
using NHibernate.Linq;

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
            var query = (from u in _sessionFactory.GetCurrentSession().Query<User>()
                         select new { User = u, Count = u.Ads.Count });
            IDictionary<User, int> result = query.ToList().ToDictionary(x => x.User, x => x.Count);
            return result;
        }

        public List<Ad> GetAllAds()
        {
            return _sessionFactory.GetCurrentSession().Query<Ad>().Fetch(x=>x.CreatedBy).Fetch(x=>x.City).ToList();
        }

        public Ad GetAdById(long adId)
        {
            
            Ad ad = _sessionFactory.GetCurrentSession().Query<Ad>().Fetch(x => x.CreatedBy).Fetch(x => x.City).Where(x => x.Id == adId).FirstOrDefault();
            return ad;
        }

        public IList<Ad> SearchAdsByTitle(string searchString)
        {
            IQueryable<Ad> query = _sessionFactory.GetCurrentSession().Query<Ad>();

            if (!String.IsNullOrEmpty(searchString))
                query = query.Where(a => a.Title.Contains(searchString));

            return query.OrderByDescending(a => a.CreationDate).ToList();
        }

        public void DeleteAdById(long adId)
        {
            Ad ad = GetAdById(adId);
            if (ad != null)
            {
                _sessionFactory.GetCurrentSession().Delete(ad);
                _sessionFactory.GetCurrentSession().Flush();
            }
        }

        public void AddAd(Ad ad)
        {
            _sessionFactory.GetCurrentSession().Save(ad);
        }
    }
}
