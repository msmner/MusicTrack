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
    [Route("api/playlists")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlaylistService _playlistService;
        private readonly ITrackService _trackService;
        private readonly ILogger<PlaylistsController> _logger;
        public PlaylistsController(IMapper mapper, IPlaylistService playlistService, ITrackService trackService, ILogger<PlaylistsController> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _playlistService = playlistService ?? throw new ArgumentNullException(nameof(playlistService));
            _trackService = trackService ?? throw new ArgumentNullException(nameof(trackService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CreatePlaylistDto>> CreatePlaylist(CreatePlaylistDto createPlaylistDto)
        {
            var playlist = await _playlistService.CreatePlaylist(createPlaylistDto);
            var playlistDto = _mapper.Map<CreatePlaylistDto>(playlist);
            _logger.LogInformation($"created playlist {playlistDto}");

            return playlistDto;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GetPlaylistDto>>> Search(string? name)
        {
            List<PlayList> playlists = new();
            if (name != null)
            {
                playlists = await _playlistService.SearchPlaylistsByName(name);
            }

            List<GetPlaylistDto> playlistsDto = _mapper.Map<List<GetPlaylistDto>>(playlists);

            return playlistsDto;
        }

        [HttpGet("get/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetPlaylistDto>> GetPlaylist(string name)
        {
            var playlist = await _playlistService.GetPlaylistByName(name);
            var playlistDto = _mapper.Map<GetPlaylistDto>(playlist);

            return playlistDto;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<GetPlaylistDto>> GetPlaylist(Guid id)
        {
            var playlist = await _playlistService.GetPlaylistById(id);
            var playlistDto = _mapper.Map<GetPlaylistDto>(playlist);

            return playlistDto;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IList<GetPlaylistDto>> GetPlaylists()
        {
            IList<PlayList> playlists = await _playlistService.GetAllPlaylists();
            var playlistDtos = _mapper.Map<IList<GetPlaylistDto>>(playlists);

            return playlistDtos;
        }

        [HttpGet("{id}/tracks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IList<TrackDto>> GetPlaylistTracks(Guid id)
        {
            IList<Track> tracks = await _trackService.GetTracksByPlaylist(id);
            var tracksDto = _mapper.Map<IList<TrackDto>>(tracks);

            return tracksDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeletePlaylist(Guid id)
        {
            await _playlistService.DeletePlaylist(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePlaylist(Guid id, UpdatePlaylistDto playlistDto)
        {
            await _playlistService.UpdatePlaylist(id, playlistDto);

            return NoContent();
        }

        [HttpPut("{playlistId}/add/{trackId}/{position?}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> AddTrack(Guid playlistId, Guid trackId, int? position, string? body)
        {
            await _playlistService.AddTrack(playlistId, trackId, position);

            return NoContent();
        }

        [HttpPut("{playlistId}/remove/{trackId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> RemoveTrack(Guid playlistId, Guid trackId)
        {
            await _playlistService.RemoveTrack(playlistId, trackId);

            return NoContent();
        }
    }
}
