using MusicTrack.Models;

namespace MusicTrack.Infrastructure.Repositories
{
    public interface ITrackRepository
    {
        Track Add(Track track);
        
        Task<Track?> FindByIdAsync(Guid id);

        Task<IList<Track>> GetAllAsync();

        Track Remove(Track track);

        Task SaveChangesAsync();

        Track Update(Track track);

        Task<List<Track>> GetTracksByName(string? name);

        Task<List<Track>> GetTracksByDurationRange(TimeSpan? startDuration, TimeSpan? endDuration);

        Task<List<Track>> GetTracksByPerformer(string? performer);

        Task<List<Track>> GetTracksByArranger(string? arranger);

        Task<List<Track>> GetTracksByType(TrackType? type);
    }
}
