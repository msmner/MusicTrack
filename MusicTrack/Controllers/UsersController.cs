using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicTrack.Dtos;
using MusicTrack.Services;

namespace MusicTrack.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper, ITokenService tokenService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> SignUp(RegisterDto registerDto)
        {
            var user = await _userService.Register(registerDto);

            string token = await _tokenService.CreateToken(user);

            var userDto = _mapper.Map<UserDto>(user);
            userDto.Token = token;

            return userDto;
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {

            var user = await _userService.Login(loginDto);

            string token = await _tokenService.CreateToken(user);

            var userDto = new UserDto
            {
                UserName = user.UserName,
                Token = token,
            };

            return userDto;
        }
    }
}
