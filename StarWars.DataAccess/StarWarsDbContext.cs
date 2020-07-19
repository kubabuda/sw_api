using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StarWars.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            //modelBuilder.Entity<CharacterFriendship>(f =>
            //{
            //    f.HasKey(bc => new { bc.FriendId, bc.FriendOfId });
                
            //    f.HasOne(bc => bc.Friend)
            //        .WithMany(e => e.FriendsOf)
            //        .HasForeignKey(bc => bc.FriendId)
            //        .IsRequired();
                
            //    f.HasOne(bc => bc.FriendOf)
            //        .WithMany(c => c.Friends)
            //        .HasForeignKey(bc => bc.FriendOfId)
            //        .IsRequired();
            //});
        }

        public async Task SeedAsync() 
        {
            var newhope = new Episode { Id = 4, Name = "NEWHOPE" };
            var empire = new Episode { Id = 5, Name = "EMPIRE" };
            var jedi = new Episode { Id = 6, Name = "JEDI" };

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

            luke_skywalker.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = luke_skywalker.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = luke_skywalker.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = luke_skywalker.Id }
            };

            darth_vader.EpisodeCharacters = new List<EpisodeCharacter> {
            new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = darth_vader.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = darth_vader.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = darth_vader.Id }
            };

            han_solo.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = han_solo.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = han_solo.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = han_solo.Id }
            };

            leia_organa.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = leia_organa.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = leia_organa.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = leia_organa.Id }
            };

            wilhuff_tarkin.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = wilhuff_tarkin.Id }
            };

            c_3p0.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = c_3p0.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = c_3p0.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = c_3p0.Id }
            };

            r2_d2.EpisodeCharacters = new List<EpisodeCharacter> {
                new EpisodeCharacter { EpisodeId = newhope.Id, CharacterId = r2_d2.Id },
                new EpisodeCharacter { EpisodeId = empire.Id, CharacterId = r2_d2.Id },
                new EpisodeCharacter { EpisodeId = jedi.Id, CharacterId = r2_d2.Id }
            };

            await Episodes.AddRangeAsync(newhope, empire, jedi);
            await Characters.AddRangeAsync(
                luke_skywalker,
                darth_vader,
                han_solo,
                leia_organa,
                wilhuff_tarkin,
                c_3p0,
                r2_d2);
            await SaveChangesAsync();
        }
    }
}
