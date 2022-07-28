using MusicTrack.Dtos;
using MusicTrack.Exceptions.NotFound;
using MusicTrack.Exceptions.Other;
using MusicTrack.Infrastructure.Repositories;
using MusicTrack.Models;
using System.Text;

namespace MusicTrack.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;
        private readonly ITrackRepository _trackRepository;

        public PlaylistService(IPlaylistRepository playlistRepository, ITrackRepository trackRepository)
        {
            _playlistRepository = playlistRepository ?? throw new ArgumentNullException(nameof(playlistRepository));
            _trackRepository = trackRepository ?? throw new ArgumentNullException(nameof(trackRepository));
        }

        public async Task AddTrack(Guid playlistId, Guid trackId, int? position)
        {
            Track? track = await _trackRepository.FindByIdAsync(trackId);
            if (track == null)
            {
                throw new TrackNotFoundException();
            }

            PlayList? playlist = await _playlistRepository.FindByIdAsync(playlistId);
            if (playlist == null)
            {
                throw new PlaylistNotFoundException();
            }

            bool trackIncluded = playlist.Tracks.Any(x => x == track);
            if (trackIncluded)
            {
                throw new TrackAlreadyIncludedInPlaylistException();
            }

            if (track.Duration.TotalHours + playlist.Duration.TotalHours > 2)
            {
                throw new PlaylistDurationExceededException();
            }

            playlist.Duration = track.Duration + playlist.Duration;

            track.PlayLists.Add(playlist);
            playlist.Tracks.Add(track);

            var sb = new StringBuilder();
            List<string>? data = playlist.TrackPosition?.Split("\r\n").ToList();
            if (data != null)
            {
                if (String.IsNullOrWhiteSpace(data[^1]))
                {
                    data.Remove(data[^1]);
                }
            }

            if (data == null)
            {
                sb.AppendLine($"1:{track.Id}");
            }
            else if (position.HasValue && position.Value <= data.Count)
            {
                for (int i = 0; i < position.Value - 1; i++)
                {
                    sb.AppendLine(data[i]);
                }

                sb.AppendLine($"{position.Value}:{track.Id}");
                for (int i = position.Value - 1; i < data.Count; i++)
                {
                    var trackIdSplit = data[i].Split(":")[1];
                    sb.AppendLine($"{position.Value + 1}:{trackIdSplit}");
                }
            }
            else
            {
                for (int i = 0; i < data.Count; i++)
                {
                    sb.AppendLine(data[i]);
                }

                sb.AppendLine($"{data.Count + 1}:{track.Id}");
            }

            string orderedTracks = sb.ToString();
            playlist.TrackPosition = orderedTracks;

            _playlistRepository.Update(playlist);
            await _playlistRepository.SaveChangesAsync();

            return;
        }

        public async Task<PlayList> CreatePlaylist(CreatePlaylistDto createPlaylistDto)
        {
            var playlist = new PlayList
            {
                Name = createPlaylistDto.Name,
                IsPublic = createPlaylistDto.IsPublic,
                CreatedOn = DateTime.UtcNow,
                Duration = TimeSpan.FromSeconds(0)
            };

            _playlistRepository.Add(playlist);
            await _playlistRepository.SaveChangesAsync();

            return playlist;
        }

        public async Task DeletePlaylist(Guid playlistId)
        {
            PlayList? playlist = await _playlistRepository.FindByIdAsync(playlistId);
            if (playlist == null)
            {
                throw new PlaylistNotFoundException();
            }

            _playlistRepository.Remove(playlist);
            await _playlistRepository.SaveChangesAsync();

            return;
        }

        public async Task<PlayList?> GetPlaylistById(Guid playlistId)
        {
            PlayList? playlist = await _playlistRepository.FindByIdAsync(playlistId);
            if (playlist == null)
            {
                throw new PlaylistNotFoundException();
            }

            return playlist;
        }

        public async Task<IList<PlayList>> GetAllPlaylists()
        {
            return await _playlistRepository.GetAllAsync();
        }

        public async Task RemoveTrack(Guid playlistId, Guid trackId)
        {
            Track? track = await _trackRepository.FindByIdAsync(trackId);
            if (track == null)
            {
                throw new TrackNotFoundException();
            }

            PlayList? playlist = await _playlistRepository.FindByIdAsync(playlistId);
            if (playlist == null)
            {
                throw new PlaylistNotFoundException();
            }

            bool trackIncluded = playlist.Tracks.Any(x => x == track);
            if (!trackIncluded)
            {
                throw new TrackIsNotIncludedInPlaylistException();
            }

            var sb = new StringBuilder();
            List<string>? data = playlist.TrackPosition?.Split("\r\n").ToList();

            if (data == null)
            {
                throw new PlaylistIsEmptyException();
            }
            else
            {
                bool isRemoved = false;
                if (String.IsNullOrWhiteSpace(data[^1]))
                {
                    data.Remove(data[^1]);
                }

                for (int i = 0; i < data.Count; i++)
                {
                    string[] dataItems = data[i].Split(":");
                    int index = int.Parse(dataItems[0]);
                    Guid id = Guid.Parse(dataItems[1]);
                    if (isRemoved == false)
                    {
                        if (id != track.Id)
                        {
                            sb.AppendLine($"{index}:{id}");
                        }
                        else
                        {
                            isRemoved = true;
                            continue;
                        }
                    }
                    else
                    {
                        sb.AppendLine($"{index - 1}:{id}");
                    }
                }
            }

            string orderedTracks = sb.ToString();
            playlist.TrackPosition = orderedTracks;

            track.PlayLists.Remove(playlist);
            playlist.Tracks.Remove(track);
            playlist.Duration -= track.Duration;

            _playlistRepository.Update(playlist);
            await _playlistRepository.SaveChangesAsync();

            return;
        }

        public async Task UpdatePlaylist(Guid playlistId, UpdatePlaylistDto playlistDto)
        {
            PlayList? playlist = await _playlistRepository.FindByIdAsync(playlistId);
            if (playlist == null)
            {
                throw new PlaylistNotFoundException();
            }

            playlist.Name = playlistDto.Name;
            playlist.IsPublic = playlistDto.IsPublic;
            playlist.ModifiedOn = DateTime.UtcNow;

            _playlistRepository.Update(playlist);
            await _playlistRepository.SaveChangesAsync();

            return;
        }

        public async Task<List<PlayList>> SearchPlaylistsByName(string? name)
        {
            List<PlayList> playlists = await _playlistRepository.GetPlaylistsByName(name);

            return playlists;
        }

        public async Task<PlayList> GetPlaylistByName(string name)
        {
            PlayList? playlist = await _playlistRepository.GetPlaylistByName(name);
            if (playlist == null)
            {
                throw new PlaylistNotFoundException();
            }

            return playlist;
        }
    }
}
