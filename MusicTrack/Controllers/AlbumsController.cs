using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicTrack.Dtos;
using MusicTrack.Models;
using MusicTrack.Services;

namespace MusicTrack.Controllers
{
    [Authorize(Roles = "user")]
    [Produces("application/json")]
    [Route("api/albums")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IMapper _mapper;
        private readonly ILogger<AlbumsController> _logger;

        public AlbumsController(IAlbumService albumsService, IMapper mapper, ILogger<AlbumsController> logger)
        {
            _albumService = albumsService ?? throw new ArgumentNullException(nameof(albumsService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AlbumDto>> GetAlbum(Guid id)
        {
            Album? album = await _albumService.GetAlbumById(id);
            var albumDto = _mapper.Map<AlbumDto>(album);
            _logger.LogInformation($"got album {albumDto}");

            return albumDto;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<AlbumDto>>> GetAlbums()
        {
            IList<Album>? albums = await _albumService.GetAlbums();
            var albumDtos = _mapper.Map<IList<AlbumDto>>(albums);

            return Ok(albumDtos);
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AlbumDto>> CreateAlbum(CreateAlbumDto createAlbumDto)
        {
            Album? album = await _albumService.CreateAlbum(createAlbumDto);
            var albumDto = _mapper.Map<AlbumDto>(album);

            return albumDto;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateAlbum(Guid id, UpdateAlbumDto albumDto)
        {
            await _albumService.UpdateAlbum(id, albumDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteAlbum(Guid id)
        {
            await _albumService.DeleteAlbum(id);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AlbumDto>>> Search(string? name, int? startYear, int? endYear, TimeSpan? startDuration, TimeSpan? endDuration)
        {
            List<Album> albums = new();
            if (name != null)
            {
                albums = await _albumService.GetAlbumsByName(name);
            }
            else if (startYear != null && endYear != null)
            {
                albums = await _albumService.GetAlbumsByYearRange(startYear, endYear);
            }
            else if (startDuration != null && endDuration != null)
            {
                albums = await _albumService.GetAlbumsByDuration(startDuration, endDuration);
            }

            var albumsDto = _mapper.Map<List<AlbumDto>>(albums);

            return albumsDto;
        }
    }
}
