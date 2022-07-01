using MusicTrack.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Dtos
{
    public class CreateTrackDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string WrittenBy { get; set; } = null!;

        [Required]
        public string PerformedBy { get; set; } = null!;

        [Required]
        public string ArrangedBy { get; set; } = null!;

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public TrackType Type { get; set; }
    }
}
