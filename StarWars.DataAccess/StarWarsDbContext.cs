using Microsoft.EntityFrameworkCore;
using StarWars.DataAccess.Models;

namespace StarWars.DataAccess
{
    public class StarWarsDbContext : DbContext
    {
        public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options)
            : base(options)
        {

        }

        public DbSet<SwEpisode> Episodes { get; set; }
        public DbSet<SwCharacter> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SwEpisode>(ep =>
            {
                ep.HasKey(o => o.Id);
                ep.Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
                ep.Property(o => o.Name).IsRequired();
                // TODO move to migration seed
                ep.HasData(
                    new SwEpisode { Id = 1, Name = "NEWHOPE" },
                    new SwEpisode { Id = 2, Name = "EMPIRE" },
                    new SwEpisode { Id = 3, Name = "JEDI" });
            });

            modelBuilder.Entity<SwCharacter>(c => { 
                c.HasKey(o => o.Id);
                c.Property(o => o.Id).IsRequired().ValueGeneratedOnAdd();
                c.Property(o => o.Name).IsRequired();
                c.HasData(
                    new SwCharacter { Id = 1, Name = "Luke Skywalker" });
            });
        }
    }
}
