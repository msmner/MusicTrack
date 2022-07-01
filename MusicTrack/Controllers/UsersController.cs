using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicTrack.Dtos;
using MusicTrack.Models;
using MusicTrack.Services;

namespace MusicTrack.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userData)
        {
            var user = _mapper.Map<User>(userData);
            await _userService.AddAsync(user);
            var result = _mapper.Map<UserDto>(user);

            return result;
        }
    }
}
