using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StarWars.DataAccess.Models;

namespace StarWars.DataAccess
{
    public class StarWarsDbContextFactory : IDesignTimeDbContextFactory<StarWarsDbContext>
    {
        public StarWarsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseSqlite("Filename=:memory:"); // TODO use real file

            return new StarWarsDbContext(optionsBuilder.Options);
        }
    }

    public class StarWarsDbContext : DbContext
    {
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<EpisodeCharacter> EpisodeCharacters { get; set; }
        public DbSet<CharacterFriendship> CharacterFriendships { get; set; }

        public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTables(modelBuilder);

            SeedData(modelBuilder);

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
                c.Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
                c.Property(o => o.Name).IsRequired();
            });

            modelBuilder.Entity<EpisodeCharacter>()
                .HasKey(bc => new { bc.EpisodeId, bc.CharacterId });

            modelBuilder.Entity<EpisodeCharacter>()
                .HasOne(bc => bc.Episode)
                .WithMany(e => e.Characters)
                .HasForeignKey(bc => bc.EpisodeId);

            modelBuilder.Entity<EpisodeCharacter>()
                .HasOne(bc => bc.Character)
                .WithMany(c => c.Episodes)
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

        private static void SeedData(ModelBuilder modelBuilder)
        {
            var newhope = new Episode { Id = 4, Name = "NEWHOPE" };
            var empire = new Episode { Id = 5, Name = "EMPIRE" };
            var jedi = new Episode { Id = 6, Name = "JEDI" };

            modelBuilder.Entity<Episode>(ep => ep.HasData(newhope, empire, jedi));

            var luke_skywalker = new Character
            {
                Id = 1,
                Name = "Luke Skywalker"
            };
            var darth_vader = new Character
            {
                Id = 2,
                Name = "Darth Vader"
            };
            var han_solo = new Character
            {
                Id = 3,
                Name = "Han Solo"
            };
            var leia_organa = new Character
            {
                Id = 4,
                Name = "Leia Organa",
                Planet = "Alderaan"
            };
            var wilhuff_tarkin = new Character
            {
                Id = 5,
                Name = "Wilhuff Tarkin"
            };
            var c_3p0 = new Character
            {
                Id = 6,
                Name = "C-3PO"                
            };
            var r2_d2 = new Character
            {
                Id = 7,
                Name = "R2-D2"
            };

            modelBuilder.Entity<Character>(c => c.HasData(
                new[] {
                    luke_skywalker,
                    darth_vader,
                    han_solo,
                    leia_organa,
                    wilhuff_tarkin,
                    c_3p0,
                    r2_d2
            }));

            modelBuilder.Entity<EpisodeCharacter>(c => c.HasData(
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = luke_skywalker.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = luke_skywalker.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = luke_skywalker.Id },
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = darth_vader.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = darth_vader.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = darth_vader.Id },
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = han_solo.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = han_solo.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = han_solo.Id },
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = leia_organa.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = leia_organa.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = leia_organa.Id },
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = wilhuff_tarkin.Id },
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = c_3p0.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = c_3p0.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = c_3p0.Id },
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = r2_d2.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = r2_d2.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = r2_d2.Id }
            ));

            //luke_skywalker.Friends = new[] { han_solo, leia_organa, c_3p0, r2_d2 };
            //darth_vader.Friends = new[] { wilhuff_tarkin };
            //han_solo.Friends = new[] { luke_skywalker, leia_organa, r2_d2 };
            //leia_organa.Friends = new[] { luke_skywalker, han_solo, c_3p0, r2_d2 };
            //wilhuff_tarkin.Friends = new[] { darth_vader };
            //c_3p0.Friends = new[] { luke_skywalker, han_solo, leia_organa, r2_d2 };
            //r2_d2.Friends = new[] { luke_skywalker, han_solo, leia_organa };

            //luke_skywalker.Episodes = new[] { newHope, empire, jedi };
            //darth_vader.Episodes = new[] { newHope, empire, jedi };
            //han_solo.Episodes = new[] { newHope, empire, jedi };
            //leia_organa.Episodes = new[] { newHope, empire, jedi };
            //wilhuff_tarkin.Episodes = new[] { newHope };
            //c_3p0.Episodes = new[] { newHope, empire, jedi };
            //r2_d2.Episodes = new[] { newHope, empire, jedi }
        }
    }
}
