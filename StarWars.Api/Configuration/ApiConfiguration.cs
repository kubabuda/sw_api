using Microsoft.Extensions.Configuration;
using StarWars.BusinessLogic.Interfaces;

namespace StarWars.Api.Configuration
{
    public class ApiConfiguration : IStarWarsApiConfiguration
    {
        public ApiConfiguration(IConfiguration configuration)
        {
            // DatabasePath = configuration["DatabasePath"];
            PageSize = 5;
        }

        public int PageSize { get; }
    }
}
