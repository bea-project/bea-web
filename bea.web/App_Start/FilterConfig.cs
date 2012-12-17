using System.Web;
using System.Web.Mvc;
using Bea.Web.NhibernateHelper;
using System.Web.Http.Filters;


namespace Bea.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new NHibernateSessionActionFilterAttribute());
        }

        public static void RegisterWebApiGlobalFilters(HttpFilterCollection filters)
        {
            filters.Add(new NHibernateSessionWebApiActionFilterAttribute());
        }
    }
}