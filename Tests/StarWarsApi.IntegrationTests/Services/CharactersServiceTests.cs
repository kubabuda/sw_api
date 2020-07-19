using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using StarWars.Api.Configuration;
using StarWars.BusinessLogic.Services;
using StarWars.BusinessLogic.Services.Interfaces;
using StarWars.DataAccess;
using StarWarsApi.IntegrationTests.Infrastructure;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace StarWarsApi.IntegrationTests.Services
{
    [TestFixture]
    public class CharactersServiceTests : IDisposable
    {
        private DbConnection _connection;

        private ICharactersService _serviceUnderTests;

        [SetUp]
        public async Task Setup()
        {
            _connection = InMemoryDbConnectionFactory.CreateInMemoryDbConnection();
            await PrepareTestDbContext(_connection);
            IConfiguration configuration = Substitute.For<IConfiguration>();
            var services = new ServiceCollection();
            services.Bootstrap(configuration);
            // overwrites
            services.AddDbContext<StarWarsDbContext>(options =>
                options.UseSqlite(_connection));
            // resolve tested component
            var provider = services.BuildServiceProvider();
            _serviceUnderTests = provider.GetRequiredService<ICharactersService>();
        }


        public async Task PrepareTestDbContext(DbConnection connection)
        {
            var options = new DbContextOptionsBuilder<StarWarsDbContext>()
                .UseSqlite(connection)
                .Options;
            var dbContext = new StarWarsDbContext(options);
            await dbContext.Database.MigrateAsync();
            await dbContext.SeedAsync();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
