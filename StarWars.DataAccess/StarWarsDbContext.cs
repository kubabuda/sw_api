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
        public DbSet<SwEpisode> Episodes { get; set; }
        public DbSet<SwCharacter> Characters { get; set; }

        public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureTables(modelBuilder);

            SeedData(modelBuilder);
        }

        private static void ConfigureTables(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SwEpisode>(ep =>
            {
                ep.HasKey(o => o.Id);
                ep.Property(o => o.Id).IsRequired();
                ep.Property(o => o.Name).IsRequired();
            });

            modelBuilder.Entity<SwCharacter>(c =>
            {
                c.HasKey(o => o.Id);
                c.Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
                c.Property(o => o.Name).IsRequired();
            });

            modelBuilder.Entity<SwEpisodeCharacter>(ec =>
            {
                ec.HasKey(bc => new { bc.SwEpisodeId, bc.SwCharacterId });
                
                ec.HasOne(bc => bc.SwEpisode)
                    .WithMany(e => e.Characters)
                    .HasForeignKey(bc => bc.SwCharacterId)
                    .IsRequired();
                
                ec.HasOne(bc => bc.SwCharacter)
                    .WithMany(c => c.Episodes)
                    .HasForeignKey(bc => bc.SwEpisodeId)
                    .IsRequired();
            });

            modelBuilder.Entity<SwCharacterFriendship>(f =>
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
            var newhope = new SwEpisode { Id = 4, Name = "NEWHOPE" };
            var empire = new SwEpisode { Id = 5, Name = "EMPIRE" };
            var jedi = new SwEpisode { Id = 6, Name = "JEDI" };

            modelBuilder.Entity<SwEpisode>(ep => ep.HasData(newhope, empire, jedi));

            var luke_skywalker = new SwCharacter
            {
                Id = 1,
                Name = "Luke Skywalker"
            };
            var darth_vader = new SwCharacter
            {
                Id = 2,
                Name = "Darth Vader"
            };
            var han_solo = new SwCharacter
            {
                Id = 3,
                Name = "Han Solo"
            };
            var leia_organa = new SwCharacter
            {
                Id = 4,
                Name = "Leia Organa",
                Planet = "Alderaan"
            };
            var wilhuff_tarkin = new SwCharacter
            {
                Id = 5,
                Name = "Wilhuff Tarkin"
            };
            var c_3p0 = new SwCharacter
            {
                Id = 6,
                Name = "C-3PO"                
            };
            var r2_d2 = new SwCharacter
            {
                Id = 7,
                Name = "R2-D2"
            };

            modelBuilder.Entity<SwCharacter>(c => c.HasData(
                new[] {
                    luke_skywalker,
                    darth_vader,
                    han_solo,
                    leia_organa,
                    wilhuff_tarkin,
                    c_3p0,
                    r2_d2
            }));

            //modelBuilder.Entity<SwEpisodeCharacter>(c => c.HasData(
                //new SwEpisodeCharacter { SwEpisodeId = newhope.Id, SwCharacterId = luke_skywalker.Id },
                //new SwEpisodeCharacter { SwEpisodeId = empire.Id, SwCharacterId = luke_skywalker.Id },
                //new SwEpisodeCharacter { SwEpisodeId = jedi.Id, SwCharacterId = luke_skywalker.Id },
                //new SwEpisodeCharacter { SwEpisodeId = newhope.Id, SwCharacterId = darth_vader.Id },
                //new SwEpisodeCharacter { SwEpisodeId = empire.Id, SwCharacterId = darth_vader.Id },
                //new SwEpisodeCharacter { SwEpisodeId = jedi.Id, SwCharacterId = darth_vader.Id },
                //new SwEpisodeCharacter { SwEpisodeId = newhope.Id, SwCharacterId = han_solo.Id },
                //new SwEpisodeCharacter { SwEpisodeId = empire.Id, SwCharacterId = han_solo.Id },
                //new SwEpisodeCharacter { SwEpisodeId = jedi.Id, SwCharacterId = han_solo.Id },
                //new SwEpisodeCharacter { SwEpisodeId = newhope.Id, SwCharacterId = leia_organa.Id },
                //new SwEpisodeCharacter { SwEpisodeId = empire.Id, SwCharacterId = leia_organa.Id },
                //new SwEpisodeCharacter { SwEpisodeId = jedi.Id, SwCharacterId = leia_organa.Id },
                //new SwEpisodeCharacter { SwEpisodeId = newhope.Id, SwCharacterId = wilhuff_tarkin.Id },
                //new SwEpisodeCharacter { SwEpisodeId = newhope.Id, SwCharacterId = c_3p0.Id },
                //new SwEpisodeCharacter { SwEpisodeId = empire.Id, SwCharacterId = c_3p0.Id },
                //new SwEpisodeCharacter { SwEpisodeId = jedi.Id, SwCharacterId = c_3p0.Id },
                //new SwEpisodeCharacter { SwEpisodeId = newhope.Id, SwCharacterId = r2_d2.Id },
                //new SwEpisodeCharacter { SwEpisodeId = empire.Id, SwCharacterId = r2_d2.Id },
            //    new SwEpisodeCharacter { SwEpisodeId = jedi.Id, SwCharacterId = r2_d2.Id }
            //));

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
