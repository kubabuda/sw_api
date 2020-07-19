using Microsoft.EntityFrameworkCore;
using StarWars.DataAccess.Models;

namespace StarWars.DataAccess
{
    public class StarWarsDbContext : DbContext
    {
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Character> Characters { get; set; }
        
        public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTables(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Episode>(ep =>
            {
                ep.HasKey(o => o.Id);
                ep.Property(o => o.Id).IsRequired();
                ep.Property(o => o.Name).IsRequired();
            });

            modelBuilder.Entity<Character>(c =>
            {
                c.HasKey(o => o.Id);
                c.Property(o => o.Id).IsRequired();
                c.Property(o => o.Name).IsRequired();
            });

            modelBuilder.Entity<EpisodeCharacter>()
                .HasKey(bc => new { bc.EpisodeId, bc.CharacterId });

            modelBuilder.Entity<EpisodeCharacter>()
                .HasOne(bc => bc.Episode)
                .WithMany(e => e.EpisodeCharacters)
                .HasForeignKey(bc => bc.EpisodeId);

            modelBuilder.Entity<EpisodeCharacter>()
                .HasOne(bc => bc.Character)
                .WithMany(c => c.EpisodeCharacters)
                .HasForeignKey(bc => bc.CharacterId);

            modelBuilder.Entity<CharacterFriendship>(f =>
            {
                f.HasKey(bc => new { bc.FriendId, bc.FriendOfId });

                f.HasOne(bc => bc.Friend)
                    .WithMany(e => e.FriendsOf)
                    .HasForeignKey(bc => bc.FriendId)
                    .IsRequired();

                f.HasOne(bc => bc.FriendOf)
                    .WithMany(c => c.Friends)
                    .HasForeignKey(bc => bc.FriendOfId)
                    .IsRequired();
            });
        }
    }
}
