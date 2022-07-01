using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Dtos
{
    public class GetPlaylistDto
    {
        public GetPlaylistDto()
        {
            Tracks = new List<TrackDto>();
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public ICollection<TrackDto> Tracks { get; set; }

        public string? TrackPosition { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
