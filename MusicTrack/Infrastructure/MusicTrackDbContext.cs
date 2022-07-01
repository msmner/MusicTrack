using Microsoft.EntityFrameworkCore;
using MusicTrack.Models;

namespace MusicTrack.Infrastructure
{
    public class MusicTrackDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; } = null!;

        public DbSet<Track> Tracks { get; set; } = null!;

        public DbSet<PlayList> PlayLists { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;

        public MusicTrackDbContext() : base()
        {
        }

        public MusicTrackDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
