using System.ComponentModel.DataAnnotations;
namespace MusicTrack.Models
{
    public class PlayList : BaseModel
    {
        public PlayList()
        {
            Tracks = new List<Track>();
        }

        public string? TrackPosition { get; set; }

        [Required]
        public virtual List<Track> Tracks { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
