using TodoApi2.src.core.Domain.Interfaces;
using TodoApi2.src.core.Domain.Services;

namespace TodoApi2.src.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //         public async Task<User> GetUserById(int id)
        // {
        // return await _userRepository.GetUserById(id);
        // }

    }
}