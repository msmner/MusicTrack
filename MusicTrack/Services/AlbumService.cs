using MusicTrack.Dtos;
using MusicTrack.Exceptions.NotFound;
using MusicTrack.Infrastructure.Repositories;
using MusicTrack.Models;

namespace MusicTrack.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly ITrackRepository _trackRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlaylistService _playlistService;
        public AlbumService(IAlbumRepository albumRepository, ITrackRepository trackRepository, IPlaylistRepository playlistRepository, IPlaylistService playlistService)
        {
            _albumRepository = albumRepository ?? throw new ArgumentNullException(nameof(albumRepository));
            _trackRepository = trackRepository ?? throw new ArgumentNullException(nameof(trackRepository));
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
        }

        public async Task<Album> CreateAlbum(CreateAlbumDto createAlbumDto)
        {
            var album = new Album
            {
                Name = createAlbumDto.Name,
                CreatedOn = DateTime.Now,
                Duration = TimeSpan.FromSeconds(0),
                PublishingYear = createAlbumDto.PublishingYear,
            };

            album = _albumRepository.Add(album);
            await _albumRepository.SaveChangesAsync();

            return album;
        }

        public async Task DeleteAlbum(Guid albumId)
        {
            Album? album = await _albumRepository.FindByIdAsync(albumId);
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }

            foreach (var track in album.Tracks)
            {
                _trackRepository.Remove(track);
            }

            IList<PlayList>? playlists = await _playlistRepository.GetPlaylistsByAlbumId(albumId);
            foreach (var playlist in playlists)
            {
                List<Track>? tracksToRemove = playlist.Tracks.Where(x => x.AlbumId == albumId).ToList();
                foreach (var track in tracksToRemove)
                {
                    await _playlistService.RemoveTrack(playlist.Id, track.Id);
                }
            }

            foreach (var track in album.Tracks)
            {
                _trackRepository.Remove(track);
            }

            _albumRepository.Remove(album);

            await _trackRepository.SaveChangesAsync();
            await _albumRepository.SaveChangesAsync();
        }

        public async Task<Album?> GetAlbumById(Guid albumId)
        {
            Album? album = await _albumRepository.FindByIdAsync(albumId);
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }

            return album;
        }

        public async Task UpdateAlbum(Guid albumId, UpdateAlbumDto albumDto)
        {
            Album? album = await _albumRepository.FindByIdAsync(albumId);
            if (album == null)
            {
                throw new AlbumNotFoundException();
            }

            album.Name = albumDto.Name;
            album.Duration = TimeSpan.FromHours(album.Tracks.Sum(x => x.Duration.TotalHours));
            album.PublishingYear = albumDto.PublishingYear;
            album.ModifiedOn = DateTime.UtcNow;

            _albumRepository.Update(album);
            await _albumRepository.SaveChangesAsync();

            return;
        }

        public async Task<List<Album>> GetAlbumsByName(string? name)
        {
            List<Album> albums = await _albumRepository.GetAlbumsByName(name);

            return albums;
        }

        public async Task<List<Album>> GetAlbumsByYearRange(int? startYear, int? endYear)
        {
            List<Album> albums = await _albumRepository.GetAlbumsByYearRange(startYear, endYear);

            return albums;
        }

        public async Task<List<Album>> GetAlbumsByDuration(TimeSpan? startDuration, TimeSpan? endDuration)
        {
            List<Album> albums = await _albumRepository.GetAlbumsByDuration(startDuration, endDuration);

            return albums;
        }
    }
}
