using Microsoft.Extensions.Configuration;
using StarWars.BusinessLogic.Interfaces;

namespace StarWars.Api.Configuration
{
    public class ApiConfiguration : IStarWarsApiConfiguration
    {
        public ApiConfiguration(IConfiguration configuration)
        {
            DatabasePath = configuration["DatabasePath"];
            PageSize = int.Parse(configuration["PageSize"]);
        }

        public string DatabasePath { get; }
        public int PageSize { get; }
    }
}
