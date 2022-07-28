using MusicTrack.Dtos;
using MusicTrack.Models;

namespace MusicTrack.Services
{
    public interface IAlbumService
    {
        Task<Album?> GetAlbumById(Guid albumId);

        Task<Album> CreateAlbum(CreateAlbumDto createAlbumDto);

        Task UpdateAlbum(Guid albumId, UpdateAlbumDto albumDto);

        Task DeleteAlbum(Guid albumId);

        Task<List<Album>> GetAlbumsByName(string? name);

        Task<List<Album>> GetAlbumsByYearRange(int? startYear, int? endYear);

        Task<List<Album>> GetAlbumsByDuration(TimeSpan? startDuration, TimeSpan? endDuration);

        Task<IList<Album>> GetAlbums();
    }
}
