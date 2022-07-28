using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicTrack.Dtos;
using MusicTrack.Exceptions.AlreadyExists;
using MusicTrack.Exceptions.NotFound;
using MusicTrack.Exceptions.Other;
using MusicTrack.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicTrack.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserService> _logger;

        public UserService(IConfiguration config,
            UserManager<User> userManager, SignInManager<User> signInManager,
            ILogger<UserService> logger)
        {
            _configuration = config ?? throw new ArgumentNullException(nameof(config));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<User> Register(RegisterDto userDto)
        {
            var userDb = await _userManager.FindByNameAsync(userDto.UserName.ToLower());
            if (userDb != null)
            {
                throw new UsernameAlreadyExistsException();
            }

            var user = new User { UserName = userDto.UserName };
            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                _logger.LogError("Got following errors creating the user: ", result.Errors);
                throw new ErrorCreatingUser();
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "user");
            if (!roleResult.Succeeded)
            {
                _logger.LogError("Error adding role to user: ", roleResult.Errors);
                throw new AddRoleToUserException();
            }

            return user;
        }

        public async Task<User> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            if (user == null)
            {
                throw new InvalidUsernameOrPasswordException();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                throw new InvalidUsernameOrPasswordException();
            }

            return user;
        }
    }
}
