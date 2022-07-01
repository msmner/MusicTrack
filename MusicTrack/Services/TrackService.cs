using MusicTrack.Dtos;
using MusicTrack.Exceptions.NotFound;
using MusicTrack.Exceptions.Other;
using MusicTrack.Infrastructure.Repositories;
using MusicTrack.Models;

namespace MusicTrack.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlaylistService _playlistService;
        public TrackService(ITrackRepository trackRepository, IAlbumRepository albumRepository, IPlaylistRepository playlistRepository, IPlaylistService playlistService)
        {
            _trackRepository = trackRepository ?? throw new ArgumentNullException(nameof(trackRepository));
            _albumRepository = albumRepository ?? throw new ArgumentNullException(nameof(albumRepository));
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        public async Task<Track> CreateTrack(Guid albumId, CreateTrackDto createTrackDto)
        {
            Album? album = await _albumRepository.FindByIdAsync(albumId);
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }
            
            bool isValidType = Enum.IsDefined(typeof(TrackType), createTrackDto.Type);
            if (!isValidType)
            {
                throw new TrackTypeNotValidException();
            }

            var track = new Track
            {
                Name = createTrackDto.Name,
                PerformedBy = createTrackDto.PerformedBy,
                ArrangedBy = createTrackDto.ArrangedBy,
                CreatedOn = DateTime.Now,
                AlbumId = albumId,
                Duration = createTrackDto.Duration,
                Type = createTrackDto.Type,
                WrittenBy = createTrackDto.WrittenBy,
            };

            track = _trackRepository.Add(track);
            await _trackRepository.SaveChangesAsync();

            album.Tracks.Add(track);
            album.Duration += track.Duration;
            album.ModifiedOn = DateTime.Now;
            await _albumRepository.SaveChangesAsync();

            return track;
        }

        public async Task DeleteTrack(Guid trackId)
        {
            Track? track = await _trackRepository.FindByIdAsync(trackId);
            if (track == null)
            {
                throw new TrackNotFoundException();
            }

            Album? album = await _albumRepository.FindByIdAsync(track.AlbumId);
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }

            IList<PlayList>? playlists = await _playlistRepository.GetPlaylistByTrackId(trackId);
            foreach (var playlist in playlists)
            {
                await _playlistService.RemoveTrack(playlist.Id, track.Id);
            }

            album.Tracks.Remove(track);
            album.Duration -= track.Duration;
            _trackRepository.Remove(track);

            await _trackRepository.SaveChangesAsync();
            await _albumRepository.SaveChangesAsync();
        }

        public async Task<Track?> GetTrackById(Guid trackId)
        {
            Track? track = await _trackRepository.FindByIdAsync(trackId);
            if (track == null)
            {
                throw new TrackNotFoundException();
            }

            return track;
        }

        public async Task UpdateTrack(Guid albumId, Guid trackId, UpdateTrackDto trackDto)
        {
            Album? album = await _albumRepository.FindByIdAsync(albumId);
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }

            Track? track = await _trackRepository.FindByIdAsync(trackId);
            if (track == null)
            {
                throw new TrackNotFoundException();
            }

            if (albumId != track.AlbumId)
            {
                throw new TrackDoesNotBelongToAlbumException();
            }

            track.Name = trackDto.Name;
            track.WrittenBy = trackDto.WrittenBy;
            track.ArrangedBy = trackDto.ArrangedBy;
            track.Duration = trackDto.Duration;
            track.Type = trackDto.Type;
            track.PerformedBy = trackDto.PerformedBy;
            track.ModifiedOn = DateTime.UtcNow;

            _trackRepository.Update(track);
            await _trackRepository.SaveChangesAsync();

            return;
        }

        public async Task<List<Track>?> GetTracksByName(string? name)
        {
            List<Track>? tracks = await _trackRepository.GetTracksByName(name);

            return tracks;
        }

        public async Task<List<Track>?> GetTracksByDuration(TimeSpan? startDuration, TimeSpan? endDuration)
        {
            List<Track>? tracks = await _trackRepository.GetTracksByDurationRange(startDuration, endDuration);

            return tracks;
        }

        public async Task<List<Track>?> GetTracksByPerformer(string? performer)
        {
            List<Track>? tracks = await _trackRepository.GetTracksByPerformer(performer);

            return tracks;
        }

        public async Task<List<Track>?> GetTracksByArranger(string? arranger)
        {
            List<Track>? tracks = await _trackRepository.GetTracksByArranger(arranger);

            return tracks;
        }

        public async Task<List<Track>?> GetTracksByType(TrackType? type)
        {
            bool isValidType = Enum.IsDefined(typeof(TrackType), type!);
            if (!isValidType)
            {
                throw new TrackTypeNotValidException();
            }

            List<Track>? tracks = await _trackRepository.GetTracksByType(type);

            return tracks;
        }
    }
}
