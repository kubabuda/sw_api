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

namespace StarWarsApi.IntegrationTests.Services
{
    [TestFixture]
    public class CharactersServiceTests : IDisposable
    {
        private DbConnection _connection;

        private ICharactersService _serviceUnderTests;

        [SetUp]
        public void Setup()
        {
            _connection = InMemoryDbConnectionFactory.CreateInMemoryDatabase();

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

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
