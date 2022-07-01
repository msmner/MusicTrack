using MusicTrack.Models;

namespace MusicTrack.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> FindUser(string username, string password);

        User Add(User user);

        Task SaveChangesAsync();

        //    Task<User?> FindByIdAsync(Guid id);
        //    Task<IList<User>> GetAllAsync();
        //    User Remove(User user);
        //    User Update(User user);
    }
}
