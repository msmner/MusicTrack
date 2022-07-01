using Microsoft.EntityFrameworkCore;
using MusicTrack.Dtos;
using MusicTrack.Exceptions.NotFound;
using MusicTrack.Exceptions.Other;
using MusicTrack.Infrastructure;
using MusicTrack.Infrastructure.Repositories;
using MusicTrack.Models;
using MusicTrack.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MusicTrack.Tests
{
    public class TrackServiceTests : IDisposable
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IPlaylistRepository _playlistRepository;
        private readonly IPlaylistService _playlistService;
        private readonly MusicTrackDbContext dbContext;

        public TrackServiceTests()
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
        public async Task TestCreateTrackWorks()
        {
            TrackService service = await SetUp();
            CreateTrackDto dto = new()
            {
                Name = "test",
                WrittenBy = "test",
                PerformedBy = "test",
                ArrangedBy = "test",
                Duration = TimeSpan.FromSeconds(1),
                Type = TrackType.BackgroundMusic
            };
            var track = await service.CreateTrack(Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"), dto);
            Assert.NotNull(track);
        }

        [Fact]
        public async Task TestCreateTrackThrowsAlbumNotFoundException()
        {
            TrackService service = await SetUp();
            CreateTrackDto dto = new()
            {
                Name = "test",
                WrittenBy = "test",
                PerformedBy = "test",
                ArrangedBy = "test",
                Duration = TimeSpan.FromSeconds(1),
                Type = TrackType.BackgroundMusic
            };
            var exception = await Assert.ThrowsAsync<AlbumNotFoundException>(() => service.CreateTrack(Guid.Parse("42cb12d3-d860-4fe5-978d-b37eca61cdce"), dto));
            Assert.Equal("Album not found", exception.Message);
        }

        [Fact]
        public async Task TestCreateTrackThrowsTrackTypeNotValidException()
        {
            TrackService service = await SetUp();
            CreateTrackDto dto = new()
            {
                Name = "test",
                WrittenBy = "test",
                PerformedBy = "test",
                ArrangedBy = "test",
                Duration = TimeSpan.FromSeconds(1),
                Type = (TrackType)6
            };
            var exception = await Assert.ThrowsAsync<TrackTypeNotValidException>(() => service.CreateTrack(Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"), dto));
            Assert.Equal("Track type not valid", exception.Message);
        }

        [Fact]
        public async Task TestDeleteTrackThrowsTrackNotFound()
        {
            TrackService service = await SetUp();
            var exception = await Assert.ThrowsAsync<TrackNotFoundException>(() => service.DeleteTrack(Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce")));
            Assert.Equal("Track not found", exception.Message);
        }

        [Fact]
        public async Task TestDeleteTrackThrowsAlbumNotFound()
        {
            TrackService service = await SetUp();
            var exception = await Assert.ThrowsAsync<AlbumNotFoundException>(() => service.DeleteTrack(Guid.Parse("c8fa0440-32a3-4f52-af43-57c1096c4dc5")));
            Assert.Equal("Album not found", exception.Message);
        }

        [Fact]
        public async Task TestDeleteWorks()
        {
            var service = await SetUp();
            await service.DeleteTrack(Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"));
            Assert.Equal(1, dbContext.Tracks.Count());
        }

        [Fact]
        public async Task TestGetTrackByIdWorks()
        {
            var service = await SetUp();
            var track = await service.GetTrackById(Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"));
            Assert.Equal(track?.Id, Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"));
        }

        [Fact]
        public async Task TestUpdateTrackThrowsTrackDoesNotBelongToAlbum()
        {
            var service = await SetUp();
            var dto = new UpdateTrackDto
            {
                Name = "test",
                WrittenBy = "test",
                PerformedBy = "test",
                ArrangedBy = "test",
                Duration = TimeSpan.FromSeconds(1),
                Type = TrackType.BackgroundMusic
            };
            var exception = await Assert.ThrowsAsync<TrackDoesNotBelongToAlbumException>(() => service.UpdateTrack(Guid.Parse("62cb12d3-d860-4fe5-978d-b37eca61cdce"), Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"), dto));
            Assert.Equal("Track does not belong to this album", exception.Message);
        }

        [Fact]
        public async Task TestUpdateTrackWorks()
        {
            var service = await SetUp();
            var dto = new UpdateTrackDto
            {
                Name = "test update",
                WrittenBy = "test",
                PerformedBy = "test",
                ArrangedBy = "test",
                Duration = TimeSpan.FromSeconds(1),
                Type = TrackType.BackgroundMusic
            };
            await service.UpdateTrack(Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"), Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"), dto);
            var track = await service.GetTrackById(Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"));

            Assert.Equal("test update", track?.Name);
        }

        [Fact]
        public async Task TestGetTracksByNameWorks()
        {
            var service = await SetUp();
            var result = await service.GetTracksByName("test");
            Assert.Equal(2, result?.Count);
        }

        [Fact]
        public async Task TestGetTracksByDurationWorks()
        {
            var service = await SetUp();
            var result = await service.GetTracksByDuration(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(5));
            Assert.Equal(2, result?.Count);
        }

        [Fact]
        public async Task TestGetTracksByPerformerWorks()
        {
            var service = await SetUp();
            var result = await service.GetTracksByPerformer("test");
            Assert.Equal(2, result?.Count);
        }

        [Fact]
        public async Task TestGetTracksByArrangerWorks()
        {
            var service = await SetUp();
            var result = await service.GetTracksByArranger("test");
            Assert.Equal(2, result?.Count);
        }

        [Fact]
        public async Task TestGetTracksByTypeWorks()
        {
            var service = await SetUp();
            var result = await service.GetTracksByType(TrackType.Recording);
            Assert.Equal(1, result?.Count);
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

        private async Task<TrackService> SetUp()
        {
            var album = new Album { Id = Guid.Parse("72cb12d3-d860-4fe5-978d-b37eca61cdce"), CreatedOn = DateTime.UtcNow, Duration = TimeSpan.FromSeconds(2), Name = "test", PublishingYear = 2022, Tracks = new List<Track>() };
            var album2 = new Album { Id = Guid.Parse("62cb12d3-d860-4fe5-978d-b37eca61cdce"), CreatedOn = DateTime.UtcNow, Duration = TimeSpan.FromSeconds(2), Name = "test", PublishingYear = 2022, Tracks = new List<Track>() };

            var track = new Track { Id = Guid.Parse("c9fa0440-32a3-4f52-af43-57c1096c4dc5"), AlbumId = Guid.Parse("42cb12d3-d860-4fe5-978d-b37eca61cdce"), ArrangedBy = "test", Name = "test", PerformedBy = "test", WrittenBy = "test", Type = TrackType.BackgroundMusic };
            var track2 = new Track { Id = Guid.Parse("c8fa0440-32a3-4f52-af43-57c1096c4dc5"), AlbumId = Guid.Parse("41cb12d3-d860-4fe5-978d-b37eca61cdce"), ArrangedBy = "test", Name = "test", PerformedBy = "test", WrittenBy = "test", Type = TrackType.Recording };
            
            album.Tracks.Add(track);

            await dbContext.Tracks.AddAsync(track);
            await dbContext.Tracks.AddAsync(track2);
            await dbContext.Albums.AddAsync(album);
            await dbContext.Albums.AddAsync(album2);
            await dbContext.SaveChangesAsync();

            return new TrackService(_trackRepository, _albumRepository, _playlistRepository, _playlistService);
        }
    }
}
