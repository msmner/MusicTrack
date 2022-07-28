using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicTrack.Models;

namespace MusicTrack.Infrastructure
{
    public class MusicTrackDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Album> Albums { get; set; } = null!;

        public DbSet<Track> Tracks { get; set; } = null!;

        public DbSet<PlayList> PlayLists { get; set; } = null!;

        public MusicTrackDbContext() : base()
        {
        }

        public MusicTrackDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(r => r.Role)
                .HasForeignKey(r => r.RoleId)
                .IsRequired();

            modelBuilder.Entity<PlayList>()
            .HasIndex(p => p.Name)
            .IsUnique();
        }
    }
}
