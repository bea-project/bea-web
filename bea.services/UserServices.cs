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
        private readonly IRepository _repository;

        public UserServices(IUserRepository userRepository, IRepository repository)
        {
            _userRepository = userRepository;
            _repository = repository;
        }
        
        public User GetUserFromEmail(string email)
        {
            return _userRepository.GetUserFromEmail(email);
        }

        public User GetUserFromId(int userId)
        {
            return _repository.Get<User>(userId);
        }
    }
}
