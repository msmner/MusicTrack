using Microsoft.EntityFrameworkCore;
using MusicTrack.Models;

namespace MusicTrack.Infrastructure.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly MusicTrackDbContext _context;

        public AlbumRepository(MusicTrackDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Album?> FindByIdAsync(Guid id)
        {
            return await _context
                .Albums
                .Where(x => x.Id == id)
                .Include(x => x.Tracks)
                .SingleOrDefaultAsync();
        }

        public Album Add(Album album)
        {
            return _context.Albums.Add(album).Entity;
        }

        public Album Update(Album album)
        {
            return _context.Albums.Update(album).Entity;
        }

        public Album Remove(Album album)
        {
            return _context.Albums.Remove(album).Entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Album>> GetAllAsync()
        {
            return await _context
                .Albums
                .ToListAsync();
        }

        public async Task<List<Album>> GetAlbumsByName(string? name)
        {
            return await _context
                .Albums
                .Where(x => x.Name.Contains(name!))
                .ToListAsync();
        }

        public async Task<List<Album>> GetAlbumsByYearRange(int? startYear, int? endYear)
        {
            return await _context
                .Albums
                .Where(x => x.PublishingYear >= startYear && x.PublishingYear <= endYear)
                .ToListAsync();
        }

        public async Task<List<Album>> GetAlbumsByDuration(TimeSpan? startDuration, TimeSpan? endDuration)
        {
            return await _context
                .Albums
                .Where(x => x.Duration >= startDuration && x.Duration <= endDuration)
                .ToListAsync();
        }
    }
}
