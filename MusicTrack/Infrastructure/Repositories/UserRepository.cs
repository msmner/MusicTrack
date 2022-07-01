using Microsoft.EntityFrameworkCore;
using MusicTrack.Exceptions.AlreadyExists;
using MusicTrack.Models;

namespace MusicTrack.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MusicTrackDbContext _context;
        public UserRepository(MusicTrackDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Add(User user)
        {
            var checkUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (checkUser != null)
            {
                throw new UsernameAlreadyExistsException();
            }

            user.Role = "user";
            user.CreatedOn = DateTime.Now;

            return _context.Users.Add(user).Entity;
        }

        public async Task<User?> FindUser(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
