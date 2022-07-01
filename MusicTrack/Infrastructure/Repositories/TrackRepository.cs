using Microsoft.EntityFrameworkCore;
using MusicTrack.Models;

namespace MusicTrack.Infrastructure.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly MusicTrackDbContext _context;

        public TrackRepository(MusicTrackDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Track?> FindByIdAsync(Guid id)
        {
            return await _context
                .Tracks
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

        public Track Add(Track track)
        {
            return _context.Tracks.Add(track).Entity;
        }

        public Track Update(Track track)
        {
            return _context.Tracks.Update(track).Entity;
        }

        public Track Remove(Track track)
        {
            return _context.Tracks.Remove(track).Entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Track>> GetAllAsync()
        {
            return await _context
                .Tracks
                .ToListAsync();
        }

        public async Task<List<Track>> GetTracksByName(string? name)
        {
            return await _context
                .Tracks
                .Where(t => t.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<List<Track>> GetTracksByDurationRange(TimeSpan? startDuration, TimeSpan? endDuration)
        {
            return await _context
                .Tracks
                .Where(t => t.Duration >= startDuration && t.Duration <= endDuration)
                .ToListAsync();
        }

        public async Task<List<Track>> GetTracksByPerformer(string? performer)
        {
            return await _context
                .Tracks
                .Where(t => t.PerformedBy.Contains(performer))
                .ToListAsync();
        }

        public async Task<List<Track>> GetTracksByArranger(string? arranger)
        {
            return await _context
                .Tracks
                .Where(t => t.ArrangedBy.Contains(arranger))
                .ToListAsync();
        }

        public async Task<List<Track>> GetTracksByType(TrackType? type)
        {
            return await _context
                .Tracks
                .Where(t => t.Type == type)
                .ToListAsync();
        }
    }
}
