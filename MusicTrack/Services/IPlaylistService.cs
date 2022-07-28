using MusicTrack.Dtos;
using MusicTrack.Models;

namespace MusicTrack.Services
{
    public interface IPlaylistService
    {
        Task<PlayList?> GetPlaylistById(Guid playlistId);

        Task<PlayList> CreatePlaylist(CreatePlaylistDto createPlaylistDto);

        Task UpdatePlaylist(Guid playlistId, UpdatePlaylistDto playlistDto);

        Task DeletePlaylist(Guid playlistId);

        Task AddTrack(Guid playlistId, Guid trackId, int? position);

        Task RemoveTrack(Guid playlistId, Guid trackId);

        Task<List<PlayList>> SearchPlaylistsByName(string? name);

        Task<IList<PlayList>> GetAllPlaylists();

        Task<PlayList> GetPlaylistByName(string name);
    }
}
