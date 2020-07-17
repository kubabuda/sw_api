using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarWars.BusinessLogic.Interfaces;
using StarWars.BusinessLogic.Services;
using StarWars.BusinessLogic.Services.Interfaces;

namespace StarWars.Api.Configuration
{
    public static class Bootstrapper
    {
        public static int ICharacterService { get; private set; }

        public static IServiceCollection Bootstrap(this IServiceCollection services, IConfiguration configuration)
        {
            //var appConfiguration = new ApiConfiguration(configuration);

            services.AddScoped<IStarWarsApiConfiguration, ApiConfiguration>();
            services.AddScoped<ICharactersService, CharactersService>();

            return services;
        }
    }
}
