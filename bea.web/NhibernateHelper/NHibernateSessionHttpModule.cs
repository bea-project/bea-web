using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Context;

namespace Bea.Web.NhibernateHelper
{
    public class NHibernateSessionHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
            context.EndRequest += context_EndRequest;
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            ISession session = ManagedWebSessionContext.Unbind(
                HttpContext.Current, DependencyResolver.Current.GetService<ISessionFactory>());

            if (session != null)
            {
                session.Close();
                session.Dispose();
            }
        }

        void context_BeginRequest(object sender, EventArgs e)
        {
            ManagedWebSessionContext.Bind(HttpContext.Current, DependencyResolver.Current.GetService<ISessionFactory>().OpenSession());
        }

        public void Dispose()
        {
           
        }

    }
}