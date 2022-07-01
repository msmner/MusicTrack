using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Dtos
{
    public class UpdatePlaylistDto
    {
        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
