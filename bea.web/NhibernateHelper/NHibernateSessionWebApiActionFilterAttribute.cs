using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using NHibernate.Context;
using NHibernate;
using System.Web.Mvc;

namespace Bea.Web.NhibernateHelper
{
    public class NHibernateSessionWebApiActionFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            CurrentSessionContext.Bind(DependencyResolver.Current.GetService<ISessionFactory>().OpenSession());
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            ISession session = CurrentSessionContext.Unbind(DependencyResolver.Current.GetService<ISessionFactory>());

            if (session != null)
            {
                session.Close();
                session.Dispose();
            }

            base.OnActionExecuted(filterContext);
        }
    }
}