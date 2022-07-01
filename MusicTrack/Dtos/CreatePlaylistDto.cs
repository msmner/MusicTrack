using MusicTrack.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Dtos
{
    public class CreatePlaylistDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
