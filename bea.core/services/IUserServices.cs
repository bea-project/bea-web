using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Domain;

namespace Bea.Core.Services
{
    public interface IUserServices
    {
        User GetUserFromEmail(string email);
    }
}
