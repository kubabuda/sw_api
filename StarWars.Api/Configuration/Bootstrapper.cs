using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Services;
using StarWars.BusinessLogic.Services.Interfaces;
using StarWars.DataAccess;
using StarWars.DataAccess.Infrastructure;
using StarWars.DataAccess.Repository;

namespace StarWars.Api.Configuration
{
    public static class Bootstrapper
    {
        public static int ICharacterService { get; private set; }

        public static IServiceCollection Bootstrap(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurations
            var appConfiguration = new ApiConfiguration(configuration);
            services.AddScoped<IStarWarsApiConfiguration, ApiConfiguration>();
            services.AddAutoMapper(typeof(Startup));
            // Business logic layer
            BootstrapBusinessLogic(services);
            // Data access layer
            BootstrapDataAccess(services, appConfiguration);

            return services;
        }

        // TODO move to proper projec
        public static void BootstrapDataAccess(IServiceCollection services, ApiConfiguration appConfiguration)
        {
            services.AddScoped<ICharacterRepository, CharacterRepository>();
            if (!IsInMemmoryDb(appConfiguration))
            {
                services.AddDbContext<StarWarsDbContext>(options =>
                    options.UseSqlite(appConfiguration.DatabasePath));
            }
            else
            {
                // for tests keep single connection all the time
                var connection = InMemoryDbConnectionFactory.CreateInMemoryDbConnection();
                var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                    .UseSqlite(connection)
                    .Options;
                PrepareContext(options);
                services.AddScoped<StarWarsDbContext>(_ => new StarWarsDbContext(options));
            }

            static bool IsInMemmoryDb(ApiConfiguration appConfiguration)
            {
                return (appConfiguration.DatabasePath == DataAccessConstants.InMemoryDbPath);
            }

            static void PrepareContext(DbContextOptions<StarWarsDbContext> options)
            {
                // get new in memory connection
                var dbContext = new StarWarsDbContext(options);
                // enforce DB migration 
                dbContext.Database.Migrate();
                dbContext.Seed();
                dbContext.SaveChangesAsync();
            }
        }

        // TODO move to proper projec
        public static void BootstrapBusinessLogic(IServiceCollection services)
        {
            services.AddScoped<ICharactersService, CharactersService>();
            services.AddScoped<IValidateActionsService, ValidateActionsService>();
        }
    }
}
