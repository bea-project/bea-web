using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace Bea.Test.TestHelper
{
    [TestClass]
    public class DataAccessTestBase
    {
        [TestInitialize()]
        public void MyTestInitialize()
        {
            CurrentSessionContext.Bind(NhibernateHelper.SessionFactory.OpenSession());
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            CurrentSessionContext.Unbind(NhibernateHelper.SessionFactory);
        }
    }
}
