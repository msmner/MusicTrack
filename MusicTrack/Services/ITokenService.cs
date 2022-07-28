using MusicTrack.Models;

namespace MusicTrack.Services
{
   public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
