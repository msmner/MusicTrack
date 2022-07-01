using Microsoft.EntityFrameworkCore;
using MusicTrack.Dtos;
using MusicTrack.Exceptions.NotFound;
using MusicTrack.Infrastructure;
using MusicTrack.Infrastructure.Repositories;
using MusicTrack.Models;
using MusicTrack.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MusicTrackTests
{
    public class AlbumServiceTests : IDisposable
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlaylistService _playlistService;
        private readonly MusicTrackDbContext dbContext;

        public AlbumServiceTests()
        {
            var options = new DbContextOptionsBuilder<MusicTrackDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            dbContext = new MusicTrackDbContext(options);
            _trackRepository = new TrackRepository(dbContext);
            _albumRepository = new AlbumRepository(dbContext);
            _playlistRepository = new PlayListRepository(dbContext);
            _playlistService = new PlaylistService(_playlistRepository, _trackRepository);
        }

        [Fact]
        public async Task TestCreateAlbumWorks()
        {
            var service = await SetUp();
            var dto = new CreateAlbumDto
            {
                Name = "test",
                PublishingYear = 2022
            };
            var result = await service.CreateAlbum(dto);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestDeleteAlbumWorks()
        {
            var service = await SetUp();
            await service.DeleteAlbum(Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"));
            Assert.Equal(0, dbContext.Albums.Count());
        }

        [Fact]
        public async Task TestDeleteAlbumThrowsAlbumNotFound()
        {
            var service = await SetUp();
            var exception = await Assert.ThrowsAsync<AlbumNotFoundException>(() => service.DeleteAlbum(Guid.Parse("22cb12d3-d860-4fe5-978d-b37eca61cdce")));
            Assert.Equal("Album not found", exception.Message);
        }

        [Fact]
        public async Task TestGetAlbumByIdWorks()
        {
            var service = await SetUp();
            var album = await service.GetAlbumById(Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"));
            Assert.Equal(Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"),album?.Id);
        }

        [Fact]
        public async Task TestUpdateAlbumWorks()
        {
            var service = await SetUp();
            var dto = new UpdateAlbumDto
            {
                Name = "test update",
                PublishingYear = 2022
            };
            await service.UpdateAlbum(Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"), dto);
            var album = await service.GetAlbumById(Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"));
            Assert.Equal("test update", album?.Name);
        }

        [Fact]
        public async Task TestGetAlbumsByNameWorks()
        {
            var service = await SetUp();
            var result = await service.GetAlbumsByName("test");
            Assert.Single(result);
        }

        [Fact]
        public async Task TestGetAlbumsByYearRangeWorks()
        {
            var service = await SetUp();
            var result = await service.GetAlbumsByYearRange(2020, 2022);
            Assert.Single(result);
        }

        [Fact]
        public async Task TestGetAlbumsByDurationWorks()
        {
            var service = await SetUp();
            var result = await service.GetAlbumsByDuration(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(2));
            Assert.Single(result);
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

        private async Task<AlbumService> SetUp()
        {
            var album = new Album { Id = Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"), CreatedOn = DateTime.UtcNow, Duration = TimeSpan.FromSeconds(2), Name = "test", PublishingYear = 2022, Tracks = new List<Track>() };
            //var album2 = new Album { Id = Guid.Parse("62cb12d3-d860-4fe5-978d-b37eca61cdce"), CreatedOn = DateTime.UtcNow, Duration = TimeSpan.FromSeconds(2), Name = "test", PublishingYear = 2022, Tracks = new List<Track>() };

            //var track = new Track { Id = Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"), AlbumId = Guid.Parse("42cb12d3-d860-4fe5-978d-b37eca61cdce"), ArrangedBy = "test", Name = "test", PerformedBy = "test", WrittenBy = "test", Type = TrackType.BackgroundMusic };
            //var track2 = new Track { Id = Guid.Parse("c8fa0440-32a3-4f52-af43-57c1096c4dc5"), AlbumId = Guid.Parse("41cb12d3-d860-4fe5-978d-b37eca61cdce"), ArrangedBy = "test", Name = "test", PerformedBy = "test", WrittenBy = "test", Type = TrackType.Recording };

            //album.Tracks.Add(track);

            //await dbContext.Tracks.AddAsync(track);
            //await dbContext.Tracks.AddAsync(track2);
            await dbContext.Albums.AddAsync(album);
            //await dbContext.Albums.AddAsync(album2);
            await dbContext.SaveChangesAsync();

            return new AlbumService(_albumRepository, _trackRepository, _playlistRepository, _playlistService);
        }
    }
}
