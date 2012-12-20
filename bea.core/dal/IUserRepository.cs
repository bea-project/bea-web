using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;

namespace Bea.Core.Dal
{
    public interface IUserRepository
    {
        /// Get the User from the email
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The user with the email</returns>
        User GetUserFromEmail(string email);
    }
}
