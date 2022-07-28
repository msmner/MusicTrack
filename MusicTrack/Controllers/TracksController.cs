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
    [Route("api/tracks")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;
        private readonly ILogger<TracksController> _logger;

        public TracksController(ITrackService trackService, IMapper mapper, ILogger<TracksController> logger)
        {
            _trackService = trackService ?? throw new ArgumentNullException(nameof(trackService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TrackDto>> GetTrack(Guid id)
        {
            Track? track = await _trackService.GetTrackById(id);
            var trackDto = _mapper.Map<TrackDto>(track);
            _logger.LogInformation($"got track {trackDto}");

            return trackDto;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IList<TrackDto>> GetTracks()
        {
            IList<Track> tracks = await _trackService.GetAllTracks();
            var trackDtos = _mapper.Map<IList<TrackDto>>(tracks);

            return trackDtos;
        }

        [HttpGet("album/{albumId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IList<TrackDto>> GetTracksByAlbum(Guid albumId)
        {
            IList<Track> tracks = await _trackService.GetTracksByAlbum(albumId);
            var trackDtos = _mapper.Map<IList<TrackDto>>(tracks);

            return trackDtos;
        }

        [HttpPost("{albumId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TrackDto>> CreateTrack(Guid albumId, CreateTrackDto createTrackDto)
        {
            Track? track = await _trackService.CreateTrack(albumId, createTrackDto);
            var trackDto = _mapper.Map<TrackDto>(track);

            return trackDto;
        }

        [HttpPut("{albumId}/{trackId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateTrack(Guid albumId, Guid trackId, UpdateTrackDto trackDto)
        {
            await _trackService.UpdateTrack(albumId, trackId, trackDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteTrack(Guid id)
        {
            await _trackService.DeleteTrack(id);

            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<TrackDto>>> SearchTracks(string? name, TimeSpan? startDuration, TimeSpan? endDuration, string? performer, string? arranger, TrackType? type)
        {
            List<Track>? tracks = new();
            if (name != null)
            {
                tracks = await _trackService.GetTracksByName(name);
            }
            else if (startDuration != null && endDuration != null)
            {
                tracks = await _trackService.GetTracksByDuration(startDuration, endDuration);
            }
            else if (performer != null)
            {
                tracks = await _trackService.GetTracksByPerformer(performer);
            }
            else if (arranger != null)
            {
                tracks = await _trackService.GetTracksByArranger(arranger);
            }
            else if (type != null)
            {
                tracks = await _trackService.GetTracksByType(type);
            }
            var tracksDto = _mapper.Map<List<TrackDto>>(tracks);

            return tracksDto;
        }
    }
}
