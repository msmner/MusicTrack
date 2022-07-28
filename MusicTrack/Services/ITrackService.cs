using MusicTrack.Dtos;
using MusicTrack.Models;

namespace MusicTrack.Services
{
    public interface ITrackService
    {
        Task<Track?> GetTrackById(Guid trackId);

        Task<IList<Track>> GetTracksByAlbum(Guid albumId);

        Task<IList<Track>> GetTracksByPlaylist(Guid playlistId);

        Task<Track> CreateTrack(Guid albumId, CreateTrackDto createTrackDto);

        Task UpdateTrack(Guid albumId, Guid trackId, UpdateTrackDto trackDto);

        Task DeleteTrack(Guid trackId);

        Task<List<Track>?> GetTracksByName(string? name);

        Task<List<Track>?> GetTracksByDuration(TimeSpan? startDuration, TimeSpan? endDuration);

        Task<List<Track>?> GetTracksByPerformer(string? performer);

        Task<List<Track>?> GetTracksByArranger(string? arranger);

        Task<List<Track>?> GetTracksByType(TrackType? type);

        Task<IList<Track>> GetAllTracks();
    }
}
