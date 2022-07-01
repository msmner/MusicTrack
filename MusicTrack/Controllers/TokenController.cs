using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicTrack.Dtos;
using MusicTrack.Exceptions.NotFound;
using MusicTrack.Exceptions.Other;
using MusicTrack.Infrastructure.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicTrack.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TokenController> _logger;

        public TokenController(IConfiguration config, IUserRepository userRepository, ILogger<TokenController> logger)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> Post(LoginDto userData)
        {
            if (userData != null && userData.Password != null)
            {
                var user = await _userRepository.FindUser(userData.Username, userData.Password);
                if (user == null)
                {
                    throw new UserNotFoundException();
                }

                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("userId", user.Id.ToString()),
                        new Claim("username", user.Username),
                        new Claim("role", user.Role),
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(100),
                    signingCredentials: signIn);

                _logger.LogInformation("issuing token");

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            else
            {
                throw new EnterValidCredentialsException();
            }
        }
    }
}
