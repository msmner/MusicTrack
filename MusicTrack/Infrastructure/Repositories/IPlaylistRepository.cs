using MusicTrack.Models;

namespace MusicTrack.Infrastructure.Repositories
{
    public interface IPlaylistRepository
    {
        PlayList Add(PlayList playlist);

        Task<PlayList?> FindByIdAsync(Guid id);

        Task<IList<PlayList>> GetAllAsync();

        PlayList Remove(PlayList playlist);

        Task SaveChangesAsync();

        PlayList Update(PlayList playlist);

        Task<IList<PlayList>> GetPlaylistsByAlbumId(Guid albumId);

        Task<IList<PlayList>> GetPlaylistByTrackId(Guid trackId);

        Task<List<PlayList>> GetPlaylistsByName(string? name);

        Task<PlayList?> GetPlaylistByIdAsync(Guid id);

        Task<PlayList?> GetPlaylistByName(string name);
    }
}
