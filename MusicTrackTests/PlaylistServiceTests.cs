using Microsoft.EntityFrameworkCore;
using MusicTrack.Dtos;
using MusicTrack.Exceptions.NotFound;
using MusicTrack.Exceptions.Other;
using MusicTrack.Infrastructure;
using MusicTrack.Infrastructure.Repositories;
using MusicTrack.Models;
using MusicTrack.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MusicTrackTests
{
    public class PlaylistServiceTests : IDisposable
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly MusicTrackDbContext dbContext;

        public PlaylistServiceTests()
        {
            var options = new DbContextOptionsBuilder<MusicTrackDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            dbContext = new MusicTrackDbContext(options);
            _trackRepository = new TrackRepository(dbContext);
            _playlistRepository = new PlayListRepository(dbContext);
        }

        [Fact]
        public async Task TestAddTrackThrowsPlaylistNotFoundException()
        {
            var service = await SetUp();
            var exception = await Assert.ThrowsAsync<PlaylistNotFoundException>(() => service.AddTrack(Guid.Parse("c2fa0440-32a3-4f52-af43-57c1096c4dc5"), Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"), null));
            Assert.Equal("Playlist not found", exception.Message);
        }

        [Fact]
        public async Task TestAddTrackThrowsTrackAlreadyIncludedException()
        {
            var service = await SetUp();
            var exception = await Assert.ThrowsAsync<TrackAlreadyIncludedInPlaylistException>(() => service.AddTrack(Guid.Parse("2128b3d9-c45d-40ef-9195-40e0ad7d8022"), Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"), null));
            Assert.Equal("Track is already included in the playlist", exception.Message);
        }

        [Fact]
        public async Task TestAddTrackThrowsDurationExceededException()
        {
            var service = await SetUp();
            var exception = await Assert.ThrowsAsync<PlaylistDurationExceededException>(() => service.AddTrack(Guid.Parse("3128b3d9-c45d-40ef-9195-40e0ad7d8022"), Guid.Parse("c7fa0440-32a3-4f52-af43-57c1096c4dc5"), null));
            Assert.Equal("Duration of playlist cannot exceed 2 hours", exception.Message);
        }

        [Fact]
        public async Task TestAddTrackToPositionWorks()
        {
            var service = await SetUp();
            await service.AddTrack(Guid.Parse("2128b3d9-c45d-40ef-9195-40e0ad7d8022"), Guid.Parse("c8fa0440-32a3-4f52-af43-57c1096c4dc5"), 1);
            var playlist = await service.GetPlaylistById(Guid.Parse("2128b3d9-c45d-40ef-9195-40e0ad7d8022"));
            Assert.NotNull(playlist);
            Assert.Equal("1:c8fa0440-32a3-4f52-af43-57c1096c4dc5\r\n2:c9fa0440-32a3-4f52-af43-57c1096c4dc5\r\n", playlist?.TrackPosition);
        }

        [Fact]
        public async Task TestAddTrackWithoutPositionWorks()
        {
            var service = await SetUp();
            await service.AddTrack(Guid.Parse("2128b3d9-c45d-40ef-9195-40e0ad7d8022"), Guid.Parse("c8fa0440-32a3-4f52-af43-57c1096c4dc5"), null);
            var playlist = await service.GetPlaylistById(Guid.Parse("2128b3d9-c45d-40ef-9195-40e0ad7d8022"));
            Assert.Equal("1:c9fa0440-32a3-4f52-af43-57c1096c4dc5\r\n2:c8fa0440-32a3-4f52-af43-57c1096c4dc5\r\n", playlist?.TrackPosition);
        }

        [Fact]
        public async Task TestCreatePlaylistWorks()
        {
            var service = await SetUp();
            var dto = new CreatePlaylistDto
            {
                IsPublic = true,
                Name = "test"
            };
            var playlist = await service.CreatePlaylist(dto);
            Assert.NotNull(playlist);
        }

        [Fact]
        public async Task TestDeletePlaylistsWorks()
        {
            var service = await SetUp();
            await service.DeletePlaylist(Guid.Parse("2128b3d9-c45d-40ef-9195-40e0ad7d8022"));
            Assert.Single(dbContext.PlayLists);
        }

        [Fact]
        public async Task TestRemoveTrackThrowsTrackIsNotIncludedInPlaylistException()
        {
            var service = await SetUp();
            var exception = await Assert.ThrowsAsync<TrackIsNotIncludedInPlaylistException>(() => service.RemoveTrack(Guid.Parse("3128b3d9-c45d-40ef-9195-40e0ad7d8022"), Guid.Parse("c7fa0440-32a3-4f52-af43-57c1096c4dc5")));
            Assert.Equal("Track is not included in the playlist", exception.Message);
        }

        [Fact]
        public async Task TestRemoveTrackWorks()
        {
            var service = await SetUp();
            await service.RemoveTrack(Guid.Parse("3128b3d9-c45d-40ef-9195-40e0ad7d8022"), Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"));
            var playlist = await service.GetPlaylistById(Guid.Parse("3128b3d9-c45d-40ef-9195-40e0ad7d8022"));
            Assert.Equal("1:c8fa0440-32a3-4f52-af43-57c1096c4dc5\r\n", playlist?.TrackPosition);
        }

        [Fact]
        public async Task TestUpdateWorks()
        {
            var service = await SetUp();
            var dto = new UpdatePlaylistDto
            {
                IsPublic = false,
                Name = "test update"
            };
            await service.UpdatePlaylist(Guid.Parse("2128b3d9-c45d-40ef-9195-40e0ad7d8022"), dto);
            var playlist = await service.GetPlaylistById(Guid.Parse("2128b3d9-c45d-40ef-9195-40e0ad7d8022"));
            Assert.Equal("test update", playlist?.Name);
        }

        [Fact]
        public async Task TestSearchPlaylistsByNameWorks()
        {
            var service = await SetUp();
            var result = await service.SearchPlaylistsByName("test");
            Assert.Equal(2, result.Count);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    dbContext.Database.EnsureDeleted();
                }
            }
            finally
            {

            }
        }

        private async Task<PlaylistService> SetUp()
        {
            var playlist = new PlayList { Id = Guid.Parse("2128b3d9-c45d-40ef-9195-40e0ad7d8022"), Duration = TimeSpan.FromSeconds(30), IsPublic = true, Name = "test", TrackPosition = "1:c9fa0440-32a3-4f52-af43-57c1096c4dc5\r\n" };
            var playlist2 = new PlayList { Id = Guid.Parse("3128b3d9-c45d-40ef-9195-40e0ad7d8022"), Duration = TimeSpan.FromSeconds(7200), IsPublic = true, Name = "test", TrackPosition = "1:c9fa0440-32a3-4f52-af43-57c1096c4dc5\r\n2:c8fa0440-32a3-4f52-af43-57c1096c4dc5\r\n" };

            var track = new Track { Id = Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"), AlbumId = Guid.Parse("42cb12d3-d860-4fe5-978d-b37eca61cdce"), ArrangedBy = "test", Name = "test", PerformedBy = "test", WrittenBy = "test", Type = TrackType.BackgroundMusic, Duration = TimeSpan.FromSeconds(2) };
            var track2 = new Track { Id = Guid.Parse("c8fa0440-32a3-4f52-af43-57c1096c4dc5"), AlbumId = Guid.Parse("41cb12d3-d860-4fe5-978d-b37eca61cdce"), ArrangedBy = "test", Name = "test", PerformedBy = "test", WrittenBy = "test", Type = TrackType.Recording, Duration = TimeSpan.FromSeconds(20) };
            var track3 = new Track { Id = Guid.Parse("c7fa0440-32a3-4f52-af43-57c1096c4dc5"), AlbumId = Guid.Parse("41cb12d3-d860-4fe5-978d-b37eca61cdce"), ArrangedBy = "test", Name = "test", PerformedBy = "test", WrittenBy = "test", Type = TrackType.Recording, Duration = TimeSpan.FromSeconds(20) };

            playlist.Tracks.Add(track);
            playlist2.Tracks.Add(track2);
            playlist2.Tracks.Add(track);

            track.PlayLists.Add(playlist);
            track2.PlayLists.Add(playlist2);


            await dbContext.Tracks.AddAsync(track);
            await dbContext.Tracks.AddAsync(track2);
            await dbContext.Tracks.AddAsync(track3);

            await dbContext.PlayLists.AddAsync(playlist);
            await dbContext.PlayLists.AddAsync(playlist2);

            await dbContext.SaveChangesAsync();

            return new PlaylistService(_playlistRepository, _trackRepository);
        }
    }
}
