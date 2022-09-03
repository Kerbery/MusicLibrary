using Microsoft.EntityFrameworkCore;
using PlaylistService.Models;

namespace PlaylistService.Data
{
    public class PlaylistContext : DbContext
    {
        public PlaylistContext(DbContextOptions<PlaylistContext> options) : base(options)
        {
        }

        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlaylistItem>()
                .HasOne(x => x.Playlist)
                .WithMany(pl => pl.Items)
                .HasForeignKey(pi => pi.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PlaylistItem>()
                .HasOne(pi => pi.Track)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Playlist>()
                .HasOne(a => a.User)
                .WithMany(b => b.Playlists)
                .HasForeignKey(p=>p.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Playlists)
                .WithOne(pl => pl.User)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Track>()
                .HasOne(t => t.User)
                .WithMany();

            modelBuilder.Entity<User>().HasData(new User
            {
                Permalink = "ted",
                UserName = "ted",
                AvatarUrl = "",
                FirstName = "ted",
                LastName = "tan",
                Id = Guid.NewGuid(),
            });
        }
    }
}
