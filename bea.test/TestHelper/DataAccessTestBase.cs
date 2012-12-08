using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace bea.test.TestHelper
{
    [TestClass]
    public class DataAccessTestBase
    {
        [TestInitialize()]
        public void MyTestInitialize()
        {
            CurrentSessionContext.Bind(NhibernateHelper.SessionFactory.OpenSession());
            new SchemaExport(NhibernateHelper.Configuration).Execute(true, true, false, NhibernateHelper.SessionFactory.GetCurrentSession().Connection, null);
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            CurrentSessionContext.Unbind(NhibernateHelper.SessionFactory);
        }
    }
}
