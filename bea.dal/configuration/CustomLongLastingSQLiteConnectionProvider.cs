using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NHibernate.Connection;

namespace Bea.Dal.configuration
{
    /// <summary>
    /// This class should only be used for testing purposes
    /// The aim of this overriden connection provider is to ensure the 
    /// db schema doesnt disappear with the session when created through 
    /// nhibernate configuration and the database is only in memory
    /// </summary>
    public class CustomLongLastingSQLiteConnectionProvider : DriverConnectionProvider
    {
        private static IDbConnection _connection;

        public override IDbConnection GetConnection()
        {
            if (_connection == null)
                _connection = base.GetConnection();
            return _connection;
        }

        /// <summary>
        /// Overrides the close connection in order to prevent 
        /// the DB schema from being wiped along with the session
        /// </summary>
        /// <param name="conn"></param>
        public override void CloseConnection(IDbConnection conn)
        {
        }

        /// <summary>
        /// Destroys the connection that is kept open in order to 
        /// keep the in-memory database alive.  Destroying
        /// the connection will destroy all of the data stored in 
        /// the mock database.  Call this method when the
        /// test is complete.
        /// </summary>
        public static void ExplicitlyDestroyConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}
