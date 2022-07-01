using System.ComponentModel.DataAnnotations;

namespace MusicTrack.Models
{
    public abstract class BaseModel : IAuditInfo
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
