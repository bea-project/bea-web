using System.Web;
using System.Web.Mvc;
using Bea.Web.NhibernateHelper;

namespace Bea.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new NHibernateSessionActionFilterAttribute());
        }
    }
}