using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Models
{
    public class Track : BaseModel
    {
        public Track()
        {
            PlayLists = new List<PlayList>();
        }

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

        [Required]
        public Guid AlbumId { get; set; }

        [Required]
        public virtual Album Album { get; set; } = null!;

        [Required]
        public virtual ICollection<PlayList> PlayLists { get; set; }
    }
}
