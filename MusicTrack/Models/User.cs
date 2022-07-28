using Microsoft.AspNetCore.Identity;

namespace MusicTrack.Models
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            UserRoles = new List<UserRole>();
        }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
