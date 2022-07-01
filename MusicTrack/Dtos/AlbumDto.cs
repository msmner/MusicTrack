using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Dtos
{
    public class AlbumDto
    {
        public AlbumDto()
        {
            Tracks = new List<string>();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int PublishingYear { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public List<string> Tracks { get; set; }
    }
}
