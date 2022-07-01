using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Dtos
{
    public class CreateAlbumDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int PublishingYear { get; set; }
    }
}
