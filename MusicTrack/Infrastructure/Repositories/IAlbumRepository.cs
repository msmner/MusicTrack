using MusicTrack.Models;

namespace MusicTrack.Infrastructure.Repositories
{
    public interface IAlbumRepository
    {
        Album Add(Album album);

        Task<Album?> FindByIdAsync(Guid id);

        Task<IList<Album>> GetAllAsync();

        Album Remove(Album album);

        Task SaveChangesAsync();

        Album Update(Album album);

        Task<List<Album>> GetAlbumsByName(string? name);

        Task<List<Album>> GetAlbumsByYearRange(int? startYear, int? endYear);

        Task<List<Album>> GetAlbumsByDuration(TimeSpan? startDuration, TimeSpan? endDuration);
    }
}
