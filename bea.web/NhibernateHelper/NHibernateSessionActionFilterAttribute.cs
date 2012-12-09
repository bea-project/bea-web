using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Context;

namespace Bea.Web.NhibernateHelper
{
    public class NHibernateSessionActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CurrentSessionContext.Bind(DependencyResolver.Current.GetService<ISessionFactory>().OpenSession());
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
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