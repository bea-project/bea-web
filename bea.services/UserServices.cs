using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bea.Core.Services;
using Bea.Domain;
using Bea.Core.Dal;

namespace Bea.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public User GetUserFromEmail(string email)
        {
            return _userRepository.GetUserFromEmail(email);
        }
    }
}
