using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Models
{
    public class Album : BaseModel
    {
        public Album()
        {
            Tracks = new List<Track>();
        }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int PublishingYear { get; set; }

        [Required]
        public virtual ICollection<Track> Tracks { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }
    }
}