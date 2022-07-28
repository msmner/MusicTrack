using MusicTrack.Dtos;
using MusicTrack.Models;

namespace MusicTrack.Services
{
    public interface IUserService
    {
        Task<User> Register(RegisterDto userDto);

        Task<User> Login(LoginDto loginDto);
    }
}
