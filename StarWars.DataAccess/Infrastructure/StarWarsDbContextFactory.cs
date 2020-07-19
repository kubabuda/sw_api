using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
}
