using MusicTrack.Models;

namespace MusicTrack.Services
{
    public interface IUserService
    {
        Task<User> AddAsync(User user);
    }
}
