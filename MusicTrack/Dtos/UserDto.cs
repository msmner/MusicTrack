using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Dtos
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;
    }
}
