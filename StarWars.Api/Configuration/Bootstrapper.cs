using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Interfaces.Repositories;
using StarWars.BusinessLogic.Services;
using StarWars.BusinessLogic.Services.Interfaces;
using StarWars.DataAccess.Repository;

namespace StarWars.Api.Configuration
{
    public static class Bootstrapper
    {
        public static int ICharacterService { get; private set; }

        public static IServiceCollection Bootstrap(this IServiceCollection services, IConfiguration configuration)
        {
            //var appConfiguration = new ApiConfiguration(configuration);
            services.AddScoped<IStarWarsApiConfiguration, ApiConfiguration>();
            // Business logic layer
            services.AddScoped<ICharactersService, CharactersService>();
            // Data access layer
            services.AddScoped<ICharacterRepository, CharacterRepository>();

            return services;
        }
    }
}
