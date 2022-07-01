using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Dtos
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
