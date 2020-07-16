using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace StarWars.Api.Configuration
{
    public static class Bootstrapper
    {
        public static IServiceCollection Bootstrap(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
