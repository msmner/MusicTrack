using Microsoft.EntityFrameworkCore;
using MusicTrack.Models;

namespace MusicTrack.Infrastructure.Repositories
{
    public class PlayListRepository : IPlaylistRepository
    {
        private readonly MusicTrackDbContext _context;

        public PlayListRepository(MusicTrackDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PlayList?> FindByIdAsync(Guid id)
        {
            return await _context
                .PlayLists
                .Where(x => x.Id == id)
                .Include(x=> x.Tracks)
                .SingleOrDefaultAsync();
        }

        public PlayList Add(PlayList playlist)
        {
            return _context.PlayLists.Add(playlist).Entity;
        }

        public PlayList Update(PlayList playlist)
        {
            return _context.PlayLists.Update(playlist).Entity;
        }

        public PlayList Remove(PlayList playlist)
        {
            return _context.PlayLists.Remove(playlist).Entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IList<PlayList>> GetAllAsync()
        {
            return await _context
                .PlayLists
                .ToListAsync();
        }

        public async Task<IList<PlayList>> GetPlaylistsByAlbumId(Guid albumId)
        {
            return await _context
                .PlayLists
                .Where(x => x.Tracks.Any(y => y.AlbumId == albumId))
                .ToListAsync();
        }

        public async Task<IList<PlayList>> GetPlaylistByTrackId(Guid trackId)
        {
            return await _context
                .PlayLists
                .Where(x => x.Tracks.Any(y => y.Id == trackId))
                .ToListAsync();
        }

        public async Task<List<PlayList>> GetPlaylistsByName(string? name)
        {
            return await _context
                .PlayLists
                .Where(x => x.Name.Contains(name!))
                .ToListAsync();
        }

        public async Task<PlayList?> GetPlaylistByIdAsync(Guid id)
        {
            return await _context
                .PlayLists
                .Include(x => x.Tracks)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PlayList?> GetPlaylistByName(string name)
        {
            return await _context
                .PlayLists
                .SingleOrDefaultAsync(x => x.Name == name);
        }
    }
}
