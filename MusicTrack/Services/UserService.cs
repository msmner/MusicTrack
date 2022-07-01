using MusicTrack.Infrastructure.Repositories;
using MusicTrack.Models;

namespace MusicTrack.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
        public async Task<User> AddAsync(User user)
        {
            _userRepository.Add(user);
            await _userRepository.SaveChangesAsync();

            return user;
        }
    }
}
